using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Grpc.Core;
using Grpc.Net.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

                // Logica para romper el string
                char[] charsToTrim = { '[', ']' };
                response=response.Trim(charsToTrim);
                string id, codAlfabetico, codNumerico, digito, nombre, droga, tipo;
                List<Medicamento> medicamentos = new List<Medicamento>();

                while(response.Length>1){
                    response=response.Remove(0, 1);
                    id=response.Substring(0, response.IndexOf(","));
                    response=response.Remove(0, response.IndexOf(",")+2);
                    codAlfabetico=response.Substring(0, response.IndexOf(","));
                    codAlfabetico=codAlfabetico.Trim('\'');
                    response=response.Remove(0, response.IndexOf(",")+2);
                    codNumerico=response.Substring(0, response.IndexOf(","));
                    response=response.Remove(0, response.IndexOf(",")+2);
                    digito=response.Substring(0, response.IndexOf(","));
                    response=response.Remove(0, response.IndexOf(",")+2);
                    nombre=response.Substring(0, response.IndexOf(","));
                    nombre=nombre.Trim('\'');
                    response=response.Remove(0, response.IndexOf(",")+2);
                    droga=response.Substring(0, response.IndexOf(","));
                    droga=droga.Trim('\'');
                    response=response.Remove(0, response.IndexOf(",")+2);
                    tipo=response.Substring(0, response.IndexOf(")"));
                    response=response.Remove(0, response.IndexOf(",")+2);

                    int intCodNumerico = Int32.Parse(codNumerico);
                    int intDigito = Int32.Parse(digito);
                    int intTipo = Int32.Parse(tipo);

                    var medicamento = new Medicamento
                    {
                        CodigoAlfabetico = codAlfabetico,
                        CodigoNumerico = intCodNumerico,
                        DigitoVerificador = intDigito,
                        Nombre = nombre,
                        Droga = droga,
                        TipoMedicamento = intTipo,
                    };

                    medicamentos.Add(medicamento);
                }
                response = JsonConvert.SerializeObject(medicamentos);
            }
            catch (Exception e)
            {
                response=e.Message + e.StackTrace;
            }
         
            return response;
        }

    }

}
