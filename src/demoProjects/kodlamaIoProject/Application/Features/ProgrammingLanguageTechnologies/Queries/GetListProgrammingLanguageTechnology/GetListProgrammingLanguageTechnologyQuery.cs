using Application.Features.ProgrammingLanguageTechnologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageTechnologies.Queries.GetListProgrammingLanguageTechnology
{

    public class GetListProgrammingLanguageTechnologyQuery : ISecuredRequest, IRequest<ProgrammingLanguageTechnologyListModel>
    {
        public PageRequest PageRequest { get; set; }

        public string[] Roles => new string[] { "admin", "user" };

        public class GetListProgrammingLanguageTechnologyQueryHandler : IRequestHandler<GetListProgrammingLanguageTechnologyQuery, ProgrammingLanguageTechnologyListModel>
        {
            IMapper _mapper;
            IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;

            public GetListProgrammingLanguageTechnologyQueryHandler(IMapper mapper, IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository)
            {
                _mapper = mapper;
                _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
            }

            public async Task<ProgrammingLanguageTechnologyListModel> Handle(GetListProgrammingLanguageTechnologyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<ProgrammingLanguageTechnology> programmingLanguageTechnologies = await _programmingLanguageTechnologyRepository.GetListAsync(include:
                                                            x => x.Include(p => p.ProgrammingLanguage),
                                                            index: request.PageRequest.Page,
                                                            size:request.PageRequest.PageSize
                                                            );
               var mappedProgrammingLanguageTechnology = _mapper.Map<ProgrammingLanguageTechnologyListModel>(programmingLanguageTechnologies);
               return mappedProgrammingLanguageTechnology;
            }
        }
    }
}
