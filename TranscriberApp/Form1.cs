using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TranscriberApp
{
    public partial class Form1 : Form
    {
        private string _outputPath = string.Empty;
        private string _fullTranscript = string.Empty;
        private CancellationTokenSource? _cts;
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnTranscribe_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();

            try
            {
                await OnTranscribeButtonClicked(sender, e, _cts.Token);
            }
            catch (OperationCanceledException)
            {
                // Clean exit; all using blocks will now properly run and dispose!
            }
            finally
            {
                _cts.Dispose();
                _cts = null;
            }

        }

        private async Task OnTranscribeButtonClicked(object sender, EventArgs e, CancellationToken cancellationToken)
        {
            using var openFileDialog = new OpenFileDialog
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

                _fullTranscript = await transcriber.TranscribeAudioAsync(openFileDialog.FileName, progress);
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Signal cancellation to abort native execution cleanly
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }

            base.OnFormClosing(e);
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Set the default file extension and filters
                saveFileDialog.Filter = "Text Documents (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.Title = "Select Destination for .txt File";
                saveFileDialog.FileName = $"Transcript-{DateTime.Now.ToString("yyyy-MM-dd-HHmmss")}.txt";

                // Open the dialog and check if the user confirmed the location
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, _fullTranscript, Encoding.UTF8);
                        MessageBox.Show("File successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to save file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
