using CandidateTesting.HaroldJairSalgadoMarquez.Business.Managers.Interfaces;
using CandidateTesting.HaroldJairSalgadoMarquez.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CandidateTesting.HaroldJairSalgadoMarquez.Business.Managers
{
    public class LogConverter: ILogConverter
    {
        private IConfiguration _configuration;

        public LogConverter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<LogModel> MapLogsFromString(string source)
        {
            var result = new List<LogModel>();
            var rows = source.Split(new[] { "\r\n", "\r", "\n" },StringSplitOptions.None);
            rows = rows.Where(row => !row.Equals("")).ToArray();
            if (rows.Length > 0)
            {
                foreach (var row in rows)
                {
                    var data = row.Split('|');
                    //TODO: Move to settings file
                    var provider = _configuration.GetValue<string>("provider");
                    var statusCode = data[1];
                    var timeTaken = data[4];
                    var dotIndex = timeTaken.IndexOf('.');
                    string cleanTimeTaken = dotIndex != 0 ? timeTaken.Substring(0, dotIndex) : timeTaken;
                    var cacheStatus = data[2];
                    var responseSize = data[0];

                    var quotesSection = row.Split('"')[1];
                    var methodAndUri = quotesSection.Split(' ');
                    var httpMethod = methodAndUri[0];
                    var uriPath = methodAndUri[1];

                    var log = new LogModel()
                    {
                        CacheStatus = cacheStatus,
                        HttpMethod = httpMethod,
                        Provider = provider,
                        ResponseSize = responseSize,
                        StatusCode = statusCode,
                        TimeTaken = cleanTimeTaken,
                        UriPath = uriPath
                    };
                    result.Add(log);
                }
            }
            return result;
        }
    }
}
