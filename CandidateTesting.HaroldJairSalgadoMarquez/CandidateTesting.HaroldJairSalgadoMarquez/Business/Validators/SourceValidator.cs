using CandidateTesting.HaroldJairSalgadoMarquez.Business.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTesting.HaroldJairSalgadoMarquez.Business.Validators
{
    class SourceValidator: ISourceValidator
    {
        public bool ValidateSourceString(string source)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(source, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }
    }
}
