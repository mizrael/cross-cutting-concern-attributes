using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace cross_cutting_concern_attributes.Queries.Handlers
{
    [Instrumentation]
    public class ValuesArchiveHandler : MediatR.IRequestHandler<ValuesArchive, IReadOnlyCollection<string>>
    {
        public Task<IReadOnlyCollection<string>> Handle(ValuesArchive request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<string> results = ImmutableArray.CreateRange(new[] {"lorem", "ipsum"});
            return Task.FromResult(results);
        }
    }
}