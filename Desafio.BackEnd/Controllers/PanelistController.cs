using AutoMapper;
using Desafio.BackEnd.Dtos;
using Desafio.BackEnd.Entities;
using Desafio.BackEnd.Interfaces;
using Desafio.BackEnd.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.ComponentModel.DataAnnotations;

namespace Desafio.BackEnd.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class PanelistController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PanelistController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(_mapper.Map<List<PanelistDto>>(await _unitOfWork.RepositoryPanelist.GetAllAsync()));
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
        {
            var p = await _unitOfWork.RepositoryPanelist.GetByIdAsync(id);

            if (p == null) return NotFound("Palestrante não encontrado.");

            var pDto = _mapper.Map<PanelistDto>(p);

            return Ok(pDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PanelistDto dto)
        {
            var validator = new PanelistValidation();
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => e.ErrorMessage).ToList());
            }

            //Verificar a validação de data de aniversario

            var p = _mapper.Map<Panelist>(dto);

            var panelistCreated = await _unitOfWork.RepositoryPanelist.CreateAsync(p);
            await _unitOfWork.CommitAsync();

            var panelistDto = _mapper.Map<PanelistDto>(panelistCreated);

            return Created($"panelist/{panelistDto.Id}", panelistDto);
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            var p = await _unitOfWork.RepositoryPanelist.GetByIdAsync(id);

            if (p == null) return NotFound();

            await _unitOfWork.RepositoryPanelist.DeleteAsync(id);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] PanelistDto dto)
        {
            if (dto.Id != id) return BadRequest("Id da Requisição diferente da Entidade");

            var e = await _unitOfWork.RepositoryPanelist.GetByIdAsync(id);
            if (e == null) return NotFound("Palestrante não encontrado");

            var validator = new PanelistValidation();
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => e.ErrorMessage).ToList());
            }

            _mapper.Map(dto, e);
            _unitOfWork.RepositoryPanelist.Update(e);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}
