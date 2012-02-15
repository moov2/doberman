using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using Doberman.Model;
using System.Web.Script.Serialization; 

namespace Doberman
{
    public class DobermanResponse
    {
        public static void Html(HttpResponse response, IList<DobermanResult> results)
        {
            var failCount = results.Where(x => x.Success == false).ToList().Count;
            var rootSireUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority;

            response.Write("<!DOCTYPE html>\n\n");
            response.Write("<html>\n\n");
            response.Write("<head>\n");
            response.Write("\t<title>Doberman Status</title>\n\n");
            response.Write("\t<style type='text/css'>\n");
            response.Write("\tbody { color: #626262; font-size: 1em; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; margin: 0; padding; 0; width: 100%; }\n");
            response.Write("\theader { background-color: #535353; color: #f3f3f3; border-bottom: 1px solid #858585; height: 45px; line-height: 45px; margin: 0; padding; 0; }\n");
            response.Write("\theader a { color: #f3f3f3; text-decoration: none; }\n");
            response.Write("\theader a:hover { color: #f3f3f3; text-decoration: none; }\n");
            response.Write("\theader a:visited { color: #f3f3f3; text-decoration: none; }\n");
            response.Write("\theader a:link { color: #f3f3f3; text-decoration: none; }\n");
            response.Write("\theader a { color: #f3f3f3; }\n");
            response.Write("\theader h1 { cursor: pointer; float: left; font-size: 1em; margin: 0 0 0 50px; }\n");
            response.Write("\theader h2 { float: right; font-size: 1em; margin: 0 50px 0 0; }\n");
            response.Write("\t#header-status { color: #fff; font-size: 4.5em; font-weight: 400; height: 160px; line-height: 160px; padding: 0 0 0 50px; }\n");
            response.Write("\t#header-status.ok { background-color: #00be29; }\n");
            response.Write("\t#header-status.not-ok { background-color: #d60e0e; }\n");
            response.Write("\t#content { margin: 50px 50px 20px 50px; }\n");
            response.Write("\t#content h2 { font-size: 1em; font-weight: normal; }\n");
            response.Write("\tul { list-style-type: none; margin: 25px 0 0 0; padding: 0; font-size: 0.85em; }\n");
            response.Write("\tul li { margin-bottom: 10px; }\n");
            response.Write("\tul li .success { background-color: #00be29; display: inline-block; height: 10px; margin: 4px 10px 0 0; width: 10px; }\n");
            response.Write("\tul li .fail { background-color: #d60e0e; display: inline-block; height: 10px; margin: 4px 10px 0 0; width: 10px; }\n");
            response.Write("\tul li .detail { display: inline-block; }\n");
            response.Write("\t</style>\n\n");
            response.Write("</head>\n\n");
            response.Write("<body>\n\n");
            response.Write("\t<header>\n");
            response.Write("\t\t<h1><a href='http://github.com/moov2/doberman'>Doberman</a></h1>\n");
            response.Write("\t\t<h2>" + Doberman.Version + "</h2>\n");
            response.Write("\t</header>\n\n");

            if (failCount == 0)
            {
                response.Write("\t\t<div id='header-status' class='ok'>\n");
                response.Write("\t\t\tEverything is okay!\n");
                response.Write("\t\t</div>\n");
            }
            else
            {
                response.Write("\t\t<div id='header-status' class='not-ok'>\n");
                response.Write("\t\t\tSomething is wrong!\n");
                response.Write("\t\t</div>\n");
            }

            response.Write("\t\t<div id='content'>\n");

            if (failCount == 0)
                response.Write("\t\t\t<h2>Everything is okay with <strong>" + rootSireUrl + "</strong>, all " + results.Count + " checks were successful.\n\n");
            else
                response.Write("\t\t\t<h2>Unfortunately <strong>" + failCount + "</strong> out of the <strong>" + results.Count + "</strong> checks have failed for <strong>" + rootSireUrl + "</strong>, details are shown below.\n\n");
  
            response.Write("\t\t\t<ul>\n");

            foreach (var result in results)
            {
                response.Write("\t\t\t\t<li>\n");
                
                if (result.Success) 
                    response.Write("\t\t\t\t<div class='success'>&nbsp;</div>\n");
                else 
                    response.Write("\t\t\t\t<div class='fail'>&nbsp;</div>\n");

                response.Write("\t\t\t\t\t<div class='detail'>\n");
                response.Write("\t\t\t\t\t\t" + result.Check + " - <em>" + result.Detail + "</em>\n");
                response.Write("\t\t\t\t\t</div>\n\n");
                response.Write("\t\t\t\t</li>\n\n");
            }

            response.Write("\t\t\t</ul>\n\n");
            response.Write("\t\t</div>\n");
            response.Write("</body>\n");
            response.Write("</html>\n");
        }

        public static void Json(HttpResponse response, IList<DobermanResult> results)
        {
            var json = new JavaScriptSerializer().Serialize(results);

            response.Clear();
            response.ContentType = "application/json";
            response.Write(json);
        }

        public static void Output(HttpContext context, IList<DobermanResult> results)
        {
            if (String.IsNullOrEmpty(context.Request.QueryString["o"]))
            {
                Html(context.Response, results);
                return;
            }

            if (context.Request.QueryString["o"].ToLower() == "json")
                Json(context.Response, results);
            else if (context.Request.QueryString["o"].ToLower() == "xml")
                XML(context.Response, results);
            else
                Html(context.Response, results);
        }

        public static void XML(HttpResponse response, IList<DobermanResult> results)
        {
            response.Clear();
            response.ContentType = "text/xml";

            response.Write("<?xml version=\"1.0\"?>");
            response.Write("<doberman>");

            foreach (var result in results)
            {
                response.Write("<result>");
                response.Write("<check>" + result.Check + "</check>");
                response.Write("<detail>" + result.Detail + "</detail>");
                response.Write("<success>" + result.Success + "</success>");
                response.Write("</result>");
            }

            response.Write("</doberman>");
        }
    }
}
