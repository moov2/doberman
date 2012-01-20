using System;
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
            response.Write("<!DOCTYPE html>\n\n");
            response.Write("<html>\n\n");
            response.Write("<head>\n");
            response.Write("\t<title>Doberman Status</title>\n\n");
            response.Write("\t<link href='http://fonts.googleapis.com/css?family=PT+Serif:400,700,400italic' rel='stylesheet' />\n");
            response.Write("\t<link href='/treats/css/styles.css' rel='Stylesheet' type='text/css' />\n\n");
            response.Write("</head>\n\n");
            response.Write("<body>\n\n");
            response.Write("\t<header>\n");
            response.Write("\t\t<img src='/treats/doberman.png' alt='Woof, Woof' />\n");
            response.Write("\t\t<h1>Doberman</h1>\n");
            response.Write("\t</header>\n\n");
            response.Write("\t<div class='clear'></div>\n\n");
            response.Write("\t<ul>\n");

            foreach (var result in results)
            {
                response.Write("\t\t<li>\n");
                
                if (result.Success) 
                    response.Write("\t\t\t<div class='success'>&nbsp;</div>\n");
                else 
                    response.Write("\t\t\t<div class='fail'>&nbsp;</div>\n");

                response.Write("\t\t\t<div class='detail'>\n");
                response.Write("\t\t\t\t<h1>" + result.Check + "</h1>\n");
                response.Write("\t\t\t\t<h2>" + result.Detail + "</h2>\n");
                response.Write("\t\t\t</div>\n\n");
                response.Write("\t\t\t<div class='clear'></div>\n");
                response.Write("\t\t</li>\n\n");
            }

            response.Write("\t</ul>\n\n");
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
