using System.IO;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class ConstrainedRequestPostProcessor<TRequest, TResponse>
        : IRequestPostProcessor<TRequest, TResponse>
        where TRequest : Requests.Donate
    {
        private readonly TextWriter _writer;

        public ConstrainedRequestPostProcessor(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Process(TRequest request, TResponse response)
        {
            return _writer.WriteLineAsync("- All Done with Ping");
        }
    }
}