using AutoMapper;
using Bordspellenapplicatie.WebAPI.Models.DTO;
using BordspellenApplicatie.Domain.Interfaces;

namespace Bordspellenapplicatie.WebAPI.GraphQL;

public class Query
{
    public async Task<IEnumerable<BoardGameNightDto>> GetBoardGameNights(
        [Service] IBoardGameNightRepository repository,
        [Service] IMapper mapper)
    {
        var boardGameNights = await repository.GetAll();

        return mapper.Map<IEnumerable<BoardGameNightDto>>(boardGameNights);
    }

    public async Task<IEnumerable<BoardgameDto>> GetBoardGames(
        [Service] IGameRepository repository,
        [Service] IMapper mapper)
    {
        var games = await repository.GetAll();

        return mapper.Map<IEnumerable<BoardgameDto>>(games);
    }
}