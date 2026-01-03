using AutoMapper;
using Bordspellenapplicatie.WebAPI.Models.DTO;
using Bordspellenapplicatie.WebAPI.Models.Request.BoardGame;
using Bordspellenapplicatie.WebAPI.Models.Request.BoardGameNight;
using Bordspellenapplicatie.WebAPI.Models.Request.Review;
using Bordspellenapplicatie.WebAPI.Models.Response;
using BordspellenApplicatie.Domain.Models;

namespace Bordspellenapplicatie.WebAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BoardGameNight, BoardGameNightDto>()
            .ForMember(dest => dest.RegisteredUsersId, opt => opt.MapFrom(src => src.RegisteredUsers.Select(user => user.Id)))
            .ForMember(dest => dest.Organizer, opt => opt.MapFrom(src => src.Organizer))
            .ForMember(dest => dest.ReviewIds, opt => opt.MapFrom(src => src.Reviews.Select(review => review.Id)))
            .ForMember(dest => dest.GamesId, opt => opt.MapFrom(src => src.Games.Select(game => game.Id)))
            .ForMember(dest => dest.UserBoardGameNightIds, opt => opt.MapFrom(src => src.UserBoardGameNights.Select(userBoardGameNight => userBoardGameNight.Id)))
            .ReverseMap();

            //CreateMap<CreateBoardGameNight, BoardGameNight>().ReverseMap();

            //CreateMap<UpdateBoardGameNightRequest, BoardGameNight>().ReverseMap();


            CreateMap<CreateBoardGameNight, BoardGameNight>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ForMember(dest => dest.UserBoardGameNights, opt => opt.Ignore())
                .ForMember(dest => dest.RegisteredUsers, opt => opt.Ignore())
                .ForMember(dest => dest.IsSignedUp, opt => opt.Ignore())
                .ForMember(dest => dest.Games, opt => opt.Ignore());

            CreateMap<UpdateBoardGameNightRequest, BoardGameNight>()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<Review, ReviewDto>()
                //.ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Author.Id))
                //.ForMember(dest => dest.BoardGameNightId, opt => opt.MapFrom(src => src.BoardGameNight.Id))
                .ReverseMap();

            CreateMap<CreateReviewRequest, Review>().ReverseMap();
                //.ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Author.Id))
                //.ForMember(dest => dest.BoardGameNightId, opt => opt.MapFrom(src => src.BoardGameNight.Id))

            CreateMap<UpdateReviewRequest, Review>().ReverseMap()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Boardgame, BoardgameDto>().ReverseMap();
            CreateMap<CreateBoardgameRequest, Boardgame>().ReverseMap();
            CreateMap<UpdateBoardgameRequest, Boardgame>().ReverseMap()
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
