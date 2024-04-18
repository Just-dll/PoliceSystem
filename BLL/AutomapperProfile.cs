using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using AutoMapper;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {

            CreateMap<DrivingLicense, DrivingLicenseModel>()
                .ForMember(dlm => dlm.DriverId, dl => dl.MapFrom(x => x.DriverId))
                .ReverseMap();

            //CreateMap<User, UserModel>()
            //    .ForMember(um => um.TicketIds, u => u.MapFrom(x => x.Tickets.Select(t => t.Id)))
            //    .ReverseMap();
            CreateMap<Ticket, TicketModel>()
                .ForMember(tm => tm.ViolatorId, t => t.MapFrom(x => x.ViolatorId))
                .ForMember(tm => tm.IssueTime, t => t.MapFrom(x => x.Report.DateOfIssuing))
                .ForMember(tm => tm.Description, t => t.MapFrom(x => x.Report.Description));
            
            //CreateMap<TicketModel, Ticket>()
            //    .ForMember(t => t.Report, tm => tm.)
        }
    }
}
