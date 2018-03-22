using System;
using System.Linq;

namespace LiveHTS.SharedKernel.Custom
{
    public static class TypeExtentions
    {
        /// <summary>
        /// Determines if a nullable Guid (Guid?) is null or Guid.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this Guid? guid)
        {
            if (null == guid)
                return true;

            if (guid.HasValue && guid.Value == Guid.Empty)
                return true;

            if (!guid.HasValue)
                return true;

            return false;
        }

        /// <summary>
        /// Determines if Guid is Guid.Empty
        /// </summary>
        public static bool IsNullOrEmpty(this Guid guid)
        {
            return guid == Guid.Empty;
        }

        public static bool IsSameAs(this object s, object other)
        {
            if (null == s)
                s = string.Empty;

            if (null == other)
                other=string.Empty;

            return s.ToString().ToLower().Trim() == other.ToString().ToLower().Trim();
        }

        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}