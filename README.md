# Doberman

Doberman is a C# library capable of performing checks on your .NET website.

## What Checks can be done?

Currently Doberman is capable of performing the checks listed below.

* Page Loading
* SQL Database Connecting
* Mongo DB Connecting
* File Saving
* Email Sending
* File Exists (v.0.2)

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

Doberman is capable of performing the variety of tests that are explained below. Some of the checks are performed magically by examining the Web.config for the website, however you can specify your own in the class that extends the Doberman MVC or Webforms class. The BaseDobermanController and DobermanPage have a Configuration property that has methods to allow you to customise your check. When calling the methods on the _Configuration_ object you are not overriding any previous calls, Doberman just adds multiple checks, so for example calling _CheckPageLoad_ multiple times will add multiple page load checks, they do not overwrite each other.

### Page Loading

Check if a web page loads, if it loads then the test passes, otherwise it is deemed a failure. Doberman will always automatically check the root page of the website when it runs the checks. However, you can specify as many custom pages as you like using the code shown below.

#### MVC


    public class DobermanController : BaseDobermanController
    {
        public DobermanController()
            : base()
        {
            Configuration.CheckPageLoad("http://my-website.com/My/Custom/Page");
            Configuration.CheckPageLoad("My/Custom/Page");
            Configuration.CheckPageLoad("/My/Custom/Page");
        }
    }

#### Web Forms

    public partial class Doberman : DobermanPage 
    {
        public Doberman()
            : base()
        {
            Configuration.CheckPageLoad("http://my-website.com/My/Custom/Page.aspx");
            Configuration.CheckPageLoad("My/Custom/Page.aspx");
            Configuration.CheckPageLoad("/My/Custom/Page.aspx");
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
            Configuration.CheckSql(@"Data Source=.\SQLEXPRESS;Integrated Security=True;database=MyDatabase");
        }
    }

#### Web Forms

    public partial class Doberman : DobermanPage 
    {
        public Doberman()
            : base()
        {
            Configuration.CheckSql(@"Data Source=.\SQLEXPRESS;Integrated Security=True;database=MyDatabase");
        }
    }
    
### Mongo DB Connecting

Checks to see if a connection can be established with a Mongo server, if one can be established then the check is passed, otherwise it fails. Doberman will automatically run this check if there is an appSetting with the key of '**MongoServer**' in the Web.config, like below.

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
            Configuration.CheckMongo("mongodb://localhost");
        }
    }

#### Web Forms

    public partial class Doberman : DobermanPage 
    {
        public Doberman()
            : base()
        {
           Configuration.CheckMongo("mongodb://localhost");
        }
    }
    
#### Notice

In order to run the Mongo connection check your website must reference the [official Mongo DB C# Driver] (https://github.com/mongodb/mongo-csharp-driver). Currently uses version **1.3.1.4349**.

### File Saving

Checks to see if a file can be saved to a directory, perfect for changing you got your file permissions working properly. If the file can be saved then the check is deemed a pass, if it can't be saved then the check is a failure. File saving is the only check that requires some additional code to get it to work. Using the Configuration object you can supply as many directories as you want to save to using the _CheckFileSave_ method, as shown below.

#### MVC
    
    public class DobermanController : BaseDobermanController
    {
        public DobermanController() : base()
        {
            Configuration.CheckFileSave("/My/Upload/Directory/");
        }
    }
    
#### Web Forms

    public partial class Doberman : DobermanPage 
    {
        public Doberman()
            : base()
        {
           Configuration.CheckFileSave("/My/Upload/Directory/");
        }
    }
    
### Email Sending

Checks to see if an email can be sent, if a connection can be established and a ready response is given from the SMTP server then the check is passed, otherwise it is deemed a failure. Doberman will check the _mailSettings_ in the Web.config, if they are present then a check is automatically performed.

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
    
Alternatively, just like the other checks you can add an email check in the constructor of your Controller / Page. Provide the network host and the port to the _CheckEmail_ method and a check will be added. You can also provide an additional property to specify that the SMTP uses SSL.

#### MVC

    public class DobermanController : BaseDobermanController
    {
        public DobermanController() : base()
        {
            Configuration.CheckEmail("localhost", 25, true);
        }
    }

#### Web Forms

    public partial class Doberman : DobermanPage 
    {
        public DobermanController() : base()
        {
            Configuration.CheckEmail("localhost", 25, true);
        }
    }

### File Exists

Checks to see if a directory of file exists in the project, if the path given does exist as a directory or file then the check is passed, otherwise it is deemed a failure. Examples are shown below of the different ways to enter the path for this check. Please ensure that when entering relative paths don't start the path with any slashes.

#### MVC

    public class DobermanController : BaseDobermanController
    {
        public DobermanController() : base()
        {
            Configuration.CheckFileExists(@"my\file.exe");
            Configuration.CheckFileExists(@"my\directory\");
            Configuration.CheckFileExists(@"C:\full\path\to\my\directory\");
        }
    }

#### Web Forms

    public partial class Doberman : DobermanPage 
    {
        public DobermanController() : base()
        {
            Configuration.CheckFileExists(@"my\file.exe");
            Configuration.CheckFileExists(@"my\directory\");
            Configuration.CheckFileExists(@"C:\full\path\to\my\directory\");
        }
    }