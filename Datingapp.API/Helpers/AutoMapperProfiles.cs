using System.Linq;
using AutoMapper;
using Datingapp.API.Models;

namespace Datingapp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(){
            CreateMap<user, DTO.UsersForListDto>().ForMember(dst => dst.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            }).ForMember(dst => dst.Age, opt => {
                opt.ResolveUsing(d => d.DateOfBirth.CalcAge());
            });     
            CreateMap<user, DTO.UserForDetailDto>().ForMember(dst => dst.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            }).ForMember(dst => dst.Age, opt => {
                opt.ResolveUsing(d => d.DateOfBirth.CalcAge());
            }); 
            CreateMap<Photo, DTO.PhotoForDetailDto>();
            CreateMap<DTO.UserForUpdateDTO, user>();
            CreateMap<Photo, DTO.PhotoForReturnDTO>();
            CreateMap<DTO.PhotoForCreateDTO, Photo>();
            CreateMap<DTO.UserForRegisterDTO, user>();
            CreateMap<DTO.MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, DTO.MessageToReturnDto>()
            .ForMember(m => m.SenderPhotoUrl, opt => opt.MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(m => m.RecipientPhotoUrl, opt => opt.MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
            }
    }
}