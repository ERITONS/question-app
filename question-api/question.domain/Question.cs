using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace question.domain
{
    public class Question
    {
        [JsonProperty(PropertyName = "id")]
        public int QuestionId { get; set; }
        [JsonProperty(PropertyName = "question")]
        public string QuestionName { get; set; }
        [JsonProperty (PropertyName = "image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName="thumb_url")]
        public string ThumbUrl { get; set; }
        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }
        [JsonProperty(PropertyName="choices")]
        public IList<Choice> Choices { get; set; }

        public Question()
        { }

    }
}

