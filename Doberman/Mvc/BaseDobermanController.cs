using System;
using System.Web.Mvc;
using Doberman.Configuration;
using Doberman.Services;

namespace Doberman.Mvc
{
    public abstract class BaseDobermanController : Controller
    {
        public IDobermanConfigurator Configuration { get; protected set; }

        public BaseDobermanController()
        {
            Configuration = new DobermanConfiguration(new ConfigurationProvider());
        }

        [HttpGet]
        public void Index()
        {
            Configuration.AddPageLoad(Request.Url);

            var doberman = new Doberman();
            var result = doberman.Run(doberman.Fetch((DobermanConfiguration)Configuration));

            DobermanResponse.Output(System.Web.HttpContext.Current, result);
        }
    }
}
