using System.ComponentModel.DataAnnotations;

namespace Evaluation_Workshop_Final_CDA_C_.Models
{
    public class CreateRaceViewModel
    {
        [Required(ErrorMessage = "Le nom de la race est obligatoire.")]
        [StringLength(100)]
        [Display(Name = "Nom de la race")]
        public string RaceName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string RaceDescription { get; set; }
    }
}
