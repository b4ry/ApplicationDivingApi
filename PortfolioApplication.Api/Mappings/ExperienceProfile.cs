﻿using AutoMapper;
using PortfolioApplication.Api.CQRS.Commands.Experiences.Commands;
using PortfolioApplication.Api.DataTransferObjects.Experiences;
using PortfolioApplication.Entities.Entities;

namespace PortfolioApplication.Api.Mappings
{
    /// <summary>
    /// Mapping profile for Experience entity
    /// </summary>
    public class ExperienceProfile : Profile
    {
        /// <summary>
        /// ExperienceProfile constructor configuring mapping profile for Experience entity
        /// </summary>
        public ExperienceProfile()
        {
            CreateMap<ExperienceEntity, ExperienceDto>().ReverseMap();
            CreateMap<CreateExperienceCommand, ExperienceEntity>();
            CreateMap<DeleteExperienceCommand, ExperienceEntity>();
            CreateMap<UpdateExperienceCommand, ExperienceEntity>();
            CreateMap<UpdateExperienceDto, ExperienceDto>().ReverseMap();
        }
    }
}
