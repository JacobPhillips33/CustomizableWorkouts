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
        public void AddExerciseToWorkout(Exercise exercise);
        public string GetTableName(Workout workout);
        public IEnumerable<Exercise> GetWorkoutExercises(Workout workout);
        public Workout AssignExercisesToWorkout(Workout workout);
        public void UpdateWorkoutExercise(Workout workout);
        public void RemoveExerciseFromWorkout(Workout workout);
    }
}
