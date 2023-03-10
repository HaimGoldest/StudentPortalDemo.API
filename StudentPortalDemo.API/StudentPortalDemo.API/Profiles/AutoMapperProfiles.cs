using AutoMapper;
using StudentPortalDemo.API.DomainModels;
using StudentPortalDemo.API.Profiles.AfterMaps;

namespace StudentPortalDemo.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModels.Student, DomainModels.Student>().ReverseMap();

            CreateMap<DataModels.Gender, DomainModels.Gender>().ReverseMap();

            CreateMap<DataModels.Address, DomainModels.Address>().ReverseMap();

            CreateMap<UpdateStudentRequest, DataModels.Student>()
                .AfterMap<UpdateStudentRequestAfterMap>();

            CreateMap<AddStudentRequest, DataModels.Student>()
                .AfterMap<AddStudentRequestAfterMap>();
        }
    }
}
