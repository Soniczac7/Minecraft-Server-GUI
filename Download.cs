using System.Diagnostics;
using System.Net;

namespace Minecraft_Server_GUI
{
    public partial class Download : Form
    {
        private string url;
        private string location;

        public Download(string downloadUrl, string downloadLocation)
        {
            InitializeComponent();
            url = downloadUrl;
            location = downloadLocation;
            if (url == null || url == "")
            {
                MessageBox.Show("Download URL is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            if (location == null || location == "")
            {
                MessageBox.Show("Download location is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            Download();

            void Download()
            {
                WebClient wc = new();
                wc.OpenRead(url);
                Int64 bytes_total = Convert.ToInt64(wc.ResponseHeaders["Content-Length"]);
                string name = wc.ResponseHeaders["content-disposition"].Remove(0, 29);
                label2.Text = name;
                label3.Text = bytes_total.ToString() + " Bytes";
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                try
                {
                    wc.DownloadFileAsync(
                        // Download URL
                        new System.Uri(url),
                        // Path to save
                        location
                    );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                    DialogResult result = MessageBox.Show(ex.Message + "\nRetry download?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (result == DialogResult.Yes)
                    {
                        Download();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }

            // Event to track the progress
            void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
            {
                progressBar1.Value = e.ProgressPercentage;
                if (e.ProgressPercentage == 100)
                {
                    this.Close();
                }
            }
        }
    }
}