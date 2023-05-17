using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.GetPerson
{
    public class GetPersonQuery
    {
        public GetPersonQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}

