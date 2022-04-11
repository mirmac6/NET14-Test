using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppFinalTest.Models;
using WebAppFinalTest.Repository.Interfaces;

namespace WebAppFinalTest.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AppDbContext _context;

        public AlbumRepository(AppDbContext context)
        {
            this._context = context;
        }

        public void Add(Album album)
        {
            _context.Albums.Add(album);
            _context.SaveChanges();
        }

        public void Delete(Album album)
        {
            _context.Albums.Remove(album);
            _context.SaveChanges();
        }

        public List<Album> FilterByName(string ime)
        {
            List<Album> albumi = _context.Albums.Include(e => e.Band).AsQueryable().Where(e=>e.Name.Equals(ime)).OrderByDescending(e => e.Sold).ToList();
            return albumi;
        }

        public List<Album> FilterByYear(int min, int max)
        {
            List<Album> albumi = _context.Albums.Include(e => e.Band).AsQueryable().Where(e => e.Year >= min).Where(e => e.Year <= max).OrderByDescending(e => e.Year).ToList();
            return albumi;
        }

        public IEnumerable<Album> GetAll()
        {
            return _context.Albums.Include(e => e.Band).AsQueryable().OrderBy(e => e.Name);
        }

        public Album GetById(int id)
        {
            return _context.Albums.Include(e => e.Band).FirstOrDefault(e => e.Id == id);
        }

        public void Update(Album album)
        {
            _context.Entry(album).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
