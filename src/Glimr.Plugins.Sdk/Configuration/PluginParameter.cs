using System;

namespace Glimr.Plugins.Sdk.Configuration
{
    internal class PluginParameter
    {
        internal string Name { get; }

        internal Type Type { get; }

        internal PluginParameter(string identifier, Type type)
        {
            Name = identifier;
            Type = type;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is PluginParameter)
            {
                var other = (PluginParameter)obj;

                return Name == other.Name && Type == other.Type;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() * 5 + Type.GetHashCode() * 23;
        }
    }
}
