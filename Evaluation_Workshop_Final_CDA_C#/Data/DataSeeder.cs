using Microsoft.EntityFrameworkCore;

namespace Evaluation_Workshop_Final_CDA_C_.Data
{
    public class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Evaluation_Workshop_Final_CDA_C_Context(
                serviceProvider.GetRequiredService<DbContextOptions<Evaluation_Workshop_Final_CDA_C_Context>>()))
            {
                if (context.Race.Any())
                {
                    return;
                }

                var raceBete = new Race { RaceName = "Bête", RaceDescription = "Créature non-intelligente du monde sauvage." };
                var raceDragon = new Race { RaceName = "Dragon", RaceDescription = "Reptile ancien, puissant et souvent ailé." };
                var raceMagique = new Race { RaceName = "Créature Magique", RaceDescription = "Entités dont l'existence défie les lois de la nature." };
                var raceHumanoide = new Race { RaceName = "Humanoïde", RaceDescription = "Créature bipède avec une intelligence notable." };

                context.Race.AddRange(raceBete, raceDragon, raceMagique, raceHumanoide);

                var animaux = new Animal[]
                {
                    new Animal { AnimalName = "Warg", AnimalDescription = "Loup surdimensionné et anormalement intelligent.", Race = raceBete },
                    new Animal { AnimalName = "Griffon", AnimalDescription = "Créature à corps de lion et à tête et ailes d'aigle.", Race = raceMagique },
                    new Animal { AnimalName = "Basilic", AnimalDescription = "Serpent gigantesque dont le regard pétrifie.", Race = raceDragon },
                    new Animal { AnimalName = "Gobelin", AnimalDescription = "Petite créature verte, sournoise et chapardeuse.", Race = raceHumanoide },
                    new Animal { AnimalName = "Hydre", AnimalDescription = "Créature reptilienne à plusieurs têtes qui se régénèrent.", Race = raceDragon }
                };

                context.Animal.AddRange(animaux);

                context.SaveChanges();
            }
        }
    }
}
