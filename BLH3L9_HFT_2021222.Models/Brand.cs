using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLH3L9_HFT_2021222.Models
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public int Percentage { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Alcohol Alcohol { get; set; }

        [NotMapped]
        public virtual ICollection<Cocktail> Cocktails { get; set; }

        public Brand()
        {
            Cocktails = new HashSet<Cocktail>();
        }

        [ForeignKey(nameof(Alcohol))]
        public int AlcoholId { get; set; }

        public override string ToString()
        {
            return ("BrandID: " + BrandId + ", AlcoholID: " + AlcoholId + ", Brand name: " + BrandName + ", Percentage: " + Percentage + "%");
        }
    }
}
