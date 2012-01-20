using System;
using Doberman.Configuration;
using Doberman.WebForms;
using Doberman.Services;

namespace Doberman.Example.WebForms
{
    public partial class Status : DobermanPage
    {
        public Status() : base()
        {
            Configuration.AddDirectorySave("Uploads");
        }
    }
}