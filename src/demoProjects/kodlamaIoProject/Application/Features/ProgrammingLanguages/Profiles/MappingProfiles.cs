using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreatedProgrammingLanguageDto, ProgrammingLanguage > ().ReverseMap();
            CreateMap<CreateProgrammingLanguageCommand, ProgrammingLanguage>().ReverseMap();


            CreateMap<UpdatedProgrammingLanguageDto, ProgrammingLanguage>().ReverseMap();
            CreateMap<UpdateProgrammingLanguageCommand, ProgrammingLanguage>().ReverseMap();

            CreateMap<DeletedProgrammingLanguageDto, ProgrammingLanguage>().ReverseMap();
            CreateMap<DeleteProgrammingLanguageCommand, ProgrammingLanguage>().ReverseMap();

            CreateMap<ProgrammingLanguageListModel, IPaginate<ProgrammingLanguage>>().ReverseMap();

            CreateMap<ProgrammingLanguageGetByIdDto, ProgrammingLanguage>().ReverseMap();

        }
    }
}
