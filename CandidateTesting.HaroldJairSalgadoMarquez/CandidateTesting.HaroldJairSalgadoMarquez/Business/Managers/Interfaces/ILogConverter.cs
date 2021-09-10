using CandidateTesting.HaroldJairSalgadoMarquez.Models;
using System.Collections.Generic;

namespace CandidateTesting.HaroldJairSalgadoMarquez.Business.Managers.Interfaces
{
    interface ILogConverter
    {
        List<LogModel> MapLogsFromString(string source);
    }
}
