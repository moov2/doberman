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
        private const int TimeOut = 1000 * 180; // 3 minutes

        public string PageUrl { get; private set; }

        public PageLoadsCheck(string pageUrl)
        {
            PageUrl = pageUrl;
        }

        public DobermanResult Execute()
        {
            var result = new DobermanResult { Check = CheckName };

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(PageUrl));
                request.Timeout = TimeOut;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // TODO: I think there maybe more work required here to ensure that the home page is functioning as expected.
                result.Success = (response.StatusCode == HttpStatusCode.OK);

                if (result.Success)
                    result.Detail = PageUrl + " is loading just fine.";

                response.Close();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Detail = ex.Message;
            }

            return result;
        }
    }
}
