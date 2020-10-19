using System;
using System.Collections.Generic;
using System.Text;

namespace graphqlimplementation.response
{
    public class Repository
    {
        public string name { get; set; }

    }
    public class Nodes1
    {
        public DateTime occurredAt { get; set; }
        public Repository repository { get; set; }

    }
    public class Contributions
    {
        public IList<Nodes1> nodes { get; set; }

    }
    public class CommitContributionsByRepository
    {
        public Contributions contributions { get; set; }

    }
    public class ContributionsCollection
    {
        public IList<CommitContributionsByRepository> commitContributionsByRepository { get; set; }

    }
    public class Nodes
    {
        public string email { get; set; }
        //public string name { get; set; }
        public string company { get; set; }
        public string login { get; set; }
        public ContributionsCollection contributionsCollection { get; set; }

    }
    public class PageInfo
    {
        public string endCursor { get; set; }

    }
    public class Search
    {
        public IList<Nodes> nodes { get; set; }
        public int userCount { get; set; }
        public PageInfo pageInfo { get; set; }

    }
    public class Data
    {
        public Search search { get; set; }

    }
    public class ResponseModel
    {
        
        public Data data { get; set; }

    }
    
}
