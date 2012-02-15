using System;
using System.Collections.Generic;
using Doberman.Model;
using System.Net;
using System.Web;

namespace Doberman.Checks
{
    public class PageLoadsCheck : ICheck
    {
        private const string CheckName = "Page Loads";
        private const int TimeOut = 20000;

        private string _originalPageUrl;

        public string PageUrl { get; private set; }

        /// <summary>
        /// Gets the URL to the root of the site.
        /// </summary>
        private string SiteRootUrl
        {
            get { 
                var url = HttpContext.Current.Request.Url;
                return url.Scheme + "://" + url.Authority;
            }
        }

        public PageLoadsCheck(string pageUrl)
        {
            _originalPageUrl = pageUrl;

            if (NeedsSiteRoot(_originalPageUrl))
            {
                PageUrl = SiteRootUrl;
                PageUrl += (_originalPageUrl.StartsWith("/")) ? _originalPageUrl : "/" + _originalPageUrl; 
            }
            else
                PageUrl = _originalPageUrl;
        }

        public DobermanResult Execute()
        {
            var result = new DobermanResult { Check = CheckName };

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(PageUrl));
                request.Timeout = TimeOut;
                request.KeepAlive = false;
                request.Headers.Add("Cache-Control", "no-cache");
                request.Headers.Add("Pragma", "no-cache");

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // TODO: I think there maybe more work required here to ensure that the home page is functioning as expected.
                result.Success = (response.StatusCode == HttpStatusCode.OK);

                if (result.Success)
                    result.Detail = PageUrl + " is loading just fine.";

                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Detail = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// Indicates whether the given url requires the site root url.
        /// </summary>
        /// <param name="url">URL to be verified.</param>
        /// <returns>True if requires root site url, otherwise false.</returns>
        private static bool NeedsSiteRoot(string url)
        {
            return !(url.StartsWith("http://") || url.StartsWith("https://"));
        }
    }
}
