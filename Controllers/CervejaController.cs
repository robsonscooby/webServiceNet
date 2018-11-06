using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using webServiceNet.Models;
using webServiceNet.Repository;

namespace webServiceNet.Controllers
{
    [Route("api/[Controller]")]
    public class CervejaController : Controller
    {
        private readonly ICervejaRepository _cervejaRepository;
        public CervejaController(ICervejaRepository cervejaRepo)
        {
            _cervejaRepository = cervejaRepo;
        }

        [HttpGet]
        public IEnumerable<Cerveja> GetAll()
        {
            return _cervejaRepository.GetAll();
        }

        [HttpGet("{id}",Name="GetCerveja")]
        public IActionResult GetById(long id)
        {
            var cerveja = _cervejaRepository.Find(id);
            if(cerveja == null)
            {
                return NotFound();
            }
            return new ObjectResult(cerveja);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Cerveja cerveja)
        {
            if(cerveja == null)
            {
                return BadRequest();
            }
            _cervejaRepository.Add(cerveja);
            return CreatedAtRoute("GetCerveja", new Cerveja{Id=cerveja.Id},cerveja);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Cerveja cerveja)
        {
            if(cerveja == null || cerveja.Id != id)
            {
                return BadRequest();
            }

            var cerv = _cervejaRepository.Find(id);
            if(cerv == null)
            {
                return NotFound();
            }
            cerv.Marca = cerveja.Marca;
            cerv.Tipo = cerveja.Tipo;
            cerv.Codigo = cerveja.Codigo;

            _cervejaRepository.Update(cerv);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var cerveja = _cervejaRepository.Find(id);
            if(cerveja == null)
            {
                return NotFound();
            }

            _cervejaRepository.Remove(id);
            return new NoContentResult();
        }
    }
}