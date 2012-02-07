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

        public IDobermanConfigurator Configuration { get; private set; }

        public DobermanPage()
        {
            Configuration = new DobermanConfiguration(new ConfigurationProvider());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Configuration.AddPageLoad(HttpContext.Current.Request.Url);

            var doberman = new Doberman();
            var result = doberman.Run(doberman.Fetch((DobermanConfiguration)Configuration));

            DobermanResponse.Output(Context, result);
        }
    }
}
