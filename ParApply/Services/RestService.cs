using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace ParApply
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

        private void OnResponse(IAsyncResult responseResult)
        {
            
        }

        public abstract IEnumerable<T> ParseXml(XmlReader reader);

        private IEnumerable<T> ParseResult(WebResponse response)
        {                        
            var encoding = Encoding.GetEncoding("iso-8859-1");            
            using (var sr = new StreamReader(response.GetResponseStream(), encoding))
            using (var xmlReader = XmlReader.Create(sr))
            {
                return ParseXml(xmlReader);
            }
        }
    }
}