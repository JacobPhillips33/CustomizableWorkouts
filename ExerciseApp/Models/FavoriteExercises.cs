namespace ExerciseApp.Models
{
    public class FavoriteExercises
    {
        public int ExerciseID { get; set; }
        public IEnumerable<Exercise> FavoriteExercisesList { get; set; }
    }
}
