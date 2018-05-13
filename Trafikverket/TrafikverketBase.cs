using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Trafikverket.Request;

namespace Trafikverket
{
    public abstract class TrafikverketBase : IDisposable
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Request.Request), new Type[] { typeof(Login), typeof(Query), typeof(BaseGroup), typeof(BaseCondition) });

        static string url = "http://api.trafikinfo.trafikverket.se/v1.3/data.xml";

        protected readonly string apiKey;
        HttpClient client = new HttpClient();

        public TrafikverketBase(string apiKey)
        {
            this.apiKey = apiKey;
        }

        protected async Task<TResult> MakeRequestAsync<TResult>(Request.Request data, CancellationToken cancellationToken)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(TResult));
            string requestXML;
            using (var stream = new MemoryStream())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = false;
                settings.NewLineHandling = NewLineHandling.None;
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    serializer.Serialize(writer, data, ns);
                    writer.Flush();
                }
                stream.Position = 0;
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    requestXML = reader.ReadToEnd();
                }
            }

            var result = await client.PostAsync(url, new StringContent(requestXML, Encoding.UTF8, "text/xml"), cancellationToken);
            using (var stream = await result.Content.ReadAsStreamAsync())
            {
                var response = (TResult)deserializer.Deserialize(stream);
                return response;
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
