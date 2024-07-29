using AutoMapper;
using BLL.Models;
using BLL.Grpc;
using DAL.Entities;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile(Identity.IdentityClient identityClient)
        {
            CreateMap<DrivingLicense, DrivingLicenseModel>()
                .ForMember(dlm => dlm.DriverId, dl => dl.MapFrom(x => x.DriverId))
                .ForMember(dlm => dlm.IssueDate, dl => dl.MapFrom(x => x.IssueDate))
                .ForMember(dlm => dlm.ExpirationDate, dl => dl.MapFrom(x => x.ExpirationDate))  
                .ReverseMap();

            CreateMap<User, UserModel>()
                .ForMember(um => um.Tickets, u => u.MapFrom(x => x.Tickets))
                .ForMember(um => um.Warrants, u => u.MapFrom(x => x.WarrantsOn))
                .ForMember(um => um.Reports, u => u.MapFrom(x => x.Reports));

            CreateMap<UserModel, UserSearchModel>()
                .ReverseMap();

            CreateMap<Ticket, TicketModel>()
                .ForMember(tm => tm.ViolatorId, t => t.MapFrom(x => x.ViolatorId))
                .ForMember(tm => tm.IssueTime, t => t.MapFrom(x => x.Report.DateOfIssuing))
                .ForMember(tm => tm.Description, t => t.MapFrom(x => x.Report.Description));

            CreateMap<User, UserSearchModel>();
                /*.AfterMap(async (user, userModel, context) =>
                {
                    var tempUser = await identityClient.GetPersonAsync(new() { PersonId = user.IdentityId });
                    userModel.Email = tempUser.Email;
                    userModel.Name = tempUser.Username;
                });*/

            CreateMap<CaseFile, CaseFileModel>()
                .ForMember(cfm => cfm.Warrants, cf => cf.MapFrom(cf => cf.Warrants))
                .ForMember(cfm => cfm.Reports, cf => cf.MapFrom(cf => cf.Reports))
                .ForMember(cfm => cfm.Type, cf => cf.MapFrom(cf => (CaseFileTypeEnum)cf.CaseFileTypeId))
                .AfterMap((src, dest, context) =>
                {
                    dest.ConnectedPersons = src.CaseFileConnections
                        .GroupBy(cfc => cfc.CaseFileConnectionType.Value)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(cfc => context.Mapper.Map<UserSearchModel>(cfc.Person)).AsEnumerable()
                        );
                });

            CreateMap<PersonResponse, UserModel>()
                .ForMember(um => um.Name, x => x.MapFrom(pr => pr.Email))
                .ForMember(um => um.Id, x => x.MapFrom(pr => pr.PersonId));

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
                .ForMember(rm => rm.CaseFileId, r => r.MapFrom(x => x.CaseFileId))
                .ReverseMap();

            CreateMap<CaseFile, CaseFilePreview>()
                .ForMember(cfp => cfp.Type, cf => cf.MapFrom(x => x.CaseFileType.Value))
                .ForMember(cfp => cfp.Suspects, cf => cf.MapFrom(x => x.Warrants.Select(w => w.Suspect)));

            CreateMap<Warrant, WarrantModel>();

            CreateMap<CaseFileConnection, CaseFileConnectionModel>()
                .ForMember(cfcm => cfcm.TypeOfConnection, cfc => cfc.MapFrom(x => x.CaseFileConnectionType.Value))
                .ForMember(cfcm => cfcm.ConnectedPerson, cfc => cfc.MapFrom(x => x.Person));

        }

        private CaseFileTypeEnum ParseCaseFileType(string type)
        {
            return Enum.TryParse(typeof(CaseFileTypeEnum), type, out var result) ? (CaseFileTypeEnum)result : CaseFileTypeEnum.Civil;
        }
    }
}
