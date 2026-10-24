using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    [Index(nameof(Name), IsUnique = true)]
    public class CarType
    {
        public int Id { get; set; }
        
        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
