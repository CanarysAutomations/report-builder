using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace graphqlimplementation
{
   
    public partial class UseModel
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }

    public partial class User
    {
        [JsonProperty("userCount")]
        public long UserCount { get; set; }

        [JsonProperty("nodes")]
        public UserNode[] Nodes { get; set; }
    }

    public partial class UserNode
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("contributionsCollection")]
        public ContributionsCollection ContributionsCollection { get; set; }
    }

    public partial class ContributionsCollection
    {
        [JsonProperty("hasActivityInThePast")]
        public bool HasActivityInThePast { get; set; }

        [JsonProperty("hasAnyContributions")]
        public bool HasAnyContributions { get; set; }

        [JsonProperty("commitContributionsByRepository")]
        public CommitContributionsByRepository[] CommitContributionsByRepository { get; set; }
    }

    public partial class CommitContributionsByRepository
    {
        [JsonProperty("contributions")]
        public Contributions Contributions { get; set; }
    }

    public partial class Contributions
    {
        [JsonProperty("nodes")]
        public ContributionsNode[] Nodes { get; set; }
    }

    public partial class ContributionsNode
    {
        [JsonProperty("occurredAt")]
        public DateTimeOffset OccurredAt { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }
    }

    public partial class Repository
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

