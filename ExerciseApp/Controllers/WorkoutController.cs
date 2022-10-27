using ExerciseApp.Models;
using ExerciseApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseApp.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutController(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public IActionResult Index()
        {
            var workoutList = _workoutRepository.GetAllWorkouts();
            return View(workoutList);
        }
        
        public IActionResult CreateWorkout()
        {
            var workout = _workoutRepository.InstantianteWorkout();
            _workoutRepository.CreateWorkout();
            return View(workout);
        }

        public IActionResult InsertWorkoutToDatabase(Workout workout)
        {
            _workoutRepository.InsertWorkout(workout);
            return RedirectToAction("Index");
        }
    }
}
