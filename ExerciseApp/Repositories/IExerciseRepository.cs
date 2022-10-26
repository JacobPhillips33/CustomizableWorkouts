using ExerciseApp.Models;

namespace ExerciseApp.Repositories
{
    public interface IExerciseRepository
    {
        public IEnumerable<Exercise> AllExercisesList();
        public Exercise SpecificExercise(int id);
        public IEnumerable<Exercise> ExercisesByBodyPart(Exercise exercise);
        public IEnumerable<Exercise> ExercisesByTargetMuscle(Exercise exercise);
        public IEnumerable<Exercise> ExercisesByEquipmentNeeded(Exercise exercise);
        public IEnumerable<BodyParts> GetBodyPartsList();
        public IEnumerable<TargetMuscles> GetTargetMusclesList();
        public IEnumerable<EquipmentNeeded> GetEquipmentNeededList();
        public Exercise AssignProperties();
    }
}
