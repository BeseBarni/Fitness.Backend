using AutoMapper;
using Fitness.Backend.Application.DataContracts.Models.Entity;

namespace Fitness.Backend.WebApi.ViewModels
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<User,UserData>();
            CreateMap<UserData, User>();
            CreateMap<Lesson,LessonData>();
            CreateMap<LessonData,Lesson>();
            CreateMap<Sport,SportData>();
            CreateMap<SportData,Sport>();
            CreateMap<Instructor,InstructorData>();
            CreateMap<InstructorData, Instructor>();
        }
    }
}
