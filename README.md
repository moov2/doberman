# Doberman

Doberman is a C# library capable of performing checks on your .NET website.

## What Checks can be done?

Currently Doberman is capable of performing the checks listed below.

* Page Loading
* SQL Database Connecting
* Mongo DB Connecting
* File Saving
* Email Sending

## Getting Started

Firstly download the latest Doberman dll from the [downloads] (https://github.com/moov2/doberman/downloads) section on Github and then place the dll file into your .NET website. Currently Doberman is only catered for MVC & Web Forms.

### MVC

Create a new empty controller (recommend calling it 'DobermanController.cs') that should extend BaseDobermanController instead of the default Controller, so you have something resembling the code below.

    using System;
    using Doberman.Mvc;
  
    namespace Example.Controllers
    {
        public class DobermanController : BaseDobermanController
        {
    
        }
    }

You should now at least get a page load check that is checking the root page of your website.

### Web Forms

Create a new web form (recommend calling it 'Doberman.aspx'). You can delete all the HTML that is created and just be left with the code below.

    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Doberman.aspx.cs" Inherits="ExampleWebsite.Doberman" %>

Then if you go into the code behind file and make your class extend 'DobermanPage' instead of 'System.Web.UI.Page' you should hopefully be left with something looking like the code below.

    using System;
    using Doberman.WebForms;
    
    namespace Example
    {
        public partial class Doberman : DobermanPage 
        {
    
        }
    }

You should now at least get a page load check that is checking the root page of your website.

## The Checks

Doberman is capable of performing the variety of tests that are explained below. Some of the checks are performed magically by examining the Web.config for the website, however you can specify your own in the class that extends the Doberman MVC or Webforms class. The BaseDobermanController and DobermanPage have a Configuration property that has methods to allow you to customise your check.

### Page Loading

Check if a web page loads, if it loads then the test passes, otherwise it is deemed a failure. Doberman will always automatically check the root page of the website when it runs the checks. However, you can specify as many custom pages as you like using the code shown below.

#### MVC


    public class DobermanController : BaseDobermanController
    {
        public DobermanController()
            : base()
        {
            Configuration.AddPageLoad("http://my-website.com/My/Custom/Page");
        }
    }

#### Web Forms

    public partial class Doberman : DobermanPage 
    {
        public Doberman()
            : base()
        {
            Configuration.AddPageLoad("http://my-website.com/My/Custom/Page");
        }
    }

### SQL Database Connecting

Check if we can establish a connection to a SQL database, if we can then the check passes otherwise it is determined as a failure. Doberman will automatically try and find a SQL connection string in your Web.config that has the name that is equal to your computer name, like below.

    <configuration>
        <connectionStrings>
            <add name="MY-PC"
                 connectionString="Data Source=.\SQLEXPRESS;Integrated Security=True;database=MyDatabase"
                 providerName="System.Data.SqlClient" />
        </connectionStrings>
    </configuration>

Alternatively you can specify your own in the constructor of your DobermanController or DobermanPage using the Configuration object shown below.

#### MVC

    public class DobermanController : BaseDobermanController
    {
        public DobermanController() : base()
        {
            Configuration.AddSqlConnectionString(@"Data Source=.\SQLEXPRESS;Integrated Security=True;database=MyDatabase");
        }
    }

#### Web Forms

    public partial class Doberman : DobermanPage 
    {
        public Doberman()
            : base()
        {
            Configuration.AddSqlConnectionString(@"Data Source=.\SQLEXPRESS;Integrated Security=True;database=MyDatabase");
        }
    }
    
### Mongo DB Connecting

Checks to see if a connection can be established with a Mongo server, if one can be established then the check is passed, otherwise it fails. Doberman will automatically run this check if there is an appSetting with the key of 'MongoServer' in the Web.config, like below.

    <configuration>
        <appSettings>
            <add key="MongoServer" value="mongodb://localhost/" />
        </appSettings>
    </configuration>

Alternatively you can specify your own in the constructor of your DobermanController or DobermanPage as shown below.

#### MVC

    public class DobermanController : BaseDobermanController
    {
        public DobermanController() : base()
        {
            Configuration.AddMongoConnectionString("mongodb://localhost");
        }
    }

#### Web Forms

    public partial class Doberman : DobermanPage 
    {
        public Doberman()
            : base()
        {
           Configuration.AddMongoConnectionString("mongodb://localhost");
        }
    }
    
#### Notice

In order to run the Mongo connection check your website must reference the [official Mongo DB C# Driver] (https://github.com/mongodb/mongo-csharp-driver). Currently uses version **1.3.1.4349**.

### File Saving

Checks to see if a file can be saved to a directory, perfect for changing you got your file permissions working properly. If the file can be saved then the check is deemed a pass, if it can't be saved then the check is a failure. File saving is the only check that requires some additional code to get it to work. Using the Configuration object you can supply as many directories as you want to save to using the _AddDirectorySave_ method, as shown below.

#### MVC
    
    public class DobermanController : BaseDobermanController
    {
        public DobermanController() : base()
        {
            Configuration.AddDirectorySave("/My/Upload/Directory/");
        }
    }
    
#### Web Forms

    public partial class Doberman : DobermanPage 
    {
        public Doberman()
            : base()
        {
           Configuration.AddDirectorySave("/My/Upload/Directory/");
        }
    }
    
### Email Sending

Checks to see if an email can be sent, if the email can be sent then the check is passed, otherwise it is a failure. Doberman will check the _mailSettings_ in the Web.config, if they are present then all that is required is a from email address & to email address then the check can be run. Without both of these the email check won't run.

Here is what should be in the Web.config of your website.

    <configuration>
        <system.net>
            <mailSettings>
                <smtp>
                    <network host="localhost" port="25" />
                </smtp>
            </mailSettings>
        </system.net>
    </configuration>
    
Then the from & to address should be given to the configuration as shown below.

#### MVC

    public class DobermanController : BaseDobermanController
    {
        public DobermanController() : base()
        {
            Configuration.EnableEmailCheck("from.email@address.com", "to.email@address.com");
        }
    }

#### Web Forms

    public partial class Doberman : DobermanPage 
    {
        public DobermanController() : base()
        {
            Configuration.EnableEmailCheck("from.email@address.com", "to.email@address.com");
        }
    }