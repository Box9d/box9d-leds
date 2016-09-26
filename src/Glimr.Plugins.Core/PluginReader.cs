using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Glimr.Plugins.Core.Exceptions;
using Glimr.Plugins.Plugins;

namespace Glimr.Plugins.Core
{
    public class PluginReader : IPluginReader
    {
        public IEnumerable<T> GetAvailablePlugins<T>() where T : IPlugin
        {
            var plugins = new List<T>();

            foreach (var pluginFolder in Directory.EnumerateDirectories(PluginFolder.GetRootFolder()))
            {
                foreach (var file in Directory.EnumerateFiles(pluginFolder))
                {
                    if (file.EndsWith(".dll"))
                    {
                        var assemblyName = AssemblyName.GetAssemblyName(file);
                        var assembly = Assembly.Load(assemblyName);
                        var pluginTypes = assembly.GetTypes()
                            .Where(t => typeof(T).IsAssignableFrom(t) && t.IsClass && !t.IsInterface);

                        foreach (var pluginType in pluginTypes)
                        {
                            try
                            {
                                var plugin = (T)Activator.CreateInstance(pluginType);
                                plugins.Add(plugin);
                            }
                            catch (Exception ex)
                            {
                                throw new PluginActivationException(pluginType, ex);
                            } 
                        }
                    }
                }
            }

            return plugins;
        }
    }
}
