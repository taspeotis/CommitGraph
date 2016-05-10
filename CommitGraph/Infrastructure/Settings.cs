using System.Configuration;

namespace CommitGraph.Infrastructure
{
    internal abstract class Settings : ApplicationSettingsBase
    {
        protected TValue Get<TValue>(string propertyName)
        {
            return (TValue) this[propertyName];
        }

        protected void SetImmediate<TValue>(string propertyName, TValue value)
        {
            this[propertyName] = value;

            Save();
        }
    }
}