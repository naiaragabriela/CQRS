using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Domain.Contracts;
using CQRS.Domain.Core;
using CQRS.Domain.Domain;

namespace CQRS.Domain.Queries.ListPerson
{
    public class ListPersonQueryHandler:BaseHandler
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public ListPersonQueryHandler(IPersonRepository personRepository,IMapper mapper) 
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }
        
        public async Task<List<ListPersonQueryResponse>> HandleAsync(ListPersonQuery query, CancellationToken cancellationToken)
        {
            var people = await _personRepository.GetAsync(person =>
            (query.Name == null || person.Name.Contains(query.Name.ToUpper()))&&(query.Cpf == null || person.Cpf.Contains(query.Cpf)),
            cancellationToken);

            return _mapper.Map<List<ListPersonQueryResponse>>(people);
        }
    }
}
