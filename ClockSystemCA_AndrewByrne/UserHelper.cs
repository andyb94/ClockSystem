using System.Security.Claims;

namespace ClockSystemCA_AndrewByrne
{
    // Can use this in Razor page
    public class UserHelper
    {
        // Enum to use for comparisons with Users Role. Will only need to change here
        // If name ever changes in DB         
        public enum RoleType
        {
            Admin = 1,
            Employee = 2
        }
        // HttpContextAccessor added in Program.cs, needed to access cookie info. Dependency Injection
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserHelper(IHttpContextAccessor httpContextAccessor) { 
            _httpContextAccessor = httpContextAccessor;
        }

        // Get current user, based on cookie
        public int GetCurrentUserId()
        {
            // ? used for possible null, will cause any part of _httpContextAccessor
            // to be null and prevent attempting to further check, in this scenario
            // tryParse will return a false response
            if(int.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value, out int id))
            {
                return id;
            }
            // No user will ever have -1, will be considered a failure
            return -1;
        }

        // Get current user role, based on cookie
        public string GetCurrentUserRole()
        {
            // ? used for possible null, will cause any part of _httpContextAccessor
            // to be null and prevent attempting to further check
            var role = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
            // If null allow Null return, role will either be null or string
            return role;
        }
    }
}
