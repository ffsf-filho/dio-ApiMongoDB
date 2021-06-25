using dio_ApiMongoDB.Data.Collections;
using dio_ApiMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace dio_ApiMongoDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _InfectadosCollection;
        public InfectadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _InfectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }
        
        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.DataNacimento, dto.Sexo, dto.Latitude, dto.Longitude);
            _InfectadosCollection.InsertOne(infectado);
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _InfectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            return Ok(infectados);
        }

        [HttpPut]
        public ActionResult AtualizarInfectados([FromBody] InfectadoDto dto)
        {
            _InfectadosCollection.UpdateOne(Builders<Infectado>.Filter
            .Where(_ => _.DataNascimento == dto.DataNacimento), 
            Builders<Infectado>.Update.Set("sexo", dto.Sexo));
            
            return Ok("Atualizado com sucesso");
        }
        [HttpDelete]
        public ActionResult DeleteInfectados([FromBody] InfectadoDto dto)
        {
            _InfectadosCollection.DeleteOne(Builders<Infectado>.Filter
            .Where(_ => _.DataNascimento == dto.DataNacimento));
            
            return Ok("Apagado com sucesso");
        }        
    }
}