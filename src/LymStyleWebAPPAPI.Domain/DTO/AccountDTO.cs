using LymStyleWebAPPAPI.Domain.Entities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LymStyleWebAPPAPI.Domain.DTO
{
    public class AccountDTO
    {
        public int id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Full name")]
        public string name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "Email Invalid")]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Image")]
        public string? image { get; set; }

        public Account mapToEntity()
        {
            return new Account
            {
                Id = id,
                Name = name,
                Email = email,
                Password = password,
                Image = image
            };
        }
        public AccountDTO mapToDTO(Account user)
        {
            return new AccountDTO
            {
                id = user.Id,
                name = user.Name,
                email=user.Email,
                password = user.Password,
                image = user.Image,
            };
        }
    }
}
