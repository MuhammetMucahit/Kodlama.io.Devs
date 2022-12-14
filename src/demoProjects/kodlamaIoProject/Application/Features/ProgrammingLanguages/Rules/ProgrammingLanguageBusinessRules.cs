using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
           IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(x => x.Name == name);
            if (result.Items.Any()) throw new BusinessException("Programming Language name exists");
        }
        public async Task ProgrammingLanguageShouldExistWhenRequested(int id)
        {
            ProgrammingLanguage? result = await _programmingLanguageRepository.GetAsync(x => x.Id == id);
            if (result == null) throw new BusinessException("Requested programming language does not exists.");
        }
    }
}
