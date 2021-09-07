using System;
using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;

namespace apiLaboratorio.Controllers
{
    // POST: api/AltaTipoMedicamento
    [ApiController]
    [Route("api/[controller]")]
    public class AltaTipoMedicamento : ControllerBase
    {
        [HttpPost]
        public string Post(Request1 request)
        {
            string response="";
            
            try
            {
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new LaboratorioFarmaceutico.LaboratorioFarmaceuticoClient(channel);

                var nuevoTipo = new TipoMedicamento
                {
                    Tipo = request.tipo
                };

                response=cliente.AltaTipoMedicamento(nuevoTipo).Message;    
            }
            catch (Exception e)
            {
                response=e.Message + e.StackTrace;
            }
         
            return response;
        }
    }
     public class Request1
    {
        public string tipo { get; set; } 
    }

}
