using AutoMapper;

namespace StudentPortalDemo.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModels.Student, DataModels.Student>().ReverseMap();

            CreateMap<DataModels.Gender, DataModels.Gender>().ReverseMap();

            CreateMap<DataModels.Address, DataModels.Address>().ReverseMap();
        }
    }
}
