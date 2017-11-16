﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.CQRS.Queries
{
    public class ExperienceQuery : Query<ExperienceEntity, ExperienceDto>, IExperienceQuery
    {
        public ExperienceQuery(IDatabaseSet databaseSet, IDistributedCache redisCache) : 
            base(databaseSet, redisCache)
        {
        }

        public async override Task<ExperienceDto> Get(int id)
        {
            string key = ComposeRedisKey(typeof(ExperienceEntity).Name, id.ToString());
            string cachedEntity = await RedisCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedEntity))
            {
                try
                {
                    var retrievedEntity = await EntitySet
                        .Include(exp => exp.Projects)
                        .ThenInclude(proj => proj.Technologies)
                        .ThenInclude(tech => tech.Technology)
                        .ThenInclude(tech1 => tech1.TechnologyType)
                        .Include(exp => exp.Projects)
                        .ThenInclude(proj => proj.ProjectType)
                        .SingleAsync(exp => exp.Id == id);
                    var retrievedDto = Mapper.Map<ExperienceDto>(retrievedEntity);

                    cachedEntity = JsonConvert.SerializeObject(retrievedDto, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                    await RedisCache.SetStringAsync(key, cachedEntity);
                }
                catch (Exception)
                {
                    throw new KeyNotFoundException($"Could not retrieve entity (id: '{id}', type: '{typeof(ExperienceEntity).Name}') from database.");
                }
            }

            return JsonConvert.DeserializeObject<ExperienceDto>(cachedEntity);
        }
    }
}
