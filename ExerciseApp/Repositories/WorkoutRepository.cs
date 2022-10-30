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
            MySqlCommand createWorkoutTable = new MySqlCommand($"CREATE TABLE {name} (ExerciseID int, Name varchar(100), " +
                "Reps int, Sets int);", _mySqlConn);

            createWorkoutTable.ExecuteNonQuery();
        }

        public Workout GetWorkout(int id)
        {
            return _conn.QuerySingle<Workout>("SELECT * FROM workouts WHERE WorkoutID = @id;",
                new { id = id });
        }

        public void AddExerciseToWorkout(Exercise exercise)
        {
            var workout = GetWorkout(exercise.WorkoutID);
            var tableName = GetTableName(workout);
            
            _conn.Execute($"INSERT INTO {tableName} (ExerciseID, Name) " +
                "VALUES (@id, @name);",
                new
                {
                    id = exercise.ExerciseID,
                    name = exercise.Name,
                });
        }

        public string GetTableName(Workout workout)
        {
            return workout.WorkoutName.ToLower().Replace(" ", "");
        }

        public IEnumerable<Exercise> GetWorkoutExercises(Workout workout)
        {
            var tableName = GetTableName(workout);

            return _conn.Query<Exercise>($"SELECT * FROM {tableName};");
        }

        public Workout AssignExercisesToWorkout(Workout workout)
        {
            var workoutExercises = GetWorkoutExercises(workout);
            workout.WorkoutExercises = workoutExercises;

            return workout;
        }

        public void UpdateWorkoutExercise(Workout workout)
        {
            var tableName = GetTableName(workout);

            _conn.Execute($"UPDATE {tableName} SET Reps = @reps, Sets = @sets WHERE ExerciseID = @id;",
                new { reps = workout.Reps, sets = workout.Sets, id = workout.ExerciseID });
        }
    }
}
