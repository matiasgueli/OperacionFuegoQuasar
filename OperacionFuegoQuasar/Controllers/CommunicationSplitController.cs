using Microsoft.AspNetCore.Mvc;
using OperacionFuegoQuasar.Contracts;
using OperacionFuegoQuasar.Contracts.Entities;
using OperacionFuegoQuasar.Services;

namespace OperacionFuegoQuasar.Controllers
{
    [Route("topsecret_split")]
    [ApiController]
    public class CommunicationSplitController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody] SatelliteDataRequest request)
        {
            /*
             Antes de obtener el mensaje y posición se pueden agregar validaciones, como por ejemplo, si la información que llega es de un satélite,
                si el nombre del mismo es correcto, si tiene valor el parámetro distancia, etc.
            */

            //Obtener mensaje
            string message = MessageService.GetMessage(request.Satellites);

            if (message.Equals("No se pudo leer el mensaje."))
                return NotFound("No se pueda determinar la posición o el mensaje");
            else
                return Ok(new SatelliteDataResponse
                {
                    Distance = new Position
                    {
                        Longitude = Satellite.GetSatelliteData(request.Satellites[0].Name).Position.Longitude,
                        Latitude = Satellite.GetSatelliteData(request.Satellites[0].Name).Position.Latitude
                    }
                            ,
                    Message = message
                });
        }

        [HttpGet]
        public ActionResult Get([FromBody] SatelliteDataRequest request)
        {
            /*
             Antes de obtener el mensaje y posición se pueden agregar validaciones, como por ejemplo, si la información que llega es de un satélite,
                si el nombre del mismo es correcto, si tiene valor el parámetro distancia, etc.
            */

            //Obtener mensaje
            string message = MessageService.GetMessage(request.Satellites);

            if (message.Equals("No se pudo leer el mensaje."))
                return NotFound("No se pueda determinar la posición o el mensaje");
            else
                return Ok(new SatelliteDataResponse
                {
                    Distance = new Position
                    {
                        Longitude = Satellite.GetSatelliteData(request.Satellites[0].Name).Position.Longitude,
                        Latitude = Satellite.GetSatelliteData(request.Satellites[0].Name).Position.Latitude
                    }
                            ,
                    Message = message
                });
        }
    }
}
