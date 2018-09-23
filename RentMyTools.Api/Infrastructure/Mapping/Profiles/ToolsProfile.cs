using AutoMapper;
using RentMyTools.Models;

namespace RentMyTools.Api.Infrastructure.Mapping.Profiles
{
    public class ToolsProfile : Profile
    {
        public ToolsProfile()
        {
            CreateMap<Tool, Tool>()
                .ForMember(x => x.Id, o => o.Ignore());
        }
    }
}
