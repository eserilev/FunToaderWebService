using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace FuntoaderWebService.Services
{
    public class PushNotificationSender
    {
        private HttpClient client;
        private HttpResponseMessage response;
        private HttpContent content;
        private UriBuilder builder;
        private NameValueCollection query;
        private string result;
        private string topic;
        private string EventCode;
        private DateTime currDate;
        public enum mPushType
        {
            command,
            wake_up
        };
        private static string endpoint = "http://54.171.138.196/api_functions_push_notification";

        public PushNotificationSender()
        {
            currDate = DateTime.Now;
        }
        public async void SendPushNotification(string message, mPushType type)
        {
            topic = BuildTopic();
            string url = BuildRequestParams(topic, message, type);
            await HttpGet(url);
        }

        private string BuildTopic()
        {
            string topic = "";
            try
            {
                topic = EventCode.ToString() + "_" + currDate.ToString("MM-dd-yy");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return topic;
        }

        private async Task HttpGet(string url)
        {
            try
            {
                Console.WriteLine("Making a request to the following endpoint: " + url);
                using (client = new HttpClient())
                using (response = await client.GetAsync(url))
                using (content = response.Content)
                {
                    result = await content.ReadAsStringAsync();
                    Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string BuildRequestParams(string topic, string message, mPushType type)
        {
            string url = null;
            try
            {
                builder = new UriBuilder(endpoint);
                query = HttpUtility.ParseQueryString(builder.Query);

                query["topic"] = topic;
                query["message"] = message;
                query["type"] = type.ToString();
                builder.Query = query.ToString();
                url = builder.ToString();

                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}