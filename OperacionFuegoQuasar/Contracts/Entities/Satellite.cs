using System.Collections.Generic;

namespace OperacionFuegoQuasar.Contracts.Entities
{
    public class Satellite
    {
        public string Name { get; set; }
        public Position Position { get; set; }

        public static Satellite GetSatelliteData(string name)
        {
            if (name.ToLower().Equals("kenobi"))
                return SatelliteKenobi();
            else if (name.ToLower().Equals("skywalker"))
                return SatelliteSkywalker();
            else if (name.ToLower().Equals("sato"))
                return SatelliteSato();
            else
                return new Satellite { Name = null, Position = new Position { Longitude = 0, Latitude = 0 } };
        }

        private static Satellite SatelliteKenobi()
        {
            return new Satellite { Name = "Kenobi", Position = new Position { Longitude = -500, Latitude = -200 } };
        }

        private static Satellite SatelliteSkywalker()
        {
            return new Satellite { Name = "Skywalker", Position = new Position { Longitude = 100, Latitude = -100 } };
        }

        private static Satellite SatelliteSato()
        {
            return new Satellite { Name = "Sato", Position = new Position { Longitude = 500, Latitude = 100 } };
        }
    }
}
