using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Glimr.Plugins.Core;
using Glimr.Plugins.Plugins.InputDevice;
using Glimr.Plugins.Plugins.Runner;
using Glimr.Plugins.Sdk.Chaining;
using Glimr.Plugins.Sdk.Context;
using Glimr.Plugins.Sdk.Plugins;

namespace Glimr.Plugins.Sdk.EffectPluginTester
{
    public partial class EffectPluginTester : Form
    {
        private readonly IPluginReader reader;
        private IPluginRunner pluginRunner;

        private IEnumerable<IInputDevicePlugin> inputPlugins;
        private IEnumerable<IEffectPlugin> effectPlugins;
        private IEnumerable<IOutputDevicePlugin> outputPlugins;

        public EffectPluginTester()
        {
            InitializeComponent();

            reader = new PluginReader();
            pluginRunner = new PluginRunner();
        }

        private void EffectPluginTester_Load(object sender, EventArgs e)
        {
            inputPlugins = reader.GetAvailablePlugins<IInputDevicePlugin>();
            effectPlugins = reader.GetAvailablePlugins<IEffectPlugin>();
            outputPlugins = reader.GetAvailablePlugins<IOutputDevicePlugin>();

            foreach (var inputPlugin in inputPlugins)
            {
                listBoxAvailableInputPlugins.Items.Add(inputPlugin.Configure().PluginDisplayName);
            }

            foreach (var effectPlugin in effectPlugins)
            {
                listBoxAvailableEffectPlugins.Items.Add(effectPlugin.Configure().PluginDisplayName);
            }

            foreach (var outputPlugin in outputPlugins)
            {
                listBoxAvailableOutputPlugins.Items.Add(outputPlugin.Configure().PluginDisplayName);
            }
        }

        private void listBoxAvailableInputPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateSelectButtonAvailability();
        }

        private void listBoxAvailableEffectPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateSelectButtonAvailability();
        }

        private void listBoxAvailableOutputPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateSelectButtonAvailability();
        }

        private void ValidateSelectButtonAvailability()
        {
            buttonSelectPlugins.Enabled = listBoxAvailableInputPlugins.SelectedIndex > -1
                && listBoxAvailableEffectPlugins.SelectedIndex > -1
                && listBoxAvailableOutputPlugins.SelectedIndex > -1;
        }

        private void buttonSelectPlugins_Click(object sender, EventArgs e)
        {
            var inputPlugin = inputPlugins.Single(p => p.Configure().PluginDisplayName == listBoxAvailableInputPlugins.SelectedItem.ToString());
            var effectPlugin = effectPlugins.Single(p => p.Configure().PluginDisplayName == listBoxAvailableEffectPlugins.SelectedItem.ToString());
            var outputPlugin = outputPlugins.Single(p => p.Configure().PluginDisplayName == listBoxAvailableOutputPlugins.SelectedItem.ToString());

            var effectPluginContext = PluginContextFactory.GenerateInitialEffectPluginContext(effectPlugin);
            effectPluginContext.SetInput("Frequency (Hz)", 1);
            effectPluginContext.SetInput("RGB Value - R", 255);
            effectPluginContext.SetInput("RGB Value - G", 255);
            effectPluginContext.SetInput("RGB Value - B", 255);

            var inputPluginContext = PluginContextFactory.GenerateInitialInputDevicePluginContext(inputPlugin);

            var outputPluginContext = PluginContextFactory.GenerateInitialOutputDevicePluginContext(outputPlugin);
            outputPluginContext.SetInput("Listening port", 8080);
            outputPluginContext.SetInput("Path", "/output");

            var effectPluginChain = PluginChainBuilder
                .CreateEffectPluginChain()
                .AddEffectPlugin(effectPlugin, effectPluginContext)
                .FinishAddingDependencies();

            var processingPluginChain = PluginChainBuilder
                .CreateProcessingPluginChain()
                .AddInputDevicePlugin(inputPlugin, inputPluginContext)
                .SetEffectPluginChain(effectPluginChain)
                .AddOutputDevicePlugin(outputPlugin, outputPluginContext);

            pluginRunner.StartProcessingPluginChain(processingPluginChain);

            buttonSelectPlugins.Enabled = false;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (pluginRunner != null)
            {
                pluginRunner.StopProcessingPluginChain();
            };

            buttonSelectPlugins.Enabled = true;
        }
    }
}
