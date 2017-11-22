﻿using Microsoft.Extensions.Caching.Distributed;
using PortfolioApplication.Entities.Entities;
using PortfolioApplication.Services.DatabaseContext;

namespace PortfolioApplication.Api.CQRS.Commands
{
    public class CreateTechnologyCommandHandler : CreateEntityCommandHandler<CreateTechnologyCommand, TechnologyEntity>
    {
        public CreateTechnologyCommandHandler(IDatabaseSet databaseSet, IUnitOfWork unitOfWork, IDistributedCache redisCache) : base(databaseSet, unitOfWork, redisCache)
        {
        }
    }
}
