using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFinalTest.Models.DTO
{
    public class BandDTO
    {
  
        public string Name { get; set; }

        public int Year { get; set; }

        public int SoldAlbums { get; set; }

    }
}
