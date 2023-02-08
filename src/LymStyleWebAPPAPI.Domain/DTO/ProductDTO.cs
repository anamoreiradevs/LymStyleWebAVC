using LymStyleWebAPPAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LymStyleWebAPPAPI.Domain.DTO
{
    public class ProductDTO
    {
        [Display(Name = "Id")]
        public int id { get; set; }
        [Display(Name ="Category")]
        public int categoryId { get; set; }
       
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Price")]
        public double price { get; set; }
        public virtual CategoryDTO? category { get; set; }
        public string? image { get; set; }

        public Product mapToEntity()
        {
            return new Product()
            {
                Id = id,
                CategoryId = categoryId,
                Name = name,
                Description = description,
                Price = price,
                Image = image,
            };
        }
        public ProductDTO mapToDTO(Product product)
        {
            return new ProductDTO()
            {
                id = product.Id,
                categoryId = product.CategoryId,
                name = product.Name,
                description = product.Description,
                price = product.Price,
                image = product.Image,
            };
        }
    }
}
