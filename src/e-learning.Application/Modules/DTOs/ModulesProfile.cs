using AutoMapper;
using e_learning.Domain.Entities;

namespace e_learning.Application.Modules.DTOs
{
    internal class ModulesProfile : Profile
    {
        public ModulesProfile()
        {
            CreateMap(typeof(Module), typeof(ModuleDto)).ReverseMap();

        }
    }
}
