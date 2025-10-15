using AutoMapper;
using Bordspellenapplicatie.WebAPI.Models.DTO;
using Bordspellenapplicatie.WebAPI.Models.Response;
using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Bordspellenapplicatie.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGameNightController
    {
        private readonly IMapper mapper;
        private readonly IBoardGameNightRepository boardGameNightRepository;

        public BoardGameNightController(IMapper mapper, IBoardGameNightRepository boardGameNightRepository)
        {
            this.mapper = mapper;
            this.boardGameNightRepository = boardGameNightRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BoardGameNightsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Results<Ok<BoardGameNightsResponse>, EmptyHttpResult, ProblemHttpResult>> GetBoardGameNights()
        {
            IEnumerable<BoardGameNight> boardGameNights = boardGameNightRepository.GetAll();

            return TypedResults.Ok(new BoardGameNightsResponse() { BoardGameNights = mapper.Map<IEnumerable<BoardGameNightDto>>(boardGameNights) });
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(BoardGameNightResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Results<Ok<BoardGameNightResponse>, EmptyHttpResult, ProblemHttpResult, NotFound>> GetBoardGameNightById(int id)
        {
            var boardGameNight = boardGameNightRepository.GetById(id);

            if (boardGameNight == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new BoardGameNightResponse() { BoardGameNight = mapper.Map<BoardGameNightDto>(boardGameNight) });
        }

        //[HttpDelete]
        //[Route("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<>
    }
}
