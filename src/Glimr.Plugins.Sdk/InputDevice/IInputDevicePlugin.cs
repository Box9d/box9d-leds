﻿using System.Threading;
using Glimr.Plugins.Sdk.Context;

namespace Glimr.Plugins.Sdk.InputDevice
{
    public interface IInputDevicePlugin : IPlugin
    {
        void Run(IInputDevicePluginContext pluginContext);
    }
}
