using Microsoft.AspNetCore.Identity;

namespace AngularApp1.Server.Models
{
    public class Position : IdentityRole<int>
    {

    }

    public enum JudiciaryPosition
    {
        Prosecutor,
        Judge,
        Attourney
    }
}
