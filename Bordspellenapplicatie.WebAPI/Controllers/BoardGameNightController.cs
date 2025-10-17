using AutoMapper;
using Bordspellenapplicatie.WebAPI.Models.DTO;
using Bordspellenapplicatie.WebAPI.Models.Request.BoardGameNight;
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
    public class BoardGameNightController : ControllerBase
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
            var boardGameNightDomain = await boardGameNightRepository.GetAll();

            return TypedResults.Ok(new BoardGameNightsResponse() { BoardGameNights = mapper.Map<IEnumerable<BoardGameNightDto>>(boardGameNightDomain) });
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(BoardGameNightResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Results<Ok<BoardGameNightResponse>, EmptyHttpResult, ProblemHttpResult, NotFound>> GetBoardGameNightById(int id)
        {
            var boardGameNight = await boardGameNightRepository.GetById(id);

            if (boardGameNight == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new BoardGameNightResponse() { BoardGameNight = mapper.Map<BoardGameNightDto>(boardGameNight) });
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<NoContent, NotFound, ProblemHttpResult>> DeleteBoardGameNight(int id)
        {
            var boardGameNight = await boardGameNightRepository.GetById(id);

            if (boardGameNight == null) {
                return TypedResults.NotFound();
            }

            try
            {
                await boardGameNightRepository.Delete(id);
                return TypedResults.NoContent();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
           
        }

        [HttpPost]
        [ProducesResponseType(typeof(BoardGameNightResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<Created<BoardGameNightResponse>, BadRequest, ProblemHttpResult>> CreateBoardGameNight([FromBody] CreateBoardGameNight request)
        {
            if (request == null)
            {
                return TypedResults.BadRequest();
            }

            var boardGameNightDomainModel = mapper.Map<BoardGameNight>(request);

            await boardGameNightRepository.Add(boardGameNightDomainModel);

            var response = new BoardGameNightResponse
            {
                BoardGameNight = mapper.Map<BoardGameNightDto>(boardGameNightDomainModel)
            };

            return TypedResults.Created($"https://localhost:7287/api/BoardGameNight/{boardGameNightDomainModel.Id}", response);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(BoardGameNightResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Results<Ok<BoardGameNightResponse>, NotFound, BadRequest, ProblemHttpResult>> UpdateBoardGameNight([FromRoute] int id, [FromBody] UpdateBoardGameNightRequest request)
        {
            if (request == null)
            {
                return TypedResults.BadRequest();
            }

            var boardGameNightDomainModel = mapper.Map<BoardGameNight>(request);

            boardGameNightDomainModel = await boardGameNightRepository.Update(id, boardGameNightDomainModel);

            if (boardGameNightDomainModel == null)
            {
                return TypedResults.NotFound();
            }

            var response = new BoardGameNightResponse
            {
                BoardGameNight = mapper.Map<BoardGameNightDto>(boardGameNightDomainModel)
            };

            return TypedResults.Ok(response);
        }
    }
}
