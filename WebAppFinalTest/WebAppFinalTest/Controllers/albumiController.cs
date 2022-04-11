using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppFinalTest.Models;
using WebAppFinalTest.Models.DTO;
using WebAppFinalTest.Repository.Interfaces;

namespace WebAppFinalTest.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class albumiController : ControllerBase
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public albumiController(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAlbums()
        {
            var albums = _albumRepository.GetAll().ToList();
            return Ok(_mapper.Map<List<AlbumDTO>>(albums));
        }

        [HttpGet("{id}")]
        public IActionResult GetAlbum(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id must be > 0");
            }
            var album = _albumRepository.GetById(id);
            if (album == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AlbumDTO>(album));
        }
        [Route("/api/albumi/trazi")]
        [HttpGet]
        public IActionResult GetAlbumsByName([FromQuery] string ime)
        {
            if (ime == null)
            {
                return BadRequest();
            }

            var albums = _albumRepository.FilterByName(ime);
            return Ok(_mapper.Map<List<AlbumDTO>>(albums));
        }
        [Authorize]
        [HttpPost]
        public IActionResult PostAlbum(Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _albumRepository.Add(album);
            return CreatedAtAction("GetAlbum", new { id = album.Id }, album);
        }

        [HttpPut("{id}")]
        public IActionResult PutAlbum(int id, Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != album.Id)
            {
                return BadRequest();
            }

            try
            {
                _albumRepository.Update(album);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(album);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteAlbum(int id)
        {
            if (id <= 0)
            {
                return BadRequest("id must be > 0");
            }
            var album = _albumRepository.GetById(id);
            if (album == null)
            {
                return NotFound();
            }

            _albumRepository.Delete(album);
            return NoContent();
        }
        [Authorize]
        [Route("/api/pretraga")]
        [HttpPost]
        public IActionResult GetAlbumsByYear([FromBody] AlbumDTOSearch dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (dto.Min > dto.Max)
            {
                return Conflict("Min je veci od maksa");
            }
            var albums = _albumRepository.FilterByYear(dto.Min, dto.Max);
            if (albums == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<AlbumDTO>>(albums));
        }
    }
}
