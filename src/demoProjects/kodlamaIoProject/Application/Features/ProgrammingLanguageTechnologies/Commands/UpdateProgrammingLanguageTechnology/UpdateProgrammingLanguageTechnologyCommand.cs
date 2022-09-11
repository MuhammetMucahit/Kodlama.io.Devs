using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
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

namespace Application.Features.ProgrammingLanguageTechnologies.Commands.UpdateProgrammingLanguageTechnology
{
    public class UpdateProgrammingLanguageTechnologyCommand : IRequest<UpdateProgrammingLanguageTechnologyDto>
    {
        public int Id { get; set; }
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public class UpdateProgrammingLanguageTechnologyCommandHandler : IRequestHandler<UpdateProgrammingLanguageTechnologyCommand, UpdateProgrammingLanguageTechnologyDto>
        {
            IMapper _mapper;
            IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;
            ProgrammingLanguageTechnologyBusinessRules _programmingLanguageTechnologyBusinessRules;
            ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

            public UpdateProgrammingLanguageTechnologyCommandHandler(IMapper mapper, IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository, ProgrammingLanguageTechnologyBusinessRules programmingLanguageTechnologyBusinessRules, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _mapper = mapper;
                _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
                _programmingLanguageTechnologyBusinessRules = programmingLanguageTechnologyBusinessRules;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }
            public async Task<UpdateProgrammingLanguageTechnologyDto> Handle(UpdateProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _programmingLanguageTechnologyBusinessRules.ProgrammingLanguageTechnologyShouldExistWhenRequested(request.Id);
                await _programmingLanguageBusinessRules.ProgrammingLanguageShouldExistWhenRequested(request.ProgrammingLanguageId);
                await _programmingLanguageTechnologyBusinessRules.ProgrammingLanguageTechnologyNameCanNotBeDuplicatedWhenInserted(request.ProgrammingLanguageId, request.Name);

                var mappedProgrammingLanguageTechnology = _mapper.Map<ProgrammingLanguageTechnology>(request);
                var updatedProgrammingLanguageTechnology = await _programmingLanguageTechnologyRepository.UpdateAsync(mappedProgrammingLanguageTechnology);
                var updatedProgrammingLanguageTechnologyDto = _mapper.Map<UpdateProgrammingLanguageTechnologyDto>(updatedProgrammingLanguageTechnology);

                return updatedProgrammingLanguageTechnologyDto;
            }
        }
    }
}
