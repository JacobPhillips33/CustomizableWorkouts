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

        public IEnumerable<BodyParts> GetBodyPartsList()
        {
            return _conn.Query<BodyParts>("SELECT * FROM bodyparts;");
        }
        public Exercise AssignProperties()
        {
            var bodyPartsList = GetBodyPartsList();
            var exercise = new Exercise()
            {
                BodyPartList = bodyPartsList,
            };
            return exercise;
        }
    }
}
