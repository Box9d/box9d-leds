using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Box9.Leds.Core.EventsArguments;
using Glimr.Plugins.ManagementApi.Client;

namespace Glimr.Plugins.ManagementApi.UploadTool
{
    public partial class Upload : Form
    {
        private event EventHandler<StringEventArgs> FinishedUpload;
        private string pluginFolder;

        public Upload()
        {
            InitializeComponent();
        }

        private void Upload_Load(object sender, EventArgs e)
        {
            FinishedUpload += (s, args) =>
            {
                Invoke(new Action(() =>
                {
                    labelStatus.Text = args.Value;
                }));
            };
        }

        private void buttonUploadPluginFolder_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                pluginFolder = folderBrowserDialog.SelectedPath;
                labelFolderName.Text = pluginFolder;
                buttonUploadFiles.Enabled = true;
            }
        }

        private void buttonUploadFiles_Click(object sender, EventArgs e)
        {
            labelStatus.Text = "Uploading...";

            Task<IEnumerable<PluginUploadResult>> uploadTask;

            uploadTask = Task.Run(() => 
            {
                var pluginInstance = PluginInstanceFactory.NewPluginInstance();
                return pluginInstance.SubmitFiles(pluginFolder);
            });

            Task.Run(async() =>
            {
                var result = await uploadTask;

                foreach (var upload in result)
                {
                    FinishedUpload(null, new StringEventArgs(upload.ToString()));
                }
            });
        }
    }
}
