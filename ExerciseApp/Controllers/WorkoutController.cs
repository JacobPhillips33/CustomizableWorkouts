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
            return View(workout);
        }

        public IActionResult InsertWorkoutToDatabase(Workout workout)
        {
            _workoutRepository.InsertWorkout(workout);
            _workoutRepository.CreateWorkout(workout);
            return RedirectToAction("Index");
        }

        public IActionResult ViewWorkout(int id)
        {
            var workout = _workoutRepository.GetWorkout(id);
            
            return View(workout);
        }

        public IActionResult AddExerciseToWorkout(Exercise exercise)
        {
            var workout = _workoutRepository.AddExerciseToWorkout(exercise);
            return RedirectToAction("ViewWorkoutExercises", new { id = exercise.WorkoutID });
        }

        public IActionResult ViewWorkoutExercises(int id)
        {
            var workoutTable = _workoutRepository.GetWorkoutTable(id);

            return View(workoutTable);
        }
    }
}
