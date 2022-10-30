namespace ExerciseApp.Models
{
    public class WorkoutGroup
    {
        public int WorkoutID { get; set; }
        public string? WorkoutName { get; set; }
        public IEnumerable<Workout>? WorkoutsList { get; set; }
    }
}
