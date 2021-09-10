using System.Collections.Generic;

namespace OperacionFuegoQuasar.Contracts
{
    public class SatelliteDataRequest
    {
        /// <summary>
        /// Satélites
        /// </summary>
        public List<SatelliteData> Satellites { get; set; }
    }

    public class SatelliteData
    {
        /// <summary>
        /// Nombre del satélite
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Distancia del satélite con la nave
        /// </summary>
        public float Distance { get; set; }
        /// <summary>
        /// Mensaje que envía el satélite
        /// </summary>
        public string[] Message { get; set; }
    }
}
