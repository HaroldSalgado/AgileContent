using CandidateTesting.HaroldJairSalgadoMarquez.Business.Managers.Interfaces;
using CandidateTesting.HaroldJairSalgadoMarquez.Models;
using System.Collections.Generic;

namespace CandidateTesting.HaroldJairSalgadoMarquez.Business.Managers
{
    class LogConverter: ILogConverter
    {
        public List<LogModel> MapLogsFromString(string source)
        {
            var result = new List<LogModel>();
            return result;
        }
    }
}
