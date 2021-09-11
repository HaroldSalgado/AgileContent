using CandidateTesting.HaroldJairSalgadoMarquez.Data.Facades.Interfaces;
using CandidateTesting.HaroldJairSalgadoMarquez.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace CandidateTesting.HaroldJairSalgadoMarquez.Data.Facades
{
    public class DataFacade: IDataFacade
    {
        public string GetStringLogsFromURL(string source)
        {
            string result = null;
            var client = new RestClient();
            var request = new RestRequest(source, DataFormat.Json);
            var response = client.Get(request);
            if (response.IsSuccessful)
            {
                result = response.Content;
            }
            return result;
        }
        
        public void WriteLogFile(List<LogModel> logs, string path)
        {
            var result = new List<string>();
            foreach (var log in logs)
            {
                //TODO: Move to settings file
                var format = "\"{{provider}}\" {{http-method}} {{status-code}} {{uri-path}} {{time-taken}} {{response-size}} {{cache-status}}";
                var stringLog =
                    format.Replace("{{provider}}", log.Provider)
                    .Replace("{{http-method}}", log.HttpMethod)
                    .Replace("{{status-code}}", log.StatusCode)
                    .Replace("{{uri-path}}", log.UriPath)
                    .Replace("{{time-taken}}", log.TimeTaken)
                    .Replace("{{response-size}}", log.ResponseSize)
                    .Replace("{{cache-status}}", log.CacheStatus);
                result.Add(stringLog);
            }
            var lines = result.ToArray();
            var folder = Path.GetDirectoryName(path).ToString();
            Directory.CreateDirectory(folder);
            var fileName = Path.GetFileName(path);
            var fullPath = Path.Combine(folder, fileName);
            File.WriteAllLines(fullPath, lines);
        }
    }
}
