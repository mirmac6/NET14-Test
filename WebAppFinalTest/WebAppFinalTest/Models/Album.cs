using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFinalTest.Models
{
    public class Album
    {
        public int Id { get; set; }
        [MaxLength(80, ErrorMessage = "name must be between 3 and 80 characters(server validation)")]
        [MinLength(3, ErrorMessage = "name must be between 3 and 80 characters(server validation)")]
        public string Name { get; set; }
        [MaxLength(80, ErrorMessage = "name must be between 3 and 80 characters(server validation)")]
        [MinLength(3, ErrorMessage = "name must be between 3 and 80 characters(server validation)")]
        public string Genre { get; set; }
        [Range(1950, 2022, ErrorMessage = "Year must be between 1950 and 2022 (server validation)")]
        public int Year { get; set; }
        [Range(0, 100, ErrorMessage = "Price must be between 0 and 100 (server validation)")]
        public int Sold { get; set; }
        public int BandId { get; set; }
        // Navigation property
        public Band Band { get; set; }
    }
}
