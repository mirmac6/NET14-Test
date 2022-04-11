using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFinalTest.Models.DTO
{
    public class AlbumDTOSearch
    {
        [Range(1950, 2022, ErrorMessage = "Year must be between 1950 and 2022 (server validation)")]
        public int Min { get; set; }
        [Range(1950, 2022, ErrorMessage = "Year must be between 1950 and 2022 (server validation)")]
        public int Max { get; set; }
    }
}
