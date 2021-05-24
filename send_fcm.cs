using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace FCM_testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();
            obj.sendFCM();
        }

        String serverKey = "";
        String senderId = "";                                                                                                                                                                   


        void sendFCM()
        {
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = "/topics/fcm_test",
                priority = "high",
                content_available = true,
                notification = new
                {
                    title = "",
                    body = " ",
                    badge = 1,
                    sound = "default",
                    priority= "high",
                },
                data = new
                {
                    key1 = "value1",
                    key2 = "value2"
                }
            };
            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }
        }

    }
}




