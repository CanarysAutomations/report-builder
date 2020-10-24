# Get User and Organization reports 📈 with GraphQL
 
The tool will help you get the details of **User**, **User activity** and **Organization details.** Using GraphQL queries, the application access the GitHub's **GraphQL API Endpoints** to create user and organization reports. The only input required is the name of the GitHub Organization. The Organization name is included with GraphQL queries as part of the request to generate the response for the reports.

We have 3 queries to fetch **User**, **User activity** and **Organization data**. Based on the response received, these queries are sent in a single request to generate the report.

For ease of viewing and analyzing, the JSON response is formatted into excel data and attached to separate excel sheets.

## Data populated in the report

### User

- email
- company
- login
- last active contribution

### User Activity

- login
- last active contribution
- last contributed repository

### Organization Details

- email
- login

## GraphQL

GraphQL is an open-source data query and manipulation language for APIs. With GraphQL, it is possible to customize responses to match the requests that are submitted. With a single response, many resources can be retrieved. GraphQL implementations are provided by several distinct programming languages including C#, Erlang, Java, PHP etc.

## Why

One of the great benefits is that with a single request, all the data you need can be obtained, however complicated it may be.

## Application Uses

 - As the data collected is the public record of GitHub, no special access is needed
 - You will see a summary of users and organizational level data from the excel report

### Prerequisites

 - GitHub Account & PAT Token
 
### Usage Instructions :memo:

To learn how to setup and use the tool, click [here](https://github.com/CanarysAutomations/report-builder/wiki).

### Current Report Limitations :x: :x:

- Because of GitHub's strict privacy rules, public information about users and organizations will be added to the report
- However, the tool may also be tailored to conform to the needs of a specific organisation.
    





