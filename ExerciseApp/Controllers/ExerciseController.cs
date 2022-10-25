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

        public IActionResult Index()
        {
            var exerciseList = _exerciseRepository.AllExercisesList();

            return View(exerciseList);
        }

        public IActionResult ViewExercise(int id)
        {
            var exercise = _exerciseRepository.SpecificExercise(id);

            return View(exercise);
        }

        public IActionResult ViewBodyPartExerciseList(string bodyPart)
        {
            var bodyPartExerciseList = _exerciseRepository.BodyPartExerciseList(bodyPart);

            return View(bodyPartExerciseList);
        }
    }
}
