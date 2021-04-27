using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace question.domain
{
    public class Choice
    {
        [JsonProperty(PropertyName = "choice")]
        public string ChoiceName { get; set; }
        [JsonProperty(PropertyName = "votes")]
        public int Votes { get; set; }
        [JsonIgnore]
        public int ChoiceId { get; set; }
        [JsonIgnore]
        public Question Question { get; set; }
    }
}
