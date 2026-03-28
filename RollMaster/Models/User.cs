using Microsoft.AspNetCore.Identity;

namespace RollMaster.Models
{
    public class User : IdentityUser
    {
        public IList<Character> characters = new List<Character>();
    }
}
