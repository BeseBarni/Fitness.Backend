using AutoMapper;
using Fitness.Backend.Application.DataContracts.Entity;

namespace Fitness.Backend.Application.DataContracts.Models.ViewModels
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<User, UserData>();
            CreateMap<UserData, User>();

            CreateMap<Lesson, LessonData>();
            CreateMap<LessonData, Lesson>();

            CreateMap<Sport, SportData>();
            CreateMap<SportData, Sport>();

            CreateMap<Instructor, InstructorData>();
            CreateMap<InstructorData, Instructor>();
        }
    }
}
