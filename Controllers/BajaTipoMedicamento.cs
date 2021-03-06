using System;
using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;

namespace apiLaboratorio.Controllers
{
    // DELETE: api/BajaTipoMedicamento
    [ApiController]
    [Route("api/[controller]")]
    public class BajaTipoMedicamento : ControllerBase
    {
        [HttpDelete]
        public string Delete(string tipo)
        {
            string response="";
            
            try
            {
                var channel = GrpcChannel.ForAddress("http://localhost:50051");
                var cliente = new LaboratorioFarmaceutico.LaboratorioFarmaceuticoClient(channel);

                var nuevoTipo = new TipoMedicamento
                {
                    Tipo = tipo
                };

                response=cliente.BajaTipoMedicamento(nuevoTipo).Message;    
            }
            catch (Exception e)
            {
                response=e.Message + e.StackTrace;
            }
         
            return response;
        }
    }

}
