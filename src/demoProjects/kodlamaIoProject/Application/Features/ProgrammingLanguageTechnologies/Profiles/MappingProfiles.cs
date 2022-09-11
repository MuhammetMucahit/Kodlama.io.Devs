using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguageTechnologies.Commands.CreateProgrammingLanguageTechnology;
using Application.Features.ProgrammingLanguageTechnologies.Commands.DeleteProgrammingLanguageTechnology;
using Application.Features.ProgrammingLanguageTechnologies.Commands.UpdateProgrammingLanguageTechnology;
using Application.Features.ProgrammingLanguageTechnologies.Dtos;
using Application.Features.ProgrammingLanguageTechnologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguageTechnologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateProgrammingLanguageTechnologyDto, ProgrammingLanguageTechnology>().ReverseMap();
            CreateMap<CreateProgrammingLanguageTechnologyCommand, ProgrammingLanguageTechnology>().ReverseMap();

            CreateMap<UpdateProgrammingLanguageTechnologyDto, ProgrammingLanguageTechnology>().ReverseMap();
            CreateMap<UpdateProgrammingLanguageTechnologyCommand, ProgrammingLanguageTechnology>().ReverseMap();

            CreateMap<DeleteProgrammingLanguageTechnologyDto, ProgrammingLanguageTechnology>().ReverseMap();
            CreateMap<DeleteProgrammingLanguageTechnologyCommand, ProgrammingLanguageTechnology>().ReverseMap();

            CreateMap<ProgrammingLanguageTechnology, ProgrammingLanguageTechnologyListDto>()
                .ForMember(entity => entity.TechnologyName, dto => dto.MapFrom(c => c.ProgrammingLanguage.Name));
            CreateMap<IPaginate<ProgrammingLanguageTechnology>, ProgrammingLanguageTechnologyListModel>().ReverseMap();


        }
    }
}
