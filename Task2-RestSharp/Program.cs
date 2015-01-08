using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_RestSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            string content = "{\"has_title\":true,\"title\":\"GoodLuck\",\"entries\":[[\"/getting started.pdf\",{\"thumb_exists\":false,\"path\":\"/Getting Started.pdf\",\"client_mtime\":\"Wed, 08 Jan 2014 18:00:54 +0000\",\"bytes\":249159}],[\"/task.jpg\",{\"thumb_exists\":true,\"path\":\"/Task.jpg\",\"client_mtime\":\"Tue, 14 Jan 2014 05:53:57 +0000\",\"bytes\":207696}]]}";
            var response = new RestResponse();
            response.ContentType = "application/json";
            response.Content = content;

            var deSerializer = new JsonDeserializer();

            var result = deSerializer.Deserialize<Data>(response);
        }
    }

    class Data
    {
        [DeserializeAs(Name = "has_title")]
        public bool HasTitle { get; set; }
        [DeserializeAs(Name = "Title")]
        public string Title { get; set; }

        [DeserializeAs(Name = "entries")]
        public List<List<object>> Enteries { get; set; }
    }

}
