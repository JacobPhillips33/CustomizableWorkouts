using ExerciseApp.Models;
using System.Data;

namespace ExerciseApp.Repositories
{
    public interface IWorkoutRepository
    {
        public IEnumerable<Workout> GetAllWorkouts();
        public void InsertWorkout(Workout workout);
        public Workout InstantianteWorkout();
        public void CreateWorkout(Workout workout);
        public Workout GetWorkout(int id);
        //public Workout AddExerciseToWorkout(Exercise exercise);
        public Workout GetWorkoutTable(int id);
        public string GetTableName(Workout workout);
        public IEnumerable<Exercise> GetWorkoutExercises(Workout workout);
        public Workout AssignExercisesToWorkout(Workout workout);
    }
}
