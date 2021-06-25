using System;

namespace dio_ApiMongoDB.Models
{
    public class InfectadoDto
    {
        public DateTime DataNacimento { get; set; }
        public string Sexo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}