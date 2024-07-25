using InsuranceAppRLL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAppRLL.CQRS.Queries.SchemeQueries
{
    public class GetSchemeByIdQuery : IRequest<Scheme>
    {
        public int SchemeId { get; }

        public GetSchemeByIdQuery(int schemeId)
        {
            SchemeId = schemeId;
        }
    }
}
