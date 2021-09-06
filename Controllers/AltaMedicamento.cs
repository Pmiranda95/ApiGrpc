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
    // POST: api/AltaMedicamento
    [ApiController]
    [Route("api/[controller]")]
    public class AltaMedicamento : ControllerBase
    {
        [HttpPost]
        public string Post(string codAlfabetico, int codNumerico, int digito, string nombre, string droga, int tipo)
        {
            string response="";
            
            try
            {
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new LaboratorioFarmaceutico.LaboratorioFarmaceuticoClient(channel);

                var medicamento = new Medicamento
                {
                    CodigoAlfabetico = codAlfabetico,
                    CodigoNumerico = codNumerico,
                    DigitoVerificador = digito,
                    Nombre = nombre,
                    Droga = droga,
                    TipoMedicamento = tipo
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

}
