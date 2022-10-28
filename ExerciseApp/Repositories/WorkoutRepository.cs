using Dapper;
using ExerciseApp.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace ExerciseApp.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly IDbConnection _conn;
        private readonly MySqlConnection _mySqlConn;

        public WorkoutRepository(IDbConnection conn, MySqlConnection mySqlConn)
        {
            _conn = conn;
            _mySqlConn = mySqlConn;
        }

        public IEnumerable<Workout> GetAllWorkouts()
        {
            return _conn.Query<Workout>("SELECT * FROM workouts;");
        }

        public void InsertWorkout(Workout workout)
        {
            _conn.Execute("INSERT INTO workouts (WorkoutID, WorkoutName) VALUES (@id, @name);",
                new { id = workout.WorkoutID, name = workout.WorkoutName });
        }

        public Workout InstantianteWorkout()
        {
            var workout = new Workout();
            return workout;
        }

        public void CreateWorkout(Workout workout)
        {
            var name = workout.WorkoutName.Replace(" ", "");
            MySqlCommand createWorkoutTable = new MySqlCommand($"CREATE TABLE {name} (ExerciseID int, ExerciseName varchar(100), " +
                "Reps int, Sets int);", _mySqlConn);

            createWorkoutTable.ExecuteNonQuery();
        }

        public Workout GetWorkout(int id)
        {
            return _conn.QuerySingle<Workout>("SELECT * FROM workouts WHERE WorkoutID = @id;",
                new { id = id });
        }
                
        public Workout AddExerciseToWorkout(Exercise exercise)
        {
            var workout = GetWorkout(exercise.WorkoutID);
            workout.WorkoutExercises.Add(exercise);
            
            var tableName = workout.WorkoutName.Replace(" ","");

            _conn.Execute($"INSERT INTO {tableName} (ExerciseID, ExerciseName) " +
                "VALUES (@id, @name);",
                new
                {
                    id = exercise.ExerciseID,
                    name = exercise.Name,
                });
            return workout;
        }

        public IEnumerable<WorkoutExercise> GetWorkoutTable(int id)
        {
            return _conn.Query<WorkoutExercise>("SELECT * FROM workout1;");
        }
    }
}
