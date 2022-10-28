namespace ExerciseApp.Models
{
    public class Workout
    {
        public int WorkoutID { get; set; }
        public string? WorkoutName { get; set; }
        public List<Exercise> WorkoutExercises { get; set; } = new List<Exercise>();
    }
}
