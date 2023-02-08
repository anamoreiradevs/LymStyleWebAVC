using LymStyleWebAPPAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LymStyleWebAPPAPI.Domain.DTO
{
    public class CategoryDTO
    {
        [Display(Name = "Id")]
        public int id { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        public virtual ICollection<ProductDTO>? productList { get; set; }
        public Category mapToEntity()
        {
            return new Category
            {
                Id = this.id,
                Name = this.name,
            };
        }
        public CategoryDTO maptoDTO(Category category)
        {
            return new CategoryDTO
            {
                id = category.Id,
                name = category.Name,
            };
        }
    }
}
 