using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Doberman.Configuration;
using System.Web;
using Doberman.Services;

namespace Doberman.WebForms
{
    public class DobermanPage : Page
    {
        public Repeater DobermanResultsRepeater;

        public DobermanConfiguration Configuration { get; private set; }

        public DobermanPage()
            : this(new DobermanConfiguration(new ConfigurationProvider()))
        {
        }

        public DobermanPage(DobermanConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Configuration.HasPagesToLoad)
                Configuration.AddPageLoad(HttpContext.Current.Request.Url);

            var doberman = new Doberman();
            var result = doberman.Run(doberman.Fetch(Configuration));

            DobermanResponse.Output(Context, result);
        }
    }
}
