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

namespace Application.Features.ProgrammingLanguageTechnologies.Commands.DeleteProgrammingLanguageTechnology
{
    public class DeleteProgrammingLanguageTechnologyCommand : IRequest<DeleteProgrammingLanguageTechnologyDto>
    {
        public int Id { get; set; }

        public class DeleteProgrammingLanguageTechnologyCommandHandler : IRequestHandler<DeleteProgrammingLanguageTechnologyCommand, DeleteProgrammingLanguageTechnologyDto>
        {
            IMapper _mapper;
            IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;
            ProgrammingLanguageTechnologyBusinessRules _programmingLanguageTechnologyBusinessRules;
            ProgrammingLanguageBusinessRules _programmingLanguageBusinessRules;

            public DeleteProgrammingLanguageTechnologyCommandHandler(IMapper mapper, IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository, ProgrammingLanguageTechnologyBusinessRules programmingLanguageTechnologyBusinessRules, ProgrammingLanguageBusinessRules programmingLanguageBusinessRules)
            {
                _mapper = mapper;
                _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
                _programmingLanguageTechnologyBusinessRules = programmingLanguageTechnologyBusinessRules;
                _programmingLanguageBusinessRules = programmingLanguageBusinessRules;
            }

            public async Task<DeleteProgrammingLanguageTechnologyDto> Handle(DeleteProgrammingLanguageTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _programmingLanguageTechnologyBusinessRules.ProgrammingLanguageTechnologyShouldExistWhenRequested(request.Id);

                var mappedProgrammingLanguageTechnology = _mapper.Map<ProgrammingLanguageTechnology>(request);
                var deletedProgrammingLanguageTechnology = await _programmingLanguageTechnologyRepository.DeleteAsync(mappedProgrammingLanguageTechnology);
                var deletedProgrammingLanguageTechnologyDto = _mapper.Map<DeleteProgrammingLanguageTechnologyDto>(deletedProgrammingLanguageTechnology);

                return deletedProgrammingLanguageTechnologyDto;
            }
        }
    }
}
