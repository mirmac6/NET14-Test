using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppFinalTest.Repository.Interfaces;

namespace WebAppFinalTest.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class bendoviController : ControllerBase
    {
        private readonly IBandRepository _bandRepository;

        public bendoviController(IBandRepository bandRepository)
        {
            _bandRepository = bandRepository;
        }

        [HttpGet]
        public IActionResult GetBands()
        {

            return Ok(_bandRepository.GetAll().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetBand(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id must be > 0");
            }
            var band = _bandRepository.GetById(id);
            if (band == null)
            {
                return NotFound();
            }

            return Ok(band);
        }

        [Route("/api/prodaja")]
        [HttpGet]
        public IActionResult GetSoldAlbums([FromQuery] int granica)
        {
            if (granica < 0)
            {
                return BadRequest();
            }
            return Ok(_bandRepository.FilterBySoldAlbums(granica));
        }

        [Route("/api/brojnost")]
        [HttpGet]
        public IActionResult GetRealeseAlbums()
        {
            return Ok(_bandRepository.GetAllByAlbums());
        }

        [Route("/api/bendovi/trazi")]
        [HttpGet]
        public IActionResult GetByName([FromQuery] string naziv)
        {
            if (naziv == null)
            {
                return BadRequest();
            }
            return Ok(_bandRepository.FilterByName(naziv));
        }
    }
}
