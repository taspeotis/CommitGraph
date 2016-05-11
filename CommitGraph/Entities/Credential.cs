using System;
using System.Runtime.Serialization;
using CommitGraph.Extensions;
using JetBrains.Annotations;

namespace CommitGraph.Entities
{
    [Serializable]
    public class Credential : ISerializable
    {
        public Credential(string host, string userName, string password)
        {
            Host = host;
            UserName = userName;
            Password = password;
        }

        protected Credential(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            Host = serializationInfo.GetProtectedString(nameof(Host));
            UserName = serializationInfo.GetProtectedString(nameof(UserName));
            Password = serializationInfo.GetProtectedString(nameof(Password));
        }

        [NotNull]
        public string Host { get; set; }

        [NotNull]
        public string UserName { get; set; }

        [NotNull]
        public string Password { get; set; }

        void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            serializationInfo.AddProtectedValue(nameof(Host), Host);
        }

        public override bool Equals(object @object)
        {
            if (ReferenceEquals(null, @object)) return false;
            if (ReferenceEquals(this, @object)) return true;

            var that = @object as Credential;

            return that != null && string.Equals(UserName, that.UserName) && string.Equals(Host, that.Host);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (UserName.GetHashCode()*397) ^ Host.GetHashCode();
            }
        }
    }
}