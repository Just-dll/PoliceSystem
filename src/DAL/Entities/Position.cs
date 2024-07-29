using Microsoft.AspNetCore.Identity;

namespace DAL.Entities;

public class Position : IdentityRole<int>
{

}

public enum JudiciaryPosition
{
    Prosecutor,
    Judge,
    Attourney
}
