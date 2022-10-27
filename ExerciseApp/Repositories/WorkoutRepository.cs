﻿using Dapper;
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
            //MySqlCommand createWorkoutTable = new MySqlCommand("CREATE TABLE @name (ExerciseID int, ExerciseName varchar(100), " +
            //    "Reps int, Sets int);", _mySqlConn new { name = workout.WorkoutName });

            MySqlCommand createWorkoutTable = new MySqlCommand($"CREATE TABLE {workout.WorkoutName} (ExerciseID int, " +
                $"ExerciseName varchar(100), Reps int, Sets int);", _mySqlConn);

            createWorkoutTable.ExecuteNonQuery();
        }
    }
}