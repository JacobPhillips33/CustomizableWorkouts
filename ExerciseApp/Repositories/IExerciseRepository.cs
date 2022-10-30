using ExerciseApp.Models;

namespace ExerciseApp.Repositories
{
    public interface IExerciseRepository
    {
        public IEnumerable<Exercise> AllExercisesList();
        public Exercise SpecificExercise(int id);
        public IEnumerable<Workout> GetWorkoutList();
        public IEnumerable<Exercise> AllFavoriteExercisesList();
        public void AddExerciseToFavorites(Exercise exerciseToAdd);
        public void RemoveExerciseFromFavorites(ExerciseGroup favoriteToRemove);
    }
}
