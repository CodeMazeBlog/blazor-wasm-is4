﻿using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var claims = User.Claims;

                var companies = _repository.Company.GetAllCompanies(trackChanges: false);

                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

                return Ok(companiesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedTestAction() => 
            Ok(new { Message = "Access is allowed for unauthorized users" });

        [HttpGet("Privacy")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<string> Privacy()
        {
            var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();

            return claims;
        }
    }
}