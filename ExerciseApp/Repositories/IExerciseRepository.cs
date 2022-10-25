using ExerciseApp.Models;

namespace ExerciseApp.Repositories
{
    public interface IExerciseRepository
    {
        public IEnumerable<Exercise> AllExercisesList();
        public Exercise SpecificExercise(int exerciseNumber);
    }
}
