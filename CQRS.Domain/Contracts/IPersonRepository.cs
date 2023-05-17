using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CQRS.Domain.Domain;

namespace CQRS.Domain.Contracts
{
    public interface IPersonRepository
    {
        Task InsertAsync(
            Person person,
            CancellationToken cancellation);
        Task<IEnumerable<Person>> GetAsync(
            Expression<Func<Person, bool>> expression,
            CancellationToken cancellation);
     }
}
