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
    public class Cocktail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CocktailId { get; set; }

        public string CocktailName { get; set; }

        public int Price { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Brand Brand { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        public override string ToString()
        {
            return ("CocktailID: " + CocktailId + ", BrandID: " + BrandId + ", Cocktail name:  " + CocktailName + ", Price: " + Price);
        }
    }
}
