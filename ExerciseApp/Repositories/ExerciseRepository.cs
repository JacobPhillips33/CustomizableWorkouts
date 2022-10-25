using ExerciseApp.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

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

        public IEnumerable<Exercise> AllExercisesList(string sortBy = "")
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
            switch (sortBy)
            {
                case "Name":
                    return allExercises.OrderBy(x => x.Name).ToList();
                case "Body Part":
                    return allExercises.OrderBy(x => x.BodyPart).ToList();
                case "Target Muscle":
                    return allExercises.OrderBy(x => x.TargetMuscle).ToList();
                case "Equipment Needed":
                    return allExercises.OrderBy(x => x.Equipment).ToList();
                default:
                    return allExercises.OrderBy(x => x.ExerciseNumber).ToList();
            }
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
