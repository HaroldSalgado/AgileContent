using CandidateTesting.HaroldJairSalgadoMarquez.Business.Managers.Interfaces;
using CandidateTesting.HaroldJairSalgadoMarquez.Business.Validators.Interfaces;
using CandidateTesting.HaroldJairSalgadoMarquez.Data.Facades.Interfaces;

namespace CandidateTesting.HaroldJairSalgadoMarquez.Business.Managers
{
    class ApplicationManager : IApplicationManager
    {
        private ILogConverter _logConverter;
        private IDataFacade _datafacade;
        private ISourceValidator _sourceValidator;

        public ApplicationManager(ILogConverter logConverter, IDataFacade dataFacade, ISourceValidator sourceValidator)
        {
            _logConverter = logConverter;
            _datafacade = dataFacade;
            _sourceValidator = sourceValidator;
        }
        public void ConvertLogs(string source, string target)
        {
            if (_sourceValidator.ValidateSourceString(source))
            {
                var stringLogs = _datafacade.GetStringLogsFromURL(source);
                if (stringLogs != null)
                {
                    var mappedLogs = _logConverter.MapLogsFromString(stringLogs);
                    if (mappedLogs.Count > 0)
                    {
                        _datafacade.WriteLogFile(mappedLogs, target);
                    }
                }
            }
        }
    }
}
