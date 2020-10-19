using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using graphqlimplementation.response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;

namespace graphqlimplementation
{

    public class Program
    {
        public static string endCursor1 = null;
        public static string endCursor2 = null;
        public static string endCursor3 = null;
        public static bool FirstTime = true;
        public static List<Nodes> Q1UsersData = new List<Nodes>();
        public static List<Nodes> Q2UsersData = new List<Nodes>();
        public static List<Nodes> Q3UsersData = new List<Nodes>();
        static void Main(string[] args)
        {

            Console.WriteLine("Enter the Organisation");
            string Organization = Console.ReadLine();
            Console.WriteLine("Enter the Directory for the Report to be Generated");
            string outputdir = Console.ReadLine();
            Console.WriteLine("Enter GitHub UserName");
            string git_user = Console.ReadLine();
            Console.WriteLine("Enter GitHub PAT");
            string git_pat = Console.ReadLine();

            string fileName = Organization + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff");

            do
            {
                Object queryObject = new
                {
                    query = @"query($Org:String!,$endCursor:String) { 
                              search(type: USER, query: $Org , first: 100, after: $endCursor) {
                              nodes {
                                ... on User {
                                         login
                                         company
                                         email
                                         contributionsCollection {
                                         commitContributionsByRepository(maxRepositories: 1) {
                                         contributions(first: 1, orderBy: { field: OCCURRED_AT, direction: DESC}) {
                                          nodes {
                                            occurredAt
                                                }
                                        }
                                    }
                                }
                            }
                        }
                    pageInfo {
                         endCursor
                         }
			        }
		        }",
                    variables = new
                    {
                        Org = "type:user " + Organization,
                        endCursor = endCursor1
                    }
                };


                Object queryObjActivity = new
                {
                    query = @"query($Org:String!,$endCursor:String){
                          search(type: USER, query:$Org, first: 100, after:$endCursor) {
                          userCount
                          nodes {
                          ... on User {
                                 login
                                 contributionsCollection {
                                 commitContributionsByRepository(maxRepositories: 1) {
                                 contributions(first: 1, orderBy: { field: OCCURRED_AT, direction: DESC}) {
                                    nodes {
                                        occurredAt
                                        repository {
                                            name
                                        }
                                    }
                                   
                                }
                            }
                            
                        }
                    }
                }
                pageInfo {
                         endCursor
                         }
            }
        }",
                    variables = new
                    {
                        Org = "type:user " + Organization,
                        endCursor = endCursor2

                    }
                };

                Object queryOrgObject = new
                {
                    query = @"query($Org:String!,$endCursor:String) { 
                        search(type: USER, query: $Org , first: 100, after: $endCursor) {
                        nodes {
                       ... on Organization {
                               login
                               email
                             }
                          }
                        userCount
                        pageInfo {
                             endCursor
                            }
                        }
                    }",
                    variables = new
                    {
                        Org = "type:org " + Organization,
                        endCursor = endCursor3
                    }
                };

                if (FirstTime == true)
                {
                    JsonResponse(queryObject, 1, fileName , git_pat, git_user);
                    JsonResponse(queryObjActivity, 2, fileName, git_pat, git_user);
                    JsonResponse(queryOrgObject, 3, fileName, git_pat, git_user);
                }
                else
                {
                    if (!string.IsNullOrEmpty(endCursor1))
                    {
                        JsonResponse(queryObject, 1, fileName, git_pat, git_user);
                    }
                    if (!string.IsNullOrEmpty(endCursor2))
                    {
                        JsonResponse(queryObjActivity, 2, fileName, git_pat, git_user);
                    }
                    if (!string.IsNullOrEmpty(endCursor3))
                    {
                        JsonResponse(queryOrgObject, 3, fileName, git_pat, git_user);
                    }
                }

