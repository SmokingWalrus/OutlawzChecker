using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.google.gson;
using Leaf.xNet;
using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace ConsoleApp1
{
    class puff
    {
        public static string getCapture(string username, string key,string proxy)
        {
            try
            {
                main main = new main();
                one one = new one();
                two two = new two();

                one.Line1 = "65b708073fc0480ea92a077233ca87bd";
                one.Line2 = "S-1-5-21-3906878023-3586315189-2161068791";

                two.Line1 = username;
                two.Line2 = key;

                main.one = one;
                main.two = two;
                var stream = new MemoryStream();

                Serializer.Serialize<main>(stream, main);
                HttpRequest request = new HttpRequest();
                var stream2 = new MemoryStream(request.Post("https://login5.spotify.com/v3/login", stream.ToArray(), "application/x-protobuf").ToBytes());
                Responsemain response = Serializer.Deserialize<Responsemain>(stream2);

                request.UserAgent =
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3000.145 Safari/537.36";
                request.Post("https://www.spotify.com/token-bounce/?url=/redirect/account-page", "oauth_token=" + response.one.Line2,
                    "application/x-www-form-urlencoded");
                JObject obj = JObject.Parse(request.Get("https://www.spotify.com/us/home-hub/api/v1/family/home/").ToString());
                return obj.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [ProtoContract]
        class Responsemain
        {
            [ProtoMember(1)]
            public response one { get; set; }
        }

        [ProtoContract]
        class response
        {
            [ProtoMember(1)]
            public string Line1 { get; set; }
            [ProtoMember(2)]
            public string Line2 { get; set; }
            [ProtoMember(3)]
            public string Line3 { get; set; }
        }

        [ProtoContract]
        class main
        {
            [ProtoMember(1)]
            public one one { get; set; }
            [ProtoMember(100)]
            public two two { get; set; }
        }

        [ProtoContract]
        class one
        {
            [ProtoMember(1)]
            public string Line1 { get; set; }
            [ProtoMember(2)]
            public string Line2 { get; set; }
        }

        [ProtoContract]
        class two
        {
            [ProtoMember(1)]
            public string Line1 { get; set; }
            [ProtoMember(2)]
            public string Line2 { get; set; }
        }

    }
}
