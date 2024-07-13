using AngularApp1.Server.Models;
using PoliceDAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTests
{
    public class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User? x, User? y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }
            
            return x.IdentityId == y.IdentityId && x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.GetHashCode();
        }
    }

    public class CaseFileComparer : IEqualityComparer<CaseFile>
    {
        public bool Equals(CaseFile? x, CaseFile? y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id && x.CaseFileTypeId == y.CaseFileTypeId && x.InitiationDate == y.InitiationDate;
        }

        public int GetHashCode([DisallowNull] CaseFile obj)
        {
            return obj.GetHashCode();
        }
    }

    public class ReportComparer : IEqualityComparer<Report>
    {
        public bool Equals(Report? x, Report? y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.IssuerId == y.IssuerId && x.Id == y.Id 
                && x.CaseFileId == y.CaseFileId 
                && x.DateOfIssuing == y.DateOfIssuing;
        }

        public int GetHashCode([DisallowNull] Report obj)
        {
            return obj.GetHashCode();
        }
    }

    public class TicketComparer : IEqualityComparer<Ticket>
    {
        public bool Equals(Ticket? x, Ticket? y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id && x.ReportId == y.ReportId && x.ViolatorId == y.ViolatorId;
        }

        public int GetHashCode([DisallowNull] Ticket obj)
        {
            return obj.GetHashCode();
        }

    }
}
