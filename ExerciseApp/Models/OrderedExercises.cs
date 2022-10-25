namespace ExerciseApp.Models
{
    public class OrderedExercises
    {
        public string? OrderByBodyPart { get; set; } = "body part";
        public string? OrderByName { get; set; } = "name";
        public string? OrderByTargetMuscle { get; set; } = "target muscle";
        public string? OrderByEquipment { get; set; } = "equipment";
    }
}
