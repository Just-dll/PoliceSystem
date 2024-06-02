using AngularApp1.Server.Data;
using AngularApp1.Server.Models;
using AutoMapper;
using BLL.Models;
using PoliceDAL.Entities;
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
                .ForMember(dlm => dlm.IssueDate, dl => dl.MapFrom(x => x.IssueDate))
                .ForMember(dlm => dlm.ExpirationDate, dl => dl.MapFrom(x => x.ExpirationDate))  
                .ReverseMap();

            //CreateMap<User, UserModel>()
            //    .ForMember(um => um.TicketIds, u => u.MapFrom(x => x.Tickets.Select(t => t.Id)))
            //    .ReverseMap();

            CreateMap<Ticket, TicketModel>()
                .ForMember(tm => tm.ViolatorId, t => t.MapFrom(x => x.ViolatorId))
                .ForMember(tm => tm.IssueTime, t => t.MapFrom(x => x.Report.DateOfIssuing))
                .ForMember(tm => tm.Description, t => t.MapFrom(x => x.Report.Description));

            CreateMap<User, UserSearchModel>();

            CreateMap<CaseFile, CaseFileModel>()
                .ForMember(cfm => cfm.Warrants, cf => cf.MapFrom(cf => cf.Warrants))
                .ForMember(cfm => cfm.Reports, cf => cf.MapFrom(cf => cf.Reports))
                .ForMember(cfm => cfm.Type, cf => cf.MapFrom(cf => (CaseFileTypeEnum)cf.CaseFileTypeId));

            CreateMap<CaseFileModel, CaseFile>()
                .ForMember(cf => cf.Reports, cfm => cfm.Ignore())
                .ForMember(cf => cf.Warrants, cfm => cfm.Ignore())
                .ForMember(cf => cf.CaseFileTypeId, cfm => cfm.MapFrom(cfm => ParseCaseFileType(cfm.Type)));

            CreateMap<Ticket, PersonTicketModel>()
                .ForMember(ptm => ptm.Description, t => t.MapFrom(x => x.Report.Description))
                .ForMember(ptm => ptm.IssueTime, t => t.MapFrom(x => x.Report.DateOfIssuing));

            CreateMap<Report, ReportModel>()
                .ForMember(rm => rm.Description, r => r.MapFrom(x => x.Description))
                .ForMember(rm => rm.DateOfReport, r => r.MapFrom(x => x.DateOfIssuing))
                .ForMember(rm => rm.ReportedLocation, r => r.MapFrom(x => x.ReportFileLocation))
                .ForMember(rm => rm.ReporterId, r => r.MapFrom(x => x.IssuerId))
                .ReverseMap();

            CreateMap<Warrant, WarrantModel>();


        }

        private CaseFileTypeEnum ParseCaseFileType(string type)
        {
            return Enum.TryParse(typeof(CaseFileTypeEnum), type, out var result) ? (CaseFileTypeEnum)result : CaseFileTypeEnum.Civil;
        }
    }
}
