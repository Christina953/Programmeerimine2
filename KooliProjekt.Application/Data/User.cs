using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(256)]        
        public string Email { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        [Required,MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Required, MaxLength(75)]
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }
        
        // Võimalik edasiarendus: kasutaja püsiallahindlus protsentides
        // public decimal DiscountPercent { get; set; }

    }
}
