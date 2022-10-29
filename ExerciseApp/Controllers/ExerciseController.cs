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
            ViewData["ExerciseIDSortParam"] = sortOrder == "ExerciseID" ? "exerciseID_desc" : "";
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name";
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
                case "exerciseID_desc": exerciseList = exerciseList.OrderByDescending(x => x.ExerciseID); break;
                case "name": exerciseList = exerciseList.OrderBy(x => x.Name); break;
                case "name_desc": exerciseList = exerciseList.OrderByDescending(x => x.Name); break;
                case "bodyPart": exerciseList = exerciseList.OrderBy(x => x.BodyPart); break;
                case "bodyPart_desc": exerciseList = exerciseList.OrderByDescending(x => x.BodyPart); break;
                case "targetMuscle": exerciseList = exerciseList.OrderBy(x => x.TargetMuscle); break;
                case "targetMuscle_desc": exerciseList = exerciseList.OrderByDescending(x => x.TargetMuscle); break;
                case "equipment": exerciseList = exerciseList.OrderBy(x => x.Equipment); break;
                case "equipment_desc": exerciseList = exerciseList.OrderByDescending(x => x.Equipment); break;
                default: exerciseList = exerciseList.OrderBy(x => x.ExerciseID); break;
            }

            return View(exerciseList);
        }

        //public IActionResult Index()
        //{
        //    var exerciseList = _exerciseRepository.AllExercisesList();
        //    return View(exerciseList);
        //}
        #region Views OrderedBy Ascending and Descending by Column
        public IActionResult ViewByIDDesc()
        {
            var sortedList = _exerciseRepository.AllExercisesList().OrderByDescending(x => x.ExerciseID).ToList();
            return View(sortedList);
        }
        public IActionResult ViewByNameDesc()
        {
            var sortedList = _exerciseRepository.AllExercisesList().OrderByDescending(x => x.Name).ToList();
            return View(sortedList);
        }
        public IActionResult ViewByBodyPartAsc()
        {
            var sortedList = _exerciseRepository.AllExercisesList().OrderBy(x => x.BodyPart).ToList();  
            return View(sortedList);
        }
        public IActionResult ViewByBodyPartDesc()
        {
            var sortedList = _exerciseRepository.AllExercisesList().OrderByDescending(x => x.BodyPart).ToList();
            return View(sortedList);
        }
        public IActionResult ViewByTargetMuscleAsc()
        {
            var sortedList = _exerciseRepository.AllExercisesList().OrderBy(x => x.TargetMuscle).ToList();
            return View(sortedList);
        }
        public IActionResult ViewByTargetMuscleDesc()
        {
            var sortedList = _exerciseRepository.AllExercisesList().OrderByDescending(x => x.TargetMuscle).ToList();
            return View(sortedList);
        }
        public IActionResult ViewByEquipmentAsc()
        {
            var sortedList = _exerciseRepository.AllExercisesList().OrderBy(x => x.Equipment).ToList();
            return View(sortedList);
        }
        public IActionResult ViewByEquipmentDesc()
        {
            var sortedList = _exerciseRepository.AllExercisesList().OrderByDescending(x => x.Equipment).ToList();
            return View(sortedList);
        }
        #endregion Views OrderedBy Ascending and Descending by Column
        public IActionResult ViewExercise(int id)
        {
            var exercise = _exerciseRepository.SpecificExercise(id);
            return View(exercise);
        }
        public IActionResult ViewSelectByProperty()
        {
            var exercise = _exerciseRepository.AssignProperties();
            return View(exercise);
        }

        public IActionResult ViewBodyPartExerciseList(Exercise exercise)
        {
            var exerciseList = _exerciseRepository.ExercisesByBodyPart(exercise);
            return View(exerciseList);            
        }
        public IActionResult ViewTargetMuscleExerciseList(Exercise exercise)
        {
            var exerciseList = _exerciseRepository.ExercisesByTargetMuscle(exercise);
            return View(exerciseList);
        }
        public IActionResult ViewEquipmentExerciseList(Exercise exercise)
        {
            var exerciseList = _exerciseRepository.ExercisesByEquipmentNeeded(exercise);
            return View(exerciseList);
        }
        public IActionResult ViewFavoriteExercisesList()
        {
            var favoriteExercisesList = _exerciseRepository.AllFavoriteExercisesList();
            return View(favoriteExercisesList);
        }
        public IActionResult AddExerciseToFavoritesList(Exercise exerciseToAdd)
        {
            _exerciseRepository.AddExerciseToFavorites(exerciseToAdd);
            return RedirectToAction("ViewFavoriteExercisesList");
        }
        public IActionResult RemoveExerciseFromFavorites(Exercise exerciseToRemove)
        {
            _exerciseRepository.RemoveExerciseFromFavorites(exerciseToRemove);
            return RedirectToAction("ViewFavoriteExercisesList");
        }
    }
}
