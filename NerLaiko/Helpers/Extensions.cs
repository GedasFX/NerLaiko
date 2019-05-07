using System.Security.Claims;

namespace NerLaiko.Helper
{
    public static class Extensions
    {
        public static string GetId(this ClaimsPrincipal user) => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
