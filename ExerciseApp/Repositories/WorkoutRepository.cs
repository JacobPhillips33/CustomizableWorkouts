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

        public WorkoutGroup GetWorkoutGroup()
        {
            var workoutsList = GetAllWorkouts();
            var workoutGroup = new WorkoutGroup()
            {
                WorkoutsList = workoutsList,
            };
            return workoutGroup;
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
            var tableName = GetTableName(workout);

            MySqlCommand createWorkoutTable = new MySqlCommand($"CREATE TABLE {tableName} (ExerciseID int, " +
                $"Name varchar(100), Reps varchar(45), Sets varchar(45));", _mySqlConn);

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
            char[] tableName = workout.WorkoutName.Where(x => char.IsLetterOrDigit(x)).ToArray();
            return new string(tableName).ToLower();
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

        public void RemoveExerciseFromWorkout(Workout workout)
        {
            var tableName = GetTableName(workout);

            _conn.Execute($"DELETE FROM {tableName} WHERE ExerciseID = @id;", new { id = workout.ExerciseID });
        }

        public void UpdateWorkout(WorkoutGroup workoutGroup)
        {
            _conn.Execute("UPDATE workouts SET WorkoutName = @name WHERE WorkoutID = @id;", 
                new { id = workoutGroup.WorkoutID, name = workoutGroup.WorkoutName });
        }

        public void DeleteWorkout(WorkoutGroup workoutGroup)
        {
            _conn.Execute("DELETE FROM workouts WHERE WorkoutID = @id;", new { id = workoutGroup.WorkoutID });
        }
    }
}
