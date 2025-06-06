using Evaluation_Workshop_Final_CDA_C_.Controllers;
using Evaluation_Workshop_Final_CDA_C_.Data;
using Evaluation_Workshop_Final_CDA_C_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using Assert = Xunit.Assert;
using Animal = Evaluation_Workshop_Final_CDA_C_.Data.Animal;

namespace Eval_CDA_2025_Tests
{
    public class AnimalsControllerTest
    {
        private readonly DbContextOptions<Evaluation_Workshop_Final_CDA_C_Context> _options; // Options pour la base de données en mémoire

        public AnimalsControllerTest() // Constructeur de la classe de test
        {
            // On utilise une base de données en mémoire pour isoler les tests
            _options = new DbContextOptionsBuilder<Evaluation_Workshop_Final_CDA_C_Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task IndexReturnsViewResultWithListOfAnimals() // Test de la méthode Index
        {
            // Arrange : création du contexte et du contrôleur
            using var context = new Evaluation_Workshop_Final_CDA_C_Context(_options);
            var controller = new AnimalsController(context);

            // Act : appel de la méthode Index sans filtre
            var result = await controller.Index(searchString: null, raceId: null);

            // Assert : vérifie que le résultat est une vue et que le modèle est une liste d'animaux
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsAssignableFrom<IEnumerable<Animal>>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task CreatePostRedirectsToIndexWhenModelStateIsValid() // Test pour vérifier que la méthode Create redirige vers Index après une création réussie
        {
            // Arrange : création du contexte, du contrôleur et d'un nouvel animal à ajouter
            using var context = new Evaluation_Workshop_Final_CDA_C_Context(_options);
            var controller = new AnimalsController(context);
            var newAnimal = new CreateAnimalViewModel
            {
                AnimalName = "Test Animal",
                AnimalDescription = "Test Description",
                RaceId = 1
            };

            // Act : appel de la méthode Create avec un modèle valide
            var result = await controller.Create(newAnimal);

            // Assert : vérifie que l'action redirige vers Index après création
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task DetailsReturnsNotFoundWhenIdIsNull() // Test pour vérifier le comportement de Details avec un id null
        {
            // Arrange : création du contexte et du contrôleur
            using var context = new Evaluation_Workshop_Final_CDA_C_Context(_options);
            var controller = new AnimalsController(context);

            // Act : appel de la méthode Details avec un id null
            var result = await controller.Details(null);

            // Assert : vérifie que le résultat est NotFound
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditReturnsNotFoundWhenIdIsNull() // Test pour la méthode Edit
        {
            // Arrange : création du contexte et du contrôleur
            using var context = new Evaluation_Workshop_Final_CDA_C_Context(_options);
            var controller = new AnimalsController(context);

            // Act : appel de la méthode Edit avec un id null
            var result = await controller.Edit(null);

            // Assert : vérifie que le résultat est NotFound
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteReturnsNotFoundWhenIdIsNull() // Test pour la méthode Delete avec un id null
        {
            // Arrange : création du contexte et du contrôleur
            using var context = new Evaluation_Workshop_Final_CDA_C_Context(_options);
            var controller = new AnimalsController(context);

            // Act : appel de la méthode Delete avec un id null
            var result = await controller.Delete(null);

            // Assert : vérifie que le résultat est NotFound
            Assert.IsType<NotFoundResult>(result);
        }
    }
}