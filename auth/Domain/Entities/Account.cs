using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Account
    {
        [Key]
        public Guid UserGuid { get; set; }

        public string Username { get; set; }
        public string EncryptedPassword { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }
    }
}
