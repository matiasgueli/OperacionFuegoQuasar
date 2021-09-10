using Microsoft.AspNetCore.Mvc;
using OperacionFuegoQuasar.Contracts;
using OperacionFuegoQuasar.Services;

namespace OperacionFuegoQuasar.Controllers
{
    [Route("topsecret")]
    [ApiController]
    public class CommunicationController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody] SatelliteDataRequest request)
        {
            /*
             Antes de obtener el mensaje y posición se pueden agregar validaciones, como por ejemplo, si la información que llega son de los 3 satélites,
                si los nombres de los mismos son correctos, si tiene valor el parámetro distancia, etc.
            */

            //Obtener mensaje
            string message = MessageService.GetMessage(request.Satellites);

            //Obtener posición
            double[] location = LocationService.GetLocation(request.Satellites);

            if (message.Equals("No se pudo leer el mensaje.") || location == null)
                return NotFound("No se pueda determinar la posición o el mensaje");
            else
                return Ok(new SatelliteDataResponse
                {
                    Distance = new Position
                    {
                        Longitude = ((float)location[0]),
                        Latitude = ((float)location[1])
                    },
                    Message = message
                });
        }
    }
}
