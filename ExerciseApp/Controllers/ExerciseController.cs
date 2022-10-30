using ExerciseApp.Models;
using ExerciseApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseApp.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseController(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["BodyPartSortParam"] = String.IsNullOrEmpty(sortOrder) ? "bodyPart_desc" : "bodyPart";
            ViewData["TargetMuscleSortParam"] = String.IsNullOrEmpty(sortOrder) ? "targetMuscle_desc" : "targetMuscle";
            ViewData["EquipmentSortParam"] = String.IsNullOrEmpty(sortOrder) ? "equipment_desc" : "equipment";

            ViewData["CurrentFilter"] = searchString;

            var exerciseList = _exerciseRepository.AllExercisesList();

            if (!String.IsNullOrEmpty(searchString))
            {
                exerciseList = exerciseList.Where(x => x.Name.ToLower().Contains(searchString.ToLower()) || 
                                                       x.BodyPart.ToLower().Contains(searchString.ToLower()) || 
                                                       x.TargetMuscle.ToLower().Contains(searchString.ToLower()) || 
                                                       x.Equipment.ToLower().Contains(searchString.ToLower()));
            }

            switch (sortOrder)
            {
                case "name_desc": exerciseList = exerciseList.OrderByDescending(x => x.Name); break;
                case "bodyPart": exerciseList = exerciseList.OrderBy(x => x.BodyPart); break;
                case "bodyPart_desc": exerciseList = exerciseList.OrderByDescending(x => x.BodyPart); break;
                case "targetMuscle": exerciseList = exerciseList.OrderBy(x => x.TargetMuscle); break;
                case "targetMuscle_desc": exerciseList = exerciseList.OrderByDescending(x => x.TargetMuscle); break;
                case "equipment": exerciseList = exerciseList.OrderBy(x => x.Equipment); break;
                case "equipment_desc": exerciseList = exerciseList.OrderByDescending(x => x.Equipment); break;
                default: exerciseList = exerciseList.OrderBy(x => x.Name); break;
            }

            var allExercisesGroup = new ExerciseGroup()
            {
                ExercisesList = exerciseList
            };
            return View(allExercisesGroup);
        }

        public IActionResult ViewExercise(int id)
        {
            var exercise = _exerciseRepository.SpecificExercise(id);
            return View(exercise);
        }
        public IActionResult FavoriteExercisesList(string searchString)
        {
            ViewData["CurrentFilter1"] = searchString;

            var favoriteExercisesList = _exerciseRepository.AllFavoriteExercisesList();

            if (!String.IsNullOrEmpty(searchString))
            {
                favoriteExercisesList = favoriteExercisesList.Where(x => x.Name.ToLower().Contains(searchString.ToLower()) ||
                                                                         x.BodyPart.ToLower().Contains(searchString.ToLower()) ||
                                                                         x.TargetMuscle.ToLower().Contains(searchString.ToLower()) ||
                                                                         x.Equipment.ToLower().Contains(searchString.ToLower()));
            }


            var favoriteExercises = new ExerciseGroup()
            {
                ExercisesList = favoriteExercisesList
            };

            return View(favoriteExercises);
        }

        public IActionResult AddExerciseToFavoritesList(Exercise exerciseToAdd)
        {
            _exerciseRepository.AddExerciseToFavorites(exerciseToAdd);
            return RedirectToAction("FavoriteExercisesList");
        }

        public IActionResult RemoveExerciseFromFavorites(ExerciseGroup favoriteToRemove)
        {
            _exerciseRepository.RemoveExerciseFromFavorites(favoriteToRemove);
            return RedirectToAction("FavoriteExercisesList");
        }
    }
}
