using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Domain.Domain;
using CQRS.Domain.Helpers;

namespace CQRS.Domain.Queries.GetPerson
{
    public class GetPersonQueryProfile : Profile
    {
        public GetPersonQueryProfile()
        {
            CreateMap<Person, GetPersonQueryResponse>()
                .ForMember(fieldOutput => fieldOutput.Cpf, option => option
                    .MapFrom(input => input.Cpf.FormatCpf()));
        }
    }
}
