using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace ParApply.Business
{
    public abstract class RestService<T> where T : class
    {
        protected void Get(Uri uri, Action<Result<IEnumerable<T>>> callback)
        {
            var webRequest = (HttpWebRequest) WebRequest.Create(uri); 
            

            webRequest.BeginGetResponse(delegate(IAsyncResult responseResult)
                                            {
                                                try
                                                {
                                                    var response = webRequest.EndGetResponse(responseResult);
                                                    if (response != null)
                                                    {
                                                        var result = ParseResult(response);
                                                        response.Close();
                                                        callback(new Result<IEnumerable<T>>(result));
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    callback(new Result<IEnumerable<T>>(ex));
                                                }
                                            }, webRequest);
        }

        public abstract IEnumerable<T> ParseXml(XmlReader reader);

        private IEnumerable<T> ParseResult(WebResponse response)
        {                                   
            using (var sr = new StreamReader(response.GetResponseStream()))
            using (var xmlReader = XmlReader.Create(sr))
            {
                return ParseXml(xmlReader);
            }
        }
    }
}