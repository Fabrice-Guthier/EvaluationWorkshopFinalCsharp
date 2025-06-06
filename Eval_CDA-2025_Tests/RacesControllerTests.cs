using Evaluation_Workshop_Final_CDA_C_.Controllers;
using Evaluation_Workshop_Final_CDA_C_.Data;
using Evaluation_Workshop_Final_CDA_C_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using Assert = Xunit.Assert;
using Race = Evaluation_Workshop_Final_CDA_C_.Data.Race;

namespace Eval_CDA_2025_Tests
{
    public class RacesControllerTests
    {
        private readonly DbContextOptions<Evaluation_Workshop_Final_CDA_C_Context> _options; // Options pour la base de données en mémoire

        public RacesControllerTests() // Constructeur pour initialiser les options de la base de données
        {
            // On utilise une base de données en mémoire pour isoler chaque test
            _options = new DbContextOptionsBuilder<Evaluation_Workshop_Final_CDA_C_Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task IndexReturnsViewResultWithListOfRaces() // Test de la méthode Index
        {
            // Arrange : création du contexte et du contrôleur
            using var context = new Evaluation_Workshop_Final_CDA_C_Context(_options);
            var controller = new RacesController(context);

            // Act : appel de la méthode Index (liste des races)
            var result = await controller.Index();

            // Assert : vérifie que le résultat est une vue et que le modèle est une liste de Race
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsAssignableFrom<IEnumerable<Race>>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task CreatePostRedirectsToIndexWhenModelStateIsValid() // Test pour vérifier que la méthode Create redirige vers Index après une création réussie
        {
            // Arrange : création du contexte, du contrôleur et d'un nouveau modèle de race
            using var context = new Evaluation_Workshop_Final_CDA_C_Context(_options);
            var controller = new RacesController(context);
            var newRace = new CreateRaceViewModel
            {
                RaceName = "Test Race",
                RaceDescription = "Test Description"
            };

            // Act : appel de la méthode Create avec un modèle valide
            var result = await controller.Create(newRace);

            // Assert : vérifie que l'action redirige vers Index après la création
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task DetailsReturnsNotFoundWhenIdIsNull() // Test pour vérifier le comportement de Details avec un id null
        {
            // Arrange : création du contexte et du contrôleur
            using var context = new Evaluation_Workshop_Final_CDA_C_Context(_options);
            var controller = new RacesController(context);

            // Act : appel de la méthode Edit avec un id null
            var result = await controller.Details(null);

            // Assert : vérifie que le résultat est NotFound (404)
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditReturnsNotFoundWhenIdIsNull() // Test pour la méthode Edit
        {
            // Arrange
            using var context = new Evaluation_Workshop_Final_CDA_C_Context(_options);
            var controller = new RacesController(context);

            // Act : appel de la méthode Edit avec un id null
            var result = await controller.Edit(null);

            // Assert : vérifie que le résultat est NotFound (404)
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteReturnsNotFoundWhenIdIsNull() // Test pour la méthode Delete avec un id null
        {
            // Arrange : création du contexte et du contrôleur
            using var context = new Evaluation_Workshop_Final_CDA_C_Context(_options);
            var controller = new RacesController(context);

            // Act : appel de la méthode Delete avec un id null
            var result = await controller.Delete(null);

            // Assert : vérifie que le résultat est NotFound (404)
            Assert.IsType<NotFoundResult>(result);
        }
    }
}