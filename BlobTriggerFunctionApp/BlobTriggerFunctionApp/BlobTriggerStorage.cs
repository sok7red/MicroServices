using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BlobTriggerFunctionApp
{
    public class BlobTriggerStorage
    {
        private readonly IJokeService _jokeService;
        private readonly IConfiguration _configuration;

        public BlobTriggerStorage(IJokeService jokeService, IConfiguration configuration)
        {
            _jokeService = jokeService;
            _configuration = configuration;
        }

        [FunctionName("BlobTriggerStorage")]
        public void Run(
            [BlobTrigger("samples-workitems/{name}", Connection = "Azurefunctionappblobspace")]
            Stream myBlob,
            string name,
            ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            string keyName = "BlobTriggeeredApp:Settings:Message";
            string message = _configuration[keyName];

            string joke = _jokeService.GetJoke();
            log.LogInformation($"{joke}");
        }
    }
}