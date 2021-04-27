using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using question.domain;

namespace question.api.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<string, Choice>()
              .ForMember(dest => dest.ChoiceName, m => m.MapFrom(src => src));

            CreateMap<QuestionDTO, Question>()
              .ForMember(
                    dest => dest.Choices,
                    m => m.MapFrom(src => src.Choices));


            // means you want to map from User to UserDTO
        }
    }
}
