﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApplication.Api.DataTransferObjects;
using PortfolioApplication.Services.CQRS.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioApplication.Api.Controllers
{
    /// <summary>
    /// Controller processing requests for TechnologyType entities. Produces JSON output.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TechnologyTypeController : Controller
    {
        private readonly ITechnologyTypeEntityQuery _technologyTypeEntityQuery;

        /// <summary>
        /// TechnologyTypeController constructor
        /// </summary>
        /// <param name="technologyTypeEntityQuery"> Query consumed to retrieve TechnologyType entities </param>
        public TechnologyTypeController(
            ITechnologyTypeEntityQuery technologyTypeEntityQuery)
        {
            _technologyTypeEntityQuery = technologyTypeEntityQuery;
        }

        /// <summary>
        /// Get endpoint retrieving TechnologyType entity by its id
        /// </summary>
        /// <param name="id"> Identification number of TechnologyType entity </param>
        /// <returns> TechnologyEntity in JSON format </returns>
        [HttpGet]
        public async Task<IActionResult> GetTechnologyTypeById(int id)
        {
            var technologyTypeEntity = await _technologyTypeEntityQuery.Get(id);
            var technologyTypeDto = Mapper.Map<TechnologyTypeDto>(technologyTypeEntity);

            return new JsonResult(technologyTypeDto);
        }

        /// <summary>
        /// Get endpoint retrieving all TechnologyType entities
        /// </summary>
        /// <returns> TechnologyEntity collection in JSON format </returns>
        [HttpGet]
        public async Task<IActionResult> GetTechnologyTypes()
        {
            var technologyTypeEntities = await _technologyTypeEntityQuery.Get();
            var technologyTypeDtos = Mapper.Map<IEnumerable<TechnologyTypeDto>>(technologyTypeEntities);

            return new JsonResult(technologyTypeDtos);
        }
    }
}