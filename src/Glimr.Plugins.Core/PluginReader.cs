using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Glimr.Plugins.Sdk;
using Glimr.Plugins.Sdk.InputDevice;

namespace Glimr.Plugins.Core
{
    public class PluginReader : IPluginReader
    {
        public IEnumerable<IInputDevicePlugin> GetAvailableInputDevicePlugins()
        {
            foreach (var pluginFolder in Directory.EnumerateDirectories(PluginFolder.GetRootFolder()))
            {
                foreach (var file in Directory.EnumerateFiles(pluginFolder))
                {
                    if (file.EndsWith(".dll"))
                    {
                        var assemblyName = AssemblyName.GetAssemblyName(file);
                        var assembly = Assembly.Load(assemblyName);
                        var pluginTypes = assembly.GetTypes()
                            .Where(t => typeof(IInputDevicePlugin).IsAssignableFrom(t) && t.IsClass);

                        foreach (var pluginType in pluginTypes)
                        {
                            var plugin = (IInputDevicePlugin)Activator.CreateInstance(pluginType);
                            yield return plugin;
                        }
                    }
                }
            }
        }
    }
}
