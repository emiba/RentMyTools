using AutoMapper;

namespace RentMyTools.Api.Infrastructure.DataOperations
{
    public interface INeedMapper
    {
        IMapper Mapper { get; set; }
    }
}
