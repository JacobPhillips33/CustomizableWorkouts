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

        public IEnumerable<Exercise> ExercisesByBodyPart(string bodyPart)
        {
            return _conn.Query<Exercise>("SELECT * FROM allexercises WHERE BodyPart = @bodyPart;", 
                new {bodyPart = bodyPart});
        }
    }
}
