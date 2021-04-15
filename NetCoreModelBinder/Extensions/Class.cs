using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace NetCoreModelBinder.Extensions
{
    /*
     * Serializing objects to URL encoded form data
     * https://gunnarpeipman.com/serialize-url-encoded-form-data/
     */
    /// <summary>
    /// Serializing objects to Dictionary
    /// </summary>
    public static class FormExtension
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="metaToken"></param>
        /// <returns></returns>
        public static IDictionary<string, string> ToKeyValue(this object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }

            // Added by me: avoid cyclic references
            var serializer = new JsonSerializer { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var token = metaToken as JToken;
            if (token == null)
            {
                // Modified by me: use serializer defined above
                return ToKeyValue(JObject.FromObject(metaToken, serializer));
            }

            if (token.HasValues)
            {
                var contentData = new Dictionary<string, string>();
                foreach (var child in token.Children().ToList())
                {
                    var childContent = child.ToKeyValue();
                    if (childContent != null)
                    {
                        contentData = contentData.Concat(childContent)
                                                 .ToDictionary(k => k.Key, v => v.Value);
                    }
                }

                return contentData;
            }

            var jValue = token as JValue;
            if (jValue?.Value == null)
            {
                return null;
            }

            var value = jValue?.Type == JTokenType.Date ?
                            jValue?.ToString("o", CultureInfo.InvariantCulture) :
                            jValue?.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> { { token.Path, value } };
        }

        /// <summary>
        /// Serializing object to form data
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static FormUrlEncodedContent ToFormData(this object obj)
        {
            var formData = obj.ToKeyValue();

            return new FormUrlEncodedContent(formData);
        }

        /// <summary>
        /// Refer: https://www.paddingleft.com/2018/04/11/HttpClient-PostAsync-GetAsync-Example/
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }
    }
}