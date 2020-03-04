using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.Models.ViewModels
{
    public class RegisterResponseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public RegisterResponseViewModel(AppUser user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
        }
    }
}
