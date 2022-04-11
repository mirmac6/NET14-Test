using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFinalTest.Models
{
    public class Band
    {
        public int Id { get; set; }
        [MaxLength(200, ErrorMessage = "{0} can have a max of {1} characters (server validation)")]
        public string Name { get; set; }
        [Range(1900, 2022, ErrorMessage = "Year must beetween 1900 and 2022 (server validation)")]
        public int Year { get; set; }
    }
}
