using ExerciseApp.Models;

namespace ExerciseApp.Repositories
{
    public interface IExerciseRepository
    {
        public IEnumerable<Exercise> AllExercisesList(string sortBy = "");
        public Exercise SpecificExercise(int exerciseNumber);
        public IEnumerable<Exercise> BodyPartExerciseList(string bodyPart);
    }
}
