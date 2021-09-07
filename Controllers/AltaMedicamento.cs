using System;
using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;

namespace apiLaboratorio.Controllers
{
    // POST: api/AltaMedicamento
    [ApiController]
    [Route("api/[controller]")]
    public class AltaMedicamento : ControllerBase
    {
        [HttpPost]
        public string Post(Request request)
        {
            string response="";
            
            try
            {
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new LaboratorioFarmaceutico.LaboratorioFarmaceuticoClient(channel);

                var medicamento = new Medicamento
                {
                    CodigoAlfabetico = request.codAlfabetico,
                    CodigoNumerico = request.codNumerico,
                    DigitoVerificador = 0,
                    Nombre = request.nombre,
                    Droga = request.droga,
                    TipoMedicamento = request.tipo
                };
 
                response=cliente.AltaMedicamento(medicamento).Message;    
            }
            catch (Exception e)
            {
                response=e.Message + e.StackTrace;
            }
         
            return response;
        }
    }

    public class Request
    {
        public string codAlfabetico { get; set; } 
        public int codNumerico { get; set; } 
        public string nombre { get; set; } 
        public string droga { get; set; } 
        public int tipo { get; set; } 
    }
}
