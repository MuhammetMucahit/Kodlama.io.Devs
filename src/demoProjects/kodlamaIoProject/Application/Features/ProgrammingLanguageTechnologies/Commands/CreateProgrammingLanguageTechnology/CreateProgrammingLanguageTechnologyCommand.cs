using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.ProgrammingLanguageTechnologies.Dtos;
using Application.Features.ProgrammingLanguageTechnologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageTechnologies.Commands.CreateProgrammingLanguageTechnology
{
    public class CreateProgrammingLanguageTechnologyCommand: IRequest<CreateProgrammingLanguageTechnologyDto>
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class CreateProgrammingLanguageTechnologyCommandHandler : IRequestHandler<CreateProgrammingLanguageTechnologyCommand, CreateProgrammingLanguageTechnologyDto>
        {
            IMapper _mapper;
            IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;
            ProgrammingLanguageTechnologyBusinessRules _programmingLanguageTechnologyBusinessRules;
            ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

            public CreateProgrammingLanguageTechnologyCommandHandler(IMapper mapper, IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository, ProgrammingLanguageTechnologyBusinessRules programmingLanguageTechnologyBusiness, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _mapper = mapper;
                _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
                _programmingLanguageTechnologyBusinessRules = programmingLanguageTechnologyBusiness;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }

            public async Task<CreateProgrammingLanguageTechnologyDto> Handle(CreateProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);
                await _programmingLanguageTechnologyBusinessRules.ProgrammingLanguageTechnologyNameCanNotBeDuplicatedWhenInserted(request.ProgrammingLanguageId, request.Name);

                var mappedProgrammingLanguageTechnology = _mapper.Map<ProgrammingLanguageTechnology>(request);
                var createdProgrammingLanguageTechnology = await _programmingLanguageTechnologyRepository.AddAsync(mappedProgrammingLanguageTechnology);
                var createdProgrammingLanguageTechnologyDto = _mapper.Map<CreateProgrammingLanguageTechnologyDto>(createdProgrammingLanguageTechnology);

                return createdProgrammingLanguageTechnologyDto;
            }
        }
    }
}
