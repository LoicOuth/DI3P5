using Microsoft.VisualStudio.TextTemplating;
using System.Configuration;
using USite_Templating.Services.Dtos;
using USite_Templating.Services.T4;

namespace USite_Templating.Services
{
    public class TemplateGenerate
    {
        public string NginxConf(string dnsName, string siteName)
        {
            var confTemplate = new NginxConf();
            confTemplate.Session = new TextTemplatingSession();
            confTemplate.Session["SiteName"] = siteName;
            confTemplate.Session["DnsName"] = dnsName;
            confTemplate.Initialize();

            return confTemplate.TransformText();
        }

        public string Ci(string siteName)
        {
            var ciTemplate = new PipelineYml();
            ciTemplate.Session = new TextTemplatingSession();
            ciTemplate.Session["SiteName"] = siteName;
            ciTemplate.Session["IsDev"] = ConfigurationManager.AppSettings["IsDev"] != "false";
            ciTemplate.Initialize();

            return ciTemplate.TransformText();
        }

        public string Ingress(string dnsName, string siteName)
        {
            var ingressTemplate = new IngressYml();
            ingressTemplate.Session = new TextTemplatingSession();
            ingressTemplate.Session["SiteName"] = siteName;
            ingressTemplate.Session["DnsName"] = dnsName;
            ingressTemplate.Session["IsDev"] = ConfigurationManager.AppSettings["IsDev"] != "false";
            ingressTemplate.Initialize();

            return ingressTemplate.TransformText();
        }

        public string Html(PageDto page)
        {
            var htmlTemplate = new TemplateHtml();
            htmlTemplate.Session = new TextTemplatingSession();
            htmlTemplate.Session["Page"] = page;
            htmlTemplate.Initialize();

            return htmlTemplate.TransformText();
        }

    }
}
