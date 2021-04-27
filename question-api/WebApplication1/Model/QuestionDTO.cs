using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace question.api
{
    public class QuestionDTO
    {
     
        [JsonProperty(PropertyName = "question")]
        public string QuestionName { get; set; }
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName = "thumb_url")]
        public string ThumbUrl { get; set; }
        [JsonProperty(PropertyName = "choices")]
        public IList<String> Choices { get; set; }
    }
}
