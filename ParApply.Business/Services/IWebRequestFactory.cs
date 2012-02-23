using System;
using System.Net;

namespace ParApply.Business
{
    public interface IWebRequestFactory
    {
        HttpWebRequest CreateHttpWebRequest(Uri uri);
    }
}