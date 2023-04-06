using BlazorBootstrap;
using CoderSlave.App.Pages;
using CoderSlave.Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace CoderSlave.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var services = new ServiceCollection();
            services.AddBlazorBootstrap(); // Add this line
            services.AddWindowsFormsBlazorWebView();
            var apikey = ConfigurationManager.AppSettings["ApiKey"];
            var orgid = ConfigurationManager.AppSettings["OrgId"];
            services.AddSingleton(new CodeGpt(apikey,orgid));
            services.AddTransient<CodeEvaluator>();

            blazorWebView1.HostPage = "wwwroot\\index.html";
            blazorWebView1.Services = services.BuildServiceProvider();
            blazorWebView1.RootComponents.Add<App>("#app");
            blazorWebView1.RootComponents.Add<HeadOutlet>("head::after");
        }
    }
}