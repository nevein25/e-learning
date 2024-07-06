using AutoMapper;
using e_learning.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_learning.Application.Rates.DTOs
{
    internal class RatesProfile : Profile
    {
        public RatesProfile()
        {
            CreateMap<RateByUserDto, Rate>();
            CreateMap<Rate, RateByUserDto>();

        }
    }
}
