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
            var workoutGroup = _workoutRepository.GetWorkoutGroup();
            return View(workoutGroup);
        }
        
        public IActionResult CreateWorkout()
        {
            var workout = _workoutRepository.InstantianteWorkout();            
            return View(workout);
        }

        public IActionResult InsertWorkoutToDatabase(Workout workout)
        {
            if (workout.WorkoutName == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _workoutRepository.InsertWorkout(workout);
                _workoutRepository.CreateWorkout(workout);
                return RedirectToAction("Index");
            }            
        }

        public IActionResult ViewWorkout(int id)
        {
            var workout = _workoutRepository.GetWorkout(id);
            workout = _workoutRepository.AssignExercisesToWorkout(workout);
            
            return View(workout);
        }

        public IActionResult AddExerciseToWorkout(Exercise exercise)
        {
            if (exercise.WorkoutID == 0)
            {
                return RedirectToAction("ViewExercise", "Exercise", new { id = exercise.ExerciseID });
            }
            else
            {
                _workoutRepository.AddExerciseToWorkout(exercise);
                return RedirectToAction("ViewWorkout", new { id = exercise.WorkoutID });
            }            
        }

        public IActionResult UpdateRemoveWorkoutExercise(string cmd, Workout workout)
        {
            if (cmd == "Remove")
            {
                _workoutRepository.RemoveExerciseFromWorkout(workout);
                return RedirectToAction("ViewWorkout", new { id = workout.WorkoutID });
            }
            else
            {
                _workoutRepository.UpdateWorkoutExercise(workout);
                return RedirectToAction("ViewWorkout", new { id = workout.WorkoutID });
            }
        }

        public IActionResult UpdateDeleteWorkout(string cmd, WorkoutGroup workoutGroup)
        {
            if (cmd == "Delete")
            {
                _workoutRepository.DeleteWorkout(workoutGroup);
                return RedirectToAction("Index");
            }
            else
            {
                _workoutRepository.UpdateWorkout(workoutGroup);
                return RedirectToAction("Index");
            }
        }
    }
}
