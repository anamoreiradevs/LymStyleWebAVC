using LymStyleWebAPPAPI.Domain.DTO;

namespace LymStyleWebAPPAPI.WebMVC.Models
{
    public class AdmViewModel
    {
        public IEnumerable<ProductDTO> product { get; set; }
        public IEnumerable<CategoryDTO> category { get; set; }
        public IEnumerable<AccountDTO> user { get; set; }
    }
}
