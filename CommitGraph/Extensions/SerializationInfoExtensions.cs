using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace CommitGraph.Extensions
{
    public static class SerializationInfoExtensions
    {
        private const DataProtectionScope DataProtectionScope =
            System.Security.Cryptography.DataProtectionScope.CurrentUser;

        public static void AddProtectedValue(this SerializationInfo serializationInfo, string name, string value)
        {
            if (value != null)
            {
                var valueBytes = Encoding.UTF8.GetBytes(value);
                var protectedBytes = ProtectedData.Protect(valueBytes, null, DataProtectionScope);

                serializationInfo.AddValue(name, protectedBytes);
            }
            else
            {
                serializationInfo.AddValue(name, null);
            }
        }

        public static string GetProtectedString(this SerializationInfo serializationInfo, string name)
        {
            var protectedBytes = (byte[]) serializationInfo.GetValue(name, typeof(byte[]));

            if (protectedBytes != null)
            {
                var unprotectedBytes = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope);

                return Encoding.UTF8.GetString(unprotectedBytes);
            }

            return null;
        }
    }
}