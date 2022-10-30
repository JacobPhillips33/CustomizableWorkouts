namespace ExerciseApp.Models
{
    public class Workout
    {
        public int WorkoutID { get; set; }
        public string? WorkoutName { get; set; }
        public IEnumerable<Exercise> WorkoutExercises { get; set; } = Enumerable.Empty<Exercise>();
        public int ExerciseID { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
    }
}
