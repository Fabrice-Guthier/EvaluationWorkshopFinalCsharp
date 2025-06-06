using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evaluation_Workshop_Final_CDA_C_.Data
{
    [Table("Animal")]
    public class Animal
    {
        [Key]
        public int AnimalId { get; set; }
        public string AnimalName { get; set; }
        public string AnimalDescription { get; set; }
        public int RaceId { get; set; }
        [ForeignKey("RaceId")]
        [ValidateNever]
        public virtual Race Race { get; set; }
    }
}