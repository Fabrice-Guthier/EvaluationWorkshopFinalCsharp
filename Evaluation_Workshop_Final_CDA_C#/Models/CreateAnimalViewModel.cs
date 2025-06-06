using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Evaluation_Workshop_Final_CDA_C_.Models
{
    public class CreateAnimalViewModel
    {
        [Required(ErrorMessage = "Le nom est obligatoire !")]
        [StringLength(50)]
        public string AnimalName { get; set; }
        public string AnimalDescription { get; set; }

        [Display(Name = "Race")]
        [Required(ErrorMessage = "Vous devez sélectionner une race.")]
        public int RaceId { get; set; } // Pour recevoir l'ID de la race sélectionnée

        // Propriété pour contenir la liste des options du menu déroulant
        public SelectList? AvailableRaces { get; set; }
    }
}