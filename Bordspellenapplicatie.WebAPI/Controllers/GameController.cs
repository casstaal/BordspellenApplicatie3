using AutoMapper;
using Bordspellenapplicatie.WebAPI.Models.DTO;
using Bordspellenapplicatie.WebAPI.Models.Request.BoardGame;
using Bordspellenapplicatie.WebAPI.Models.Request.BoardGameNight;
using Bordspellenapplicatie.WebAPI.Models.Response.BoardGame;
using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Bordspellenapplicatie.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IGameRepository gameRepository;

        public GameController(IMapper mapper, IGameRepository gameRepository)
        {
            this.mapper = mapper;
            this.gameRepository = gameRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BoardgamesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Results<Ok<BoardgamesResponse>, EmptyHttpResult, ProblemHttpResult>> GetBoardgames()
        {
            var gameDomain = await gameRepository.GetAll();

            return TypedResults.Ok(new BoardgamesResponse() { Boardgames = mapper.Map<IEnumerable<BoardgameDto>>(gameDomain) });
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(typeof(BoardgameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Results<Ok<BoardgameResponse>, EmptyHttpResult, ProblemHttpResult, NotFound>> GetBoardgameById([FromRoute] Guid id)
        {
            var game = await gameRepository.GetById(id);

            if (game == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new BoardgameResponse() { Boardgame = mapper.Map<BoardgameDto>(game) });
        }

        [HttpGet]
        [Route("boardgamenight/{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BoardgamesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Results<Ok<BoardgamesResponse>, EmptyHttpResult, ProblemHttpResult, NotFound>> GetBoardgamesByBoardGameNightId([FromRoute] int id)
        {
            var gameDomain = await gameRepository.GetByBoardGameNightId(id);

            return TypedResults.Ok(new BoardgamesResponse() { Boardgames = mapper.Map<IEnumerable<BoardgameDto>>(gameDomain) });
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<NoContent, NotFound, ProblemHttpResult>> DeleteBoardgame([FromRoute] Guid id)
        {
            var game = await gameRepository.GetById(id);

            if (game == null)
            {
                return TypedResults.NotFound();
            }

            try
            {
                await gameRepository.Delete(id);
                return TypedResults.NoContent();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(BoardgameResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Results<Created<BoardgameResponse>, BadRequest, ProblemHttpResult>> CreateBoardgame([FromBody] CreateBoardgameRequest request)
        {
            if (request == null)
            {
                return TypedResults.BadRequest();
            }

            var gameDomainModel = mapper.Map<Boardgame>(request);

            await gameRepository.Add(gameDomainModel);

            var response = new BoardgameResponse
            {
                Boardgame = mapper.Map<BoardgameDto>(gameDomainModel)
            };

            return TypedResults.Created($"https://localhost:7287/api/game/{gameDomainModel.Id}", response);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ProducesResponseType(typeof(BoardgameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Results<Ok<BoardgameResponse>, NotFound, BadRequest, ProblemHttpResult>> UpdateBoardgame([FromRoute] Guid id, [FromBody] UpdateBoardgameRequest request)
        {
            if (request == null)
            {
                return TypedResults.BadRequest();
            }

            var gameDomainModel = mapper.Map<Boardgame>(request);

            gameDomainModel = await gameRepository.Update(id, gameDomainModel);

            if (gameDomainModel == null)
            {
                return TypedResults.NotFound();
            }

            var response = new BoardgameResponse
            {
                Boardgame = mapper.Map<BoardgameDto>(gameDomainModel)
            };

            return TypedResults.Ok(response);
        }
    }
}
