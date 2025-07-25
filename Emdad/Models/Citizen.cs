﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emdad.Models
{
    public class Citizen : BaseEntity
    {
        public int CitizenId { get; set; }
        [Required]
        public string CitizenNationalId { get; set; }
        [Required]
        [EmailAddress]
        public string CitizenEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string CitizenPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Not Matche")]
        public string CitizenConfirmPassword { get; set; }
        
    }
}
