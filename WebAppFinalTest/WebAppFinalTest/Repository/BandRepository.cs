using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppFinalTest.Models;
using WebAppFinalTest.Models.DTO;
using WebAppFinalTest.Repository.Interfaces;

namespace WebAppFinalTest.Repository
{
    public class BandRepository : IBandRepository
    {
        private readonly AppDbContext _context;

        public BandRepository(AppDbContext context)
        {
            this._context = context;
        }
        public List<Band> FilterByName(string naziv)
        {
            List<Band> bendovi = _context.Bands.Where(e => e.Name.Contains(naziv)).OrderByDescending(e => e.Year).ThenByDescending(e => e.Name).ToList();
            return bendovi;
        }

        public List<BandDTO> FilterBySoldAlbums(int granica)
        {
            List<BandDTO> bands = _context.Albums.Include(e => e.Band).GroupBy(e => e.BandId).Select(sel => new BandDTO
            {
                Name = _context.Bands.Where(ci => ci.Id == sel.Key).Select(ci => ci.Name).Single(),
                Year = _context.Bands.Where(ci => ci.Id == sel.Key).Select(ci => ci.Year).Single(),
                SoldAlbums = _context.Albums.Where(e => e.BandId == sel.Key).Select(ci => ci.Sold).Sum(),
            }).Where(e => e.SoldAlbums > granica).OrderByDescending(e => e.Name).ToList();
            return bands;
        }

        public IEnumerable<Band> GetAll()
        {
            return _context.Bands.AsQueryable().OrderBy(e => e.Name);
        }

        public List<BandDTOStat> GetAllByAlbums()
        {
            List<BandDTOStat> galeris = _context.Albums.Include(e => e.Band).GroupBy(e => e.BandId).Select(sel => new BandDTOStat
            {
                Name = _context.Bands.Where(ci => ci.Id == sel.Key).Select(ci => ci.Name).Single(),
                Year = _context.Bands.Where(ci => ci.Id == sel.Key).Select(ci => ci.Year).Single(),
                NumAlbums = _context.Albums.Where(e => e.BandId == sel.Key).Count(),
            }).OrderByDescending(e => e.NumAlbums).ToList();
            return galeris;
        }

        public Band GetById(int id)
        {
            return _context.Bands.FirstOrDefault(e => e.Id == id);
        }
    }
}
