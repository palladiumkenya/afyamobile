using System;

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
    }
}