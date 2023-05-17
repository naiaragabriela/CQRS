using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CQRS.Domain.Domain;
using CQRS.Domain.Helpers;
using Microsoft.AspNetCore.Components.Forms;

namespace CQRS.Domain.Queries.ListPerson
{
    public class ListPersonQueryProfile : Profile
    {
        public ListPersonQueryProfile()
        {
            CreateMap<Person, ListPersonQueryResponse>()
                .ForMember(fieldOut => fieldOut.Cpf, option => option
                .MapFrom(input => input.Cpf.FormatCpf()));
        }
    }
}
