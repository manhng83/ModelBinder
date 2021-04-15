using NetCoreModelBinder.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NetCoreModelBinder.ConsoleApp
{
    /// <summary>
    /// Refer: https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-5.0
    /// Refer: https://www.paddingleft.com/2018/04/11/HttpClient-PostAsync-GetAsync-Example/ (HAY HAY HAY)
    /// </summary>
    internal class Program
    {
        static string m_BaseAddress = "https://localhost:44349/";

        static HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            var data = new Product()
            {
                Category = "Smart Phone",
                Name = "iPhone",
                Id = "1",
                Price = 2000
            };

            await TestGet(data).ConfigureAwait(false);
            await TestPostFormUrlEncodedContent().ConfigureAwait(false);
            await TestPost(data).ConfigureAwait(false);

            ExitProgram();
        }

        static void ExitProgram()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(-1);
        }

        static async Task TestGet(Product data)
        {
            client.BaseAddress = new Uri(m_BaseAddress);
            //string endpoint = "api/values/get";
            string endpoint = "api/values/get1";
            //string endpoint = "api/values/get2";
            string requestUrl = $"{endpoint}?{ObjectExtensions.GetQueryString(data)}";
            var response = await client.GetAsync(requestUrl).ConfigureAwait(false);
            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }

        static async Task TestPostFormUrlEncodedContent()
        {
            client.BaseAddress = new Uri(m_BaseAddress);

            var parameters = new Dictionary<string, string>();
            parameters["Id"] = "Id 123";
            parameters["Category"] = "Category 123";
            parameters["Name"] = "Name 123";
            parameters["Price"] = "2000";

            var response = await client.PostAsync("api/values/post", new FormUrlEncodedContent(parameters));
            var contents = await response.Content.ReadAsStringAsync();

            Console.WriteLine(contents);
        }

        static async Task TestPost(Product data)
        {
            client.BaseAddress = new Uri(m_BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var myContent = JsonConvert.SerializeObject(data);

            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //HttpResponseMessage response = await client.PostAsync("api/values/post", byteContent);
            //HttpResponseMessage response = await client.PostAsync("api/values/post1", byteContent);
            //HttpResponseMessage response = await client.PostAsync("api/values/post2", byteContent);
            HttpResponseMessage response = await client.PostAsync("api/values/test", byteContent);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            // Write URI of the created resource
            Console.WriteLine(result);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public static class ObjectExtensions
    {
        public static string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }
    }
}