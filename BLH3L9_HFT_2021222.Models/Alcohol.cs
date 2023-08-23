using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BLH3L9_HFT_2021222.Models
{
    [Table("Alcohols")]
    public class Alcohol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlcoholId { get; set; }

        public string AlcoholName { get; set; }

        public string Grain { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Brand> Brands { get; set; }

        public Alcohol()
        {
            Brands = new HashSet<Brand>();
        }

        public override string ToString()
        {
            return ("AlcoholID: " + AlcoholId + ", Alcohol type: " + AlcoholName + ", Grain type: " + Grain);
        }
    }
}
