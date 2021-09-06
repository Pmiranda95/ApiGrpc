using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Grpc.Core;
using Grpc.Net.Client;

namespace apiLaboratorio.Controllers
{
    // GET: api/TraerAerosoles
    [ApiController]
    [Route("api/[controller]")]
    public class TraerAerosoles : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            string response="";

            try
            {
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new LaboratorioFarmaceutico.LaboratorioFarmaceuticoClient(channel);
                cliente.Listo(new Nulo());
                var nulo = new Nulo();
                
                response=cliente.TraerAerosoles(nulo).Message;    
            }
            catch (Exception e)
            {
                response=e.Message + e.StackTrace;
            }
         
            return response;
        }

    }

}
