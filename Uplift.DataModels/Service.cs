using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uplift.DataModels
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Serivce Name")]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Display(Name = "Description")]
        public string LongDesc { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public int FrequencyId { get; set; }

        [ForeignKey("FrequencyId")]
        public Frequency Frequency { get; set; }
        //[Key]
        //public int Id { get; set; }
        //[Required]
        //[DisplayName("Name")]
        //public string Name { get; set; }
        //[Required]
        //[DisplayName("Price")]
        //public double Price { get; set; }
        //[Required]
        //[DisplayName("Description")]
        //public string LongDesc { get; set; }
        //[Required]
        //[DataType(DataType.ImageUrl)]
        //[DisplayName("Image")]
        //public string ImageUrl { get; set; }

        //[Required]
        //public int CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        //public Category Category { get; set; }

        //[Required]
        //public int FrequencyId { get; set; }
        //[ForeignKey("FrequencyId")]
        //public Frequency Frequency { get; set; }
    }
}
