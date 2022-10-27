using Dapper;
using ExerciseApp.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Data;
using System.Globalization;

namespace ExerciseApp.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly IDbConnection _conn;

        public ExerciseRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Exercise> AllExercisesList()
        {
            return _conn.Query<Exercise>("SELECT * FROM allexercises;");
        }

        public Exercise SpecificExercise(int id)
        {
            return _conn.QuerySingle<Exercise>("SELECT * FROM allexercises WHERE ExerciseID = @id;", 
                new {id = id});
        }

        public IEnumerable<Exercise> ExercisesByBodyPart(Exercise exercise)
        {
            return _conn.Query<Exercise>("SELECT * FROM allexercises WHERE BodyPart = @bodyPart;", 
                new {bodyPart = exercise.BodyPart});
        }
        public IEnumerable<Exercise> ExercisesByTargetMuscle(Exercise exercise)
        {
            return _conn.Query<Exercise>("SELECT * FROM allexercises WHERE TargetMuscle = @targetMuscle;", 
                new {targetMuscle = exercise.TargetMuscle});
        }
        public IEnumerable<Exercise> ExercisesByEquipmentNeeded(Exercise exercise)
        {
            return _conn.Query<Exercise>("SELECT * FROM allexercises WHERE Equipment = @equipment;", 
                new { equipment = exercise.Equipment});
        }

        public IEnumerable<BodyParts> GetBodyPartsList()
        {
            return _conn.Query<BodyParts>("SELECT * FROM bodyparts;");
        }
        public IEnumerable<TargetMuscles> GetTargetMusclesList()
        {
            return _conn.Query<TargetMuscles>("SELECT * FROM targetmuscles;");
        }
        public IEnumerable<EquipmentNeeded> GetEquipmentNeededList()
        {
            return _conn.Query<EquipmentNeeded>("SELECT * FROM equipmentneeded;");
        }
        public Exercise AssignProperties()
        {
            var bodyPartsList = GetBodyPartsList();
            var targetMusclesList = GetTargetMusclesList();
            var equipmentNeededList = GetEquipmentNeededList();
            var exercise = new Exercise()
            {
                BodyPartList = bodyPartsList,
                TargetMuscleList = targetMusclesList,
                EquipmentList = equipmentNeededList,
            };
            return exercise;
        }
        public IEnumerable<Exercise> AllFavoriteExercisesList()
        {
            return _conn.Query<Exercise>("SELECT * FROM favoriteexercises;");
        }
        public void AddExerciseToFavorites(Exercise exerciseToAdd)
        {
            _conn.Execute("INSERT INTO favoriteexercises (ExerciseID, ApiID, Name, BodyPart, TargetMuscle, Equipment, GifURL) " +
                "VALUES (@ExerciseID, @ApiID, @Name, @BodyPart, @TargetMuscle, @Equipment, @GifURL);",
                new
                {
                    ExerciseID = exerciseToAdd.ExerciseID,
                    ApiID = exerciseToAdd.ApiID,
                    Name = exerciseToAdd.Name,
                    BodyPart = exerciseToAdd.BodyPart,
                    TargetMuscle = exerciseToAdd.TargetMuscle,
                    Equipment = exerciseToAdd.Equipment,
                    GifURL = exerciseToAdd.GifUrl,
                });
        }
        public void RemoveExerciseFromFavorites(Exercise exerciseToRemove)
        {
            _conn.Execute("DELETE FROM favoriteexercises WHERE ExerciseID = @id;", new { id = exerciseToRemove.ExerciseID });
        }
    }
}
