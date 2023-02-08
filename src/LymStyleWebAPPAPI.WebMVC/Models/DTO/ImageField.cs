using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LymStyleWebAPPAPI.Web.Models.DTO
{
    public class ImageField
    {
        public int id { get; set; }

        [DataType(DataType.Upload)]
        [DisplayName("Upload Image")]
        public string? image{ get; set; }
    }
}
