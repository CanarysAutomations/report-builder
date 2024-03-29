![.NET Core](https://github.com/CanarysAutomations/report-builder/workflows/.NET%20Core/badge.svg)

# Get User and Organization Reports 📈 with GraphQL

## Overview

The tool will help you get the details of **User**, **User activity** and **Organization details.** Using GraphQL queries, the application access the GitHub's **GraphQL API Endpoints** to create user and organization reports. The main user input is considered to be name of the organization. With this input, GraphQL queries are sent as a request to generate the response for the reports.

We have 3 queries to fetch **User**, **User activity** and **Organization data**. These queries are sent in a single request to generate the report based on the response received.

## :warning: Current Challenges

- This is how the data is viewed when we need to search GitHub for details at the organisational level

  ![img](./images/github-search-window.PNG)

- To analyse and sort this data, you'll need to go through every page on GitHub manually
- There are only a few tools available to export data to an excel file and review graphql reports online

### Advantages

- Separates user and Organization data. Get the precise number of users and Organization
- Ready-to-read export in excel format

## Data populated in the report

Below are the details populated in the report.

|User| User Activity | Organization|
|----|---------------|-------------|
|<ul><li>email</li><li>company</li><li>login</li><li>last active contribution</li></ul> | <ul><li>login</li><li>last active contribution</li><li>last contributed repository</li></ul> | <ul><li>email</li><li>login</li></ul>|

## GraphQL

GraphQL is an open-source data query and manipulation language for APIs. With GraphQL, it is possible to customize responses to match the requests that are submitted. Also many resources can be fetched with a single response. With a single response, many resources can be retrieved. GraphQL implementations are provided by several distinct programming languages including C#, Erlang, Java, PHP etc.

## Why

One of the great benefits is that with a single request, all the data you need can be obtained, however complicated it may be.

## Application Uses

 - As the data collected is the public record of GitHub, no special access is needed
 - You will see a summary of users and organizational level data from the excel report

### :mega: Prerequisites

 - GitHub Account & PAT Token

### :arrow_down: Download 

Download the tool from [here](https://github.com/CanarysAutomations/report-builder/releases).

### :memo: Usage Instructions 

To learn how to setup and use the tool, click [here](https://github.com/CanarysAutomations/report-builder/wiki).

### Current Report Limitations :x: :x:

- Only public records of users and organisations will be added to the report
    



