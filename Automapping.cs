using AutoMapper;
using next_generation.Models;
using next_generation.Models.DTO;

namespace next_generation
{
    public class Automapping : Profile
    {

        public Automapping()
        {
            CreateMap<NexGenUsers, NextGenUsersDto>();
            CreateMap<NextGenUsersDto, NexGenUsers>();

            CreateMap<NexGenUsers, NextGenUsersCreateDto>().ReverseMap();
            CreateMap<NexGenUsers, NextGenUsersUpdateDto>().ReverseMap();
        }
    }
}
