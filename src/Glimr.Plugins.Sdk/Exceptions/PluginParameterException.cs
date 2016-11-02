﻿using System;

namespace Glimr.Plugins.Plugins.Exceptions
{
    public sealed class PluginParameterException : ArgumentException
    {
        public string ParameterName { get; }

        internal PluginParameterException(string parameterName)
            : base(string.Format("Plugin parameter was not found", parameterName))
        {
            ParameterName = parameterName;
        }
    }
}