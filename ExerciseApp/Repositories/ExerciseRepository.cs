using ExerciseApp.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Globalization;

namespace ExerciseApp.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        public IRestResponse AllExercisesResponse()
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();
            string key = config.GetConnectionString("X-RapidAPI-Key");
            string host = config.GetConnectionString("X-RapidAPI-Host");
            var client = new RestClient("https://exercisedb.p.rapidapi.com/exercises");
            var request = new RestRequest(Method.GET);
            request.AddHeader("X-RapidAPI-Key", key);
            request.AddHeader("X-RapidAPI-Host", host);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public IEnumerable<Exercise> AllExercisesList()
        {
            var response = AllExercisesResponse();

            var allExerciseParse = JArray.Parse(response.Content);

            List<Exercise> allExercises = new List<Exercise>();

            for (int i = 0; i < allExerciseParse.Count; i++)
            {
                var thisBodyPart = allExerciseParse[i]["bodyPart"];
                var thisEquipment = allExerciseParse[i]["equipment"];
                var thisGifUrl = allExerciseParse[i]["gifUrl"];
                var thisID = allExerciseParse[i]["id"];
                var thisName = allExerciseParse[i]["name"];
                var thisTarget = allExerciseParse[i]["target"];

                var exercise = new Exercise()
                {
                    BodyPart = (string?)thisBodyPart,
                    Equipment = (string?)thisEquipment,
                    GifUrl = (string?)thisGifUrl,
                    ExerciseID = (string?)thisID,
                    Name = (string?)thisName,
                    TargetMuscle = (string?)thisTarget,
                    ExerciseNumber = i + 1
                };

                allExercises.Add(exercise);
            }
            
            return allExercises;
        }

        public Exercise SpecificExercise(int exerciseNumber)
        {
            var exerciseList = AllExercisesList();

            return exerciseList.ElementAt(exerciseNumber - 1);
        }

        public IEnumerable<Exercise> BodyPartExerciseList(string bodyPart)
        {
            var response = AllExercisesResponse();

            var allExerciseParse = JArray.Parse(response.Content);

            List<Exercise> bodyPartExercises = new List<Exercise>();

            for (int i = 0; i < allExerciseParse.Count; i++)
            {
                if (bodyPart == allExerciseParse[i]["bodyPart"].ToString())
                {
                    var thisBodyPart = allExerciseParse[i]["bodyPart"];
                    var thisEquipment = allExerciseParse[i]["equipment"];
                    var thisGifUrl = allExerciseParse[i]["gifUrl"];
                    var thisID = allExerciseParse[i]["id"];
                    var thisName = allExerciseParse[i]["name"];
                    var thisTarget = allExerciseParse[i]["target"];

                    var exercise = new Exercise()
                    {
                        BodyPart = (string?)thisBodyPart,
                        Equipment = (string?)thisEquipment,
                        GifUrl = (string?)thisGifUrl,
                        ExerciseID = (string?)thisID,
                        Name = (string?)thisName,
                        TargetMuscle = (string?)thisTarget,
                        ExerciseNumber = i + 1
                    };

                    bodyPartExercises.Add(exercise);
                }    
            }

            return bodyPartExercises;
        }
    }
}
