using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Doberman.Configuration;
using Doberman.Mvc;
using Doberman.Services;

namespace Doberman.Example.Mvc3.Controllers
{
    public class DobermanController : BaseDobermanController
    {
        public DobermanController() : base()
        {
            Configuration.CheckEmail("smtp.gmail.com", 465, true);
        }
    }
}
