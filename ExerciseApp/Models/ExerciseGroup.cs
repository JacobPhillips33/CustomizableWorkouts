namespace ExerciseApp.Models
{
    public class ExerciseGroup
    {
        public int ExerciseID { get; set; }
        public int ApiID { get; set; }
        public string? Name { get; set; }
        public string? BodyPart { get; set; }
        public string? TargetMuscle { get; set; }
        public string? Equipment { get; set; }
        public string? GifUrl { get; set; }
        public IEnumerable<Exercise>? ExercisesList { get; set; }        
    }
}
