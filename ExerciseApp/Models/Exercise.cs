namespace ExerciseApp.Models
{
    public class Exercise
    {
        public string? BodyPart { get; set; }
        public string? Equipment { get; set; }
        public string? GifUrl { get; set; }
        public int ExerciseID { get; set; }
        public string? Name { get; set; }
        public string? TargetMuscle { get; set; }
        public int ApiID { get; set; }
        public IEnumerable<BodyParts>? BodyPartList { get; set; }
        public IEnumerable<TargetMuscles>? TargetMuscleList { get; set; }
        public IEnumerable<EquipmentNeeded>? EquipmentList { get; set; }
        public IEnumerable<Workout>? WorkoutList { get; set; }
        public int WorkoutID { get; set; }
        public string? Reps { get; set; }
        public string? Sets { get; set; }
    }
}
