using System;
using System.Collections.Generic;
using Glimr.Plugins.Sdk;
using Glimr.Plugins.Sdk.InputDevice;

namespace Glimr.Plugins.Core
{
    public interface IPluginReader
    {
        IEnumerable<IInputDevicePlugin> GetAvailableInputDevicePlugins();
    }
}
