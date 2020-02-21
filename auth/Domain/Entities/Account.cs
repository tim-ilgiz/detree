using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Account
    {
        [Key] public Guid UserGuid { get; set; }

        public string Username { get; set; }
        public string EncryptedPassword { get; set; }

        [Required] [StringLength(20)] public string Phone { get; set; }
    }
}