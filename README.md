# Doberman

Doberman is a C# library capable of performing checks on your .NET website.

## What Checks can be done?

Currently Doberman is capable of performing the checks listed below.

* Page loading
* SQL Database Connection
* Mongo DB Connection
* Saving of file
* Email sending

## Getting Started

Firstly download the latest Doberman dll from the [downloads] (https://github.com/moov2/doberman/downloads) section on Github and then place the dll file into your .NET website. Currently Doberman is only catered for MVC & Web Forms.

### MVC

Create a new empty controller (recommend calling it 'DobermanController.cs') that should extend BaseDobermanController instead of the default Controller, so you have something resembling the code below.

  using System;
  using Doberman.Mvc;
  
  namespace VoucherGod.Controllers
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
  
  namespace Remoting
  {
      public partial class Doberman : DobermanPage 
      {
  
      }
  }

You should now at least get a page load check that is checking the root page of your website.