                FirstTime = false;
            } while (!string.IsNullOrEmpty(endCursor1) || !string.IsNullOrEmpty(endCursor2));
            ResponseModel input = new ResponseModel() { data = new response.Data() { search = new Search() { nodes = Q1UsersData, userCount = Q1UsersData.Count } } };
            jsonToCSV(JsonConvert.SerializeObject(input), 1, fileName, outputdir);
            input = new ResponseModel() { data = new response.Data() { search = new Search() { nodes = Q2UsersData, userCount = Q2UsersData.Count } } };
            jsonToCSV(JsonConvert.SerializeObject(input), 2, fileName, outputdir);
            input = new ResponseModel() { data = new response.Data() { search = new Search() { nodes = Q3UsersData, userCount = Q3UsersData.Count } } };
            jsonToCSV(JsonConvert.SerializeObject(input), 3, fileName, outputdir);
        }

        public static async void JsonResponse(Object query, int sheetnumber, string filename, string git_pat, string git_user)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.github.com/graphql")
            };

            httpClient.DefaultRequestHeaders.Add("User-Agent", "MyConsoleApp");

            string basicValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{git_user}:{git_pat}"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicValue);

            var httprequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json")
            };
            var response = httpClient.SendAsync(httprequest).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseString = await GetResponseContent(response);
                if (responseString != null)
                {
                    ResponseModel responseObjct = JsonConvert.DeserializeObject<ResponseModel>(responseString);
                    if (sheetnumber == 1)
                    {
                        endCursor1 = responseObjct.data.search.pageInfo.endCursor;
                        Q1UsersData.AddRange(responseObjct.data.search.nodes);
                        //jsonToCSV(Q1UsersData, sheetnumber, filename);
                    }
                    else if (sheetnumber == 2)
                    {
                        endCursor2 = responseObjct.data.search.pageInfo.endCursor;
                        Q2UsersData.AddRange(responseObjct.data.search.nodes);
                    }
                    else if (sheetnumber == 3)
                    {
                        endCursor3 = responseObjct.data.search.pageInfo.endCursor;
                        Q3UsersData.AddRange(responseObjct.data.search.nodes);
                    }
                }
            }
        }


        public static async Task<string> GetResponseContent(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public static void jsonToCSV(string jsonData, int sheetNumber, string filename, string outputdir)
        {
            string csvConverted = string.Empty;
            var obj = JObject.Parse(jsonData);

            // Collect column titles: all property names whose values are of type JValue, distinct, in order of encountering them.
            var values = obj.DescendantsAndSelf()
                .OfType<JProperty>()
                .Where(p => p.Value is JValue)
                .GroupBy(p => p.Name)
                .ToList();

            var columns = values.Select(g => g.Key).ToArray();

            // Filter JObjects that have child objects that have values.
            var parentsWithChildren = values.SelectMany(g => g).SelectMany(v => v.AncestorsAndSelf().OfType<JObject>().Skip(1)).ToHashSet();

            // Collect all data rows: for every object, go through the column titles and get the value of that property in the closest ancestor or self that has a value of that name.
            var rows = obj
                .DescendantsAndSelf()
                .OfType<JObject>()
                .Where(o => o.PropertyValues().OfType<JValue>().Any())
                .Where(o => o == obj || !parentsWithChildren.Contains(o)) // Show a row for the root object + objects that have no children.
                .Select(o => columns.Select(c => o.AncestorsAndSelf()
                    .OfType<JObject>()
                    .Select(parent => parent[c])
                    .Where(v => v is JValue)
                    .Select(v => (string)v)
                    .FirstOrDefault())
                    .Reverse() // Trim trailing nulls
                    .SkipWhile(s => s == null)
                    .Reverse());
            //
            // Convert to CSV
            var csvRows = new[] { columns }.Concat(rows).Select(r => string.Join(",", r));
            var csv = string.Join("\n", csvRows);

            csvConverted = csv.ToString();
            Console.WriteLine(csv);

            string worksheetsName = "Sheet";
            string excelFilePath = outputdir + filename + ".xlsx";
            var excelFileInfo = new FileInfo(excelFilePath);
            var excelTextFormat = new ExcelTextFormat();
            excelTextFormat.DataTypes = new eDataTypes[] { eDataTypes.String, eDataTypes.String, eDataTypes.String, eDataTypes.String };
            excelTextFormat.Delimiter = ',';
            excelTextFormat.EOL = "\n";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(excelFileInfo))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetsName + sheetNumber);
                worksheet.Cells["A3"].LoadFromText(csvConverted, excelTextFormat, OfficeOpenXml.Table.TableStyles.Medium25, false);


                List<int> Columns2 = new List<int>
                 {
                     1,1,5,4
                 };
                List<int> Columns1 = new List<int>
                 {
                     4,5,4
                 };
                List<int> Columns3 = new List<int>
                 {
                     2,3,3,3
                 };

                if (sheetNumber == 1)
                {
                    worksheet.Name = "User_Details";
                    worksheet.Cells["B1"].Value = "UserCount";
                    worksheet.Cells["C1"].Value = Q1UsersData.Count;
                    worksheet.Cells["D3"].Value = "LastActive_Contribution";
                    worksheet.Cells.AutoFitColumns();
                }

                if (sheetNumber == 2)
                {
                    foreach (int col in Columns2)
                    {
                        worksheet.DeleteColumn(col);
                    }


                    worksheet.Name = "User_Activity";
                    worksheet.Cells["B1"].Value = "UserCount";
                    worksheet.Cells["C1"].Value = Q2UsersData.Count;
                    worksheet.Cells["B3"].Value = "LastActive_Contribution";
                    worksheet.Cells["C3"].Value = "LastContributed_Repository";
                    worksheet.Cells.AutoFitColumns();

                }

                if (sheetNumber == 3)
                {
                    worksheet.Name = "Organization_Details";
                    foreach (int col in Columns3)
                    {
                        worksheet.DeleteColumn(col);
                    }
                    worksheet.Cells["B1"].Value = "Organization Count";
                    worksheet.Cells["C1"].Value = Q3UsersData.Count;
                    worksheet.Cells.AutoFitColumns();
                }
                package.Save();
            }
        }
    }

    
}
