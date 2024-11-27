using AutoMapper;
using Desafio.BackEnd.Dtos;
using Desafio.BackEnd.Entities;
using Desafio.BackEnd.Interfaces;
using Desafio.BackEnd.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Desafio.BackEnd.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(_mapper.Map<List<EventDto>>(await _unitOfWork.RepositoryEvent.GetAllAsync()));
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
        {
            var e = await _unitOfWork.RepositoryEvent.GetByIdAsync(id);

            if (e == null) return NotFound("Evento não encontrado.");

            var eDto = _mapper.Map<EventDto>(e);

            return Ok(eDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] EventDto dto)
        {
            var validator = new EventValidation();
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var panelist = await _unitOfWork.RepositoryPanelist.GetByIdAsync((long)dto.PanelistId);

            if (panelist == null) return BadRequest("Palestrante não encontrado");
            // verificar as mensagens de erro duplicadas, transformar a validacao de null e empty em apenas 1 

            var e = _mapper.Map<Event>(dto);

            var eventCreated = await _unitOfWork.RepositoryEvent.CreateAsync(e);
            await _unitOfWork.CommitAsync();

            var eventDto = _mapper.Map<EventDto>(eventCreated);

            return Created($"event/{eventDto.Id}", eventDto);
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            var e = await _unitOfWork.RepositoryEvent.GetByIdAsync(id);

            if (e == null) return NotFound("Evento não encontrado");

            await _unitOfWork.RepositoryEvent.DeleteAsync(id);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] EventDto dto)
        {
            if (dto.Id != id) return BadRequest("Id da Requisição diferente da Entidade");

            var e = await _unitOfWork.RepositoryEvent.GetByIdAsync(id);
            if (e == null) return NotFound("Evento não encontrado");

            var panelist = await _unitOfWork.RepositoryPanelist.GetByIdAsync((long)dto.PanelistId);
            if (panelist == null) return BadRequest("Palestrante não encontrado");

            var validator = new EventValidation();
            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => e.ErrorMessage).ToList());
            }

            _mapper.Map(dto, e);
            _unitOfWork.RepositoryEvent.Update(e);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}
