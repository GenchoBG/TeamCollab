using AutoMapper;

namespace TeamCollab.Web.Infrastructure.Mapper
{
    public interface ICustomMapping
    {
        void ConfigureMapping(Profile profile);
    }
}
