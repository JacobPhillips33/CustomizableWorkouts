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

        public IEnumerable<Workout> GetWorkoutList()
        {
            return _conn.Query<Workout>("SELECT * FROM workouts;");
        }

        public Exercise SpecificExercise(int id)
        {
            var exercise = _conn.QuerySingle<Exercise>("SELECT * FROM allexercises WHERE ExerciseID = @id;", 
                new {id = id});

            exercise.WorkoutList = GetWorkoutList();

            return exercise;
        }

        public IEnumerable<Exercise> AllFavoriteExercisesList()
        {
            return _conn.Query<Exercise>("SELECT * FROM favoriteexercises;");
        }

        public void AddExerciseToFavorites(Exercise exerciseToAdd)
        {
            var exercisesList = AllFavoriteExercisesList();

            bool onList = false;
            foreach (var exercise in exercisesList)
            {
                if (exercise.ExerciseID == exerciseToAdd.ExerciseID)
                {
                    onList = true;
                    break;
                }
            }

            if (!onList)
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
        }

        public void RemoveExerciseFromFavorites(ExerciseGroup favoriteToRemove)
        {
            _conn.Execute("DELETE FROM favoriteexercises WHERE ExerciseID = @id;", new { id = favoriteToRemove.ExerciseID });
        }
    }
}
