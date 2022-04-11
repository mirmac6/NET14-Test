using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppFinalTest.Models;

namespace WebAppFinalTest.Repository.Interfaces
{
    public interface IAlbumRepository
    {
        IEnumerable<Album> GetAll();
        List<Album> FilterByName (string ime);
        List<Album> FilterByYear(int min, int max);
        Album GetById(int id);
        void Add(Album album);
        void Update(Album album);
        void Delete(Album album);
    }
}
