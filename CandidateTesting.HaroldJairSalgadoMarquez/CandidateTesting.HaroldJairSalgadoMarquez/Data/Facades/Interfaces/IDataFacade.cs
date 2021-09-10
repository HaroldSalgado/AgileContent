using CandidateTesting.HaroldJairSalgadoMarquez.Models;
using System.Collections.Generic;

namespace CandidateTesting.HaroldJairSalgadoMarquez.Data.Facades.Interfaces
{
    interface IDataFacade
    {
        string GetStringLogsFromURL(string source);
        void WriteLogFile(List<LogModel> logs);
    }
}
