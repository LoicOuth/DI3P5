using System.Web.Http;
using USite_Templating.Presentation.Models.Dtos;
using USite_Templating.Services;

namespace USite_Templating.Presentation.Controllers
{
    public class TemplateController : ApiController
    {
        private readonly TemplateGenerate _templateGenerate;
        public TemplateController()
        {
            _templateGenerate = new TemplateGenerate();
        }

        [HttpPost]
        public string ci([FromBody] CiDto ciDto)
        {
            return _templateGenerate.Ci(ciDto.SiteName);
        }

        [HttpPost]
        public string nginx([FromBody] NginxDto nginxDto)
        {
            return _templateGenerate.NginxConf(nginxDto.DnsName, nginxDto.SiteName);
        }

        [HttpPost]
        public string ingress([FromBody] IngressDto ingressDto)
        {
            return _templateGenerate.Ingress(ingressDto.DnsName, ingressDto.SiteName);
        }

        [HttpPost]
        public string html([FromBody] HtmlDto htmlDto)
        {
            return _templateGenerate.Html(htmlDto.Page); 
        }

    }
}
