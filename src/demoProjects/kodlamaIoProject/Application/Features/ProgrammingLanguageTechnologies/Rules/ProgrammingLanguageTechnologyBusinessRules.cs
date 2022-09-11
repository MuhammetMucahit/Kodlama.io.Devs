using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageTechnologies.Rules
{
    public class ProgrammingLanguageTechnologyBusinessRules
    {
        IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;

        public ProgrammingLanguageTechnologyBusinessRules(IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository)
        {
            _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
        }

        public async Task ProgrammingLanguageTechnologyNameCanNotBeDuplicatedWhenInserted(int programmingLanguageId, string name)
        {
            IPaginate<ProgrammingLanguageTechnology> result = await _programmingLanguageTechnologyRepository
                .GetListAsync(x => x.ProgrammingLanguageId == programmingLanguageId && x.Name == name);
            if (result.Items.Any()) throw new BusinessException("Programming Language Technology name exists");
        }
        public async Task ProgrammingLanguageTechnologyShouldExistWhenRequested(int id)
        {
            ProgrammingLanguageTechnology? result = await _programmingLanguageTechnologyRepository.GetAsync(x => x.Id == id);
            if (result == null) throw new BusinessException("Requested programming language technology does not exists.");
        }
    }
}
