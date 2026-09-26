using System.Windows.Forms;
using System.IO;

namespace TranscriberApp
{
    public partial class Form1 : Form
    {
        private string _outputPath = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OnTranscribeButtonClicked(sender, e);
        }

        private async void OnTranscribeButtonClicked(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Audio Files (*.mp3)|*.mp3"
            };

            var diagResult = openFileDialog.ShowDialog();
            if (diagResult != DialogResult.OK) return;

            btnTranscribe.Enabled = false;
            txtResults.Clear();

            var progress = new Progress<string>(line =>
            {
                txtResults.AppendText(line + Environment.NewLine);
            });

            try
            {
                //var modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "ggml-base.bin");
                var modelPath = Properties.Settings.Default.ModelPath;
                var transcriber = new TranscriberService(modelPath);

                var fullTranscript = await transcriber.TranscribeAudioAsync(openFileDialog.FileName, progress);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                btnTranscribe.Enabled = true;
            }
        }

        private void txtResults_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
