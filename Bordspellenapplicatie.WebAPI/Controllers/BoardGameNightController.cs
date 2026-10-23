using AutoMapper;
using Bordspellenapplicatie.WebAPI.Models.DTO;
using Bordspellenapplicatie.WebAPI.Models.Request.BoardGameNight;
using Bordspellenapplicatie.WebAPI.Models.Response;
using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bordspellenapplicatie.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGameNightController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBoardGameNightRepository boardGameNightRepository;
        private readonly IUserRepository userRepository;

        public BoardGameNightController(IMapper mapper, IBoardGameNightRepository boardGameNightRepository, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.boardGameNightRepository = boardGameNightRepository;
            this.userRepository = userRepository;
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

        [HttpPost("{id:int}/register")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> RegisterBoardGameNight(int id)
        {
            var identityId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                             ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(identityId))
            {
                return TypedResults.Unauthorized();
            }

            var user = userRepository.GetByIdentityId(identityId);

            if (user == null)
            {
                return TypedResults.Unauthorized();
            }

            var boardGameNight = await boardGameNightRepository.GetById(id);

            if (boardGameNight == null)
            {
                return TypedResults.NotFound();
            }

            var registeredUsers = boardGameNight.UserBoardGameNights?.ToList()
                                  ?? new List<UserBoardGameNight>();

            if (registeredUsers.Any(r => r.UserId == user.Id))
            {
                return TypedResults.Problem(
                    detail: "Je bent al aangemeld voor deze bordspellenavond.",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            if (registeredUsers.Count >= boardGameNight.MaxPlayers)
            {
                return TypedResults.Problem(
                    detail: "Deze bordspellenavond is vol.",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            var age = boardGameNight.DateTime.Year - user.DateOfBirth.Year;

            if (user.DateOfBirth.Date >
                boardGameNight.DateTime.Date.AddYears(-age))
            {
                age--;
            }

            if (boardGameNight.Is18Plus && age < 18)
            {
                return TypedResults.Problem(
                    detail: "Je moet minimaal 18 jaar zijn voor deze bordspellenavond.",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            var allBoardGameNights = await boardGameNightRepository.GetAll();

            var alreadyRegisteredThatDay = allBoardGameNights.Any(bgn =>
                bgn.Id != boardGameNight.Id &&
                bgn.DateTime.Date == boardGameNight.DateTime.Date &&
                bgn.UserBoardGameNights != null &&
                bgn.UserBoardGameNights.Any(r => r.UserId == user.Id)
            );

            if (alreadyRegisteredThatDay)
            {
                return TypedResults.Problem(
                    detail: "Je bent al aangemeld voor een andere bordspellenavond op deze dag.",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            var registration = new UserBoardGameNight
            {
                BoardGameNightId = boardGameNight.Id,
                UserId = user.Id
            };

            registeredUsers.Add(registration);

            boardGameNight.UserBoardGameNights = registeredUsers;

            await boardGameNightRepository.Update(
                boardGameNight.Id,
                boardGameNight
            );

            return TypedResults.Created(
                $"/api/BoardGameNight/{boardGameNight.Id}",
                new
                {
                    Success = true,
                    Message = "Je bent succesvol aangemeld voor de bordspellenavond."
                }
            );
        }
    }
}
