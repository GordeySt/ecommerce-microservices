using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Application.Common.Utilities
{
    public static class DatabaseUtilities
    {
        public static string CreateErrorMessage(IEnumerable<IdentityError> errors) =>
            string.Join(",", errors.Select(x => x.Description));
    }
}
