using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace PizzaUI.ImgToServer
{
    public static class UploadImagesToServer
    {
        public static string UploadImage(string base64Image)
        {
            string server = "https://bv012.novakvova.com";
            UploadDTO upload = new UploadDTO();
            upload.Photo = base64Image;
            string json = JsonConvert.SerializeObject(upload);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            WebRequest request = WebRequest.Create($"{server}/api/account/upload");
            request.Method = "POST";
            request.ContentType = "application/json";
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);

            }

            try
            {
                var response = request.GetResponse();
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    string data = stream.ReadToEnd();
                    var resp = JsonConvert.DeserializeObject<UploadResponseDTO>(data);
                    return server + resp.Image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }

            return null;
        }
    }
}
