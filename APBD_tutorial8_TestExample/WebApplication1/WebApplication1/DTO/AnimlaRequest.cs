using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public class AnimlaRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public DateTime AdmissionDate { get; set; }
        [Required]
        public int IdOwner { get; set; }
        public Procedure[] procedures { get; set; }
    }
}
