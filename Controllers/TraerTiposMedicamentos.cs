using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using Newtonsoft.Json;

namespace apiLaboratorio.Controllers
{
    // GET: api/TraerTiposMedicamentos
    [ApiController]
    [Route("api/[controller]")]
    public class TraerTiposMedicamentos : ControllerBase
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
                
                response=cliente.TraerTiposMedicamentos(nulo).Message;  

                // Logica para romper el string
                char[] charsToTrim = { '[', ']' };
                response=response.Trim(charsToTrim);
                string id, tipoMedicamento, activo;
                List<TipoMedicamento> tipos = new List<TipoMedicamento>();

                while(response.Length>1){
                    response=response.Remove(0, 1);
                    id=response.Substring(0, response.IndexOf(","));
                    response=response.Remove(0, response.IndexOf(",")+2);
                    tipoMedicamento=response.Substring(0, response.IndexOf(","));
                    tipoMedicamento=tipoMedicamento.Trim('\'');
                    response=response.Remove(0, response.IndexOf(",")+2);
                    activo=response.Substring(0, response.IndexOf(")"));
                    response=response.Remove(0, response.IndexOf(",")+2);

                    var objTipoMedicamento = new TipoMedicamento
                    {
                        Tipo = tipoMedicamento
                    };

                    tipos.Add(objTipoMedicamento);
                }
                response = JsonConvert.SerializeObject(tipos);
            }
            catch (Exception e)
            {
                response=e.Message + e.StackTrace;
            }
         
            return response;
        }

    }

}
