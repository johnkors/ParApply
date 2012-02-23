using System;
using System.Net;

namespace ParApply.Business
{
    public class WebRequestFactory : IWebRequestFactory
    {
        public HttpWebRequest CreateHttpWebRequest(Uri uri)
        {
            return (HttpWebRequest)WebRequest.Create(uri);
        }
    }
}