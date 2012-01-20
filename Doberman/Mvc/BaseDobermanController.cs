using System;
using System.Web.Mvc;
using Doberman.Configuration;
using Doberman.Services;

namespace Doberman.Mvc
{
    public abstract class BaseDobermanController : Controller
    {
        public DobermanConfiguration Configuration { get; protected set; }

        public BaseDobermanController() 
            : this(new DobermanConfiguration(new ConfigurationProvider()))
        {

        }

        public BaseDobermanController(DobermanConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public void Index()
        {
            if (!Configuration.HasPagesToLoad)
                Configuration.AddPageLoad(Request.Url);

            var doberman = new Doberman();
            var result = doberman.Run(doberman.Fetch(Configuration));

            DobermanResponse.Output(System.Web.HttpContext.Current, result);
        }
    }
}
