# Get User and Organization Reports 📈 with GraphQL
 
Using GraphQL queries, the application access the GitHub's **GraphQl API Endpoints** to create user and organization reports. The main user Input is considered to be the name of the organization. With this input, GraphQL queries are sent as a request to generate the response for the reports.

We have 3 queries to fetch **User**, **User activity** and **Organization data**. These queries are sent in a single request to generate the report based on the response received.

## Data Populated in the Report

### User

- Email
- Company
- Login
- Last Active Contribution. Captured Data is Time and date the last time user has made any contribution

### User Activity

- Login
- Last Active Contribution
- Last Contributed Repository

### Organization Details

- email
- login

## GraphQL

GraphQL is an open-source data query and manipulation language for APIs. With GraphQL, responses can be tailored to fit the requests which are sent. Also many resources can be fetched with a single response. Many different programming languages support GraphQl implementations including C#, Erlang, Java, PHP etc.

## Why

One of the great advantages is that, all the data you need can be accessed in a single request, however complicated it may be.

## Application Uses

 - No specific access is required as the data fetched is GitHub's public record
 - An overview of the users and organization level data can be seen from the excel report

### Prerequisites

 - GitHub Account & PAT Token
 
### Usage Instructions :memo:

To learn how to setup and use the tool, click [here](https://github.com/CanarysAutomations/report-builder/wiki).

### Current Report Limitations :x: :x:

- Due to GitHub's Strict Policies on privacy, user and organization details which are public will be added to the report
- However the tool can also be modified to tailor a particular organization's needs.
    





