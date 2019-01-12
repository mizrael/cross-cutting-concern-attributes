using System.Collections.Generic;
using MediatR;

namespace cross_cutting_concern_attributes.Queries
{
    public class ValuesArchive : IRequest<IReadOnlyCollection<string>>
    {
    }
}
