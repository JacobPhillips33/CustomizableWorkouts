using Dapper;
using ExerciseApp.Models;
using System.Data;

namespace ExerciseApp.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly IDbConnection _conn;

        public WorkoutRepository(IDbConnection conn)
        {
            _conn = conn;
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
    }
}
