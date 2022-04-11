using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppFinalTest.Models;
using WebAppFinalTest.Models.DTO;

namespace WebAppFinalTest.Repository.Interfaces
{
    public interface IBandRepository
    {
        IEnumerable<Band> GetAll();
        Band GetById(int id);

        List<Band> FilterByName(string naziv);

        List<BandDTO> FilterBySoldAlbums(int granica);
        List<BandDTOStat> GetAllByAlbums();

    }
}
