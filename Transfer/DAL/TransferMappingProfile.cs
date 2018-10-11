using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.Models;
using Transfer.ViewModels;
using AutoMapper;

namespace Transfer.DAL
{
    public class TransferMappingProfile : Profile
    {
        public TransferMappingProfile()
        {
            CreateMap<Partner, PartnerViewModel>()
                .ReverseMap();

            CreateMap<Agency, AgencyViewModel>()
                .ReverseMap();

            CreateMap<Agency, AgencyWithIdViewModel>()
                //.ForSourceMember(m => m.AgencyPartners, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Vehicle, VehicleViewModel>()
                .ReverseMap();

            CreateMap<Partner, PartnerAddViewModel>()
                .ReverseMap();
        }
    }
}
