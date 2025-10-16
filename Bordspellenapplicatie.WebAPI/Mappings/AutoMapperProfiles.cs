using AutoMapper;
using Bordspellenapplicatie.WebAPI.Models.DTO;
using Bordspellenapplicatie.WebAPI.Models.Request;
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
            .ForMember(dest => dest.OrganizerId, opt => opt.MapFrom(src => src.Organizer.Id))
            .ForMember(dest => dest.ReviewIds, opt => opt.MapFrom(src => src.Reviews.Select(review => review.Id)))
            .ForMember(dest => dest.UserBoardGameNightIds, opt => opt.MapFrom(src => src.UserBoardGameNights.Select(userBoardGameNight => userBoardGameNight.Id)))
            .ReverseMap();

            CreateMap<CreateBoardGameNight, BoardGameNight>().ReverseMap();

            CreateMap<UpdateBoardGameNightRequest, BoardGameNight>().ReverseMap();
        }
    }
}
