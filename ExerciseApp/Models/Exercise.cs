namespace ExerciseApp.Models
{
    public class Exercise
    {
        public string? BodyPart { get; set; }
        public string? Equipment { get; set; }
        public string? GifUrl { get; set; }
        public string? ExerciseID { get; set; }
        public string? Name { get; set; }
        public string? TargetMuscle { get; set; }
        public int ExerciseNumber { get; set; }
        public IEnumerable<OrderedExercises>? OrderedExercises { get; set; }
        public IEnumerable<BodyPart>? BodyPartList { get; set; }
    }
}
