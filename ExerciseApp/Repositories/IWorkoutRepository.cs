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
    }
}
