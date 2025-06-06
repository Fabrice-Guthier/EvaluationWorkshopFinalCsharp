using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evaluation_Workshop_Final_CDA_C_.Data
{
    [Table("Race")]
    public class Race
    {
        [Key]
        public int RaceId { get; set; }
        public string RaceName { get; set; }
        public string RaceDescription { get; set; }
        [ValidateNever]
        public virtual ICollection<Animal> Animals { get; set; }
    }
}
