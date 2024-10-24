using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecoporto.Agendamento.Portal.Controllers
{
    public class DefaultController : Controller
    {
        public class ResponseJson
        {
            public ResponseJson() { }
            public string statusRetorno { get; set; }

            public bool possuiDados { get; set; }

            public object objetoRetorno { get; set; }

            public string Mensagem { get; set; }
        }

        public ResponseJson retornoJson = new ResponseJson();
    }
}