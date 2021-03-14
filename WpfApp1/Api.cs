using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Api
    {
        public static  string InPostAsync(ApiPost apiPost, string name)
        {
            WebRequest request = WebRequest.Create(String.Format("http://localhost:5000/api/analyzer/{0}", name));
            request.Method = "POST"; // для отправки используется метод Post

            // данные для отправки
            string data = JsonConvert.SerializeObject(apiPost);
            // преобразуем данные в массив байтов
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            // устанавливаем тип содержимого - параметр ContentType
            request.ContentType = "application/json";
            // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
            request.ContentLength = byteArray.Length;

            //записываем данные в поток запроса
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response =  request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    

        public static ApiPost OutGEtAsync(string name)
        {
            WebRequest request = WebRequest.Create(String.Format("http://localhost:5000/api/analyzer/{0}", name));

            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    var content = reader.ReadToEnd();
                    response.Close();
                    return JsonConvert.DeserializeObject<ApiPost>(content);
                }
            }

        }
    }



    public class ApiPost
    {
        public  string patient { get; set; }

        public List<service> services { get; set; }

    }


    public class service
    {
        public int serviceCode { get; set; }
        public int code { get; set; }

        public string result { get; set; }

    }

    
}
