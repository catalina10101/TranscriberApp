using System.Text;
using Whisper.net;
using Whisper.net.Ggml;
using static System.Windows.Forms.LinkLabel;

namespace TranscriberApp
{
    public class TranscriberService
    {
        private readonly string _modelPath;

        public TranscriberService(string modelPath)
        {
            _modelPath = modelPath;
        }

        public async Task<string> TranscribeAudioAsync(string mp3Path,
            IProgress<string>? progress = null,
            CancellationToken cancellationToken = default)
        {
            // 1. Convert MP3 to the expected 16kHz mono WAV stream
            using var wavStream = AudioConverter.ConvertMp3To16KhzMonoWav(mp3Path);

            // 2. Initialize Whisper runtime from the downloaded GGML file
            using var factory = WhisperFactory.FromPath(_modelPath);

            // 3. Configure the processor for Spanish
            using var processor = factory.CreateBuilder()
                .WithLanguage("es")             // Force Spanish transcription
                .WithProbabilities()            // Optional: confidence scoring
                .Build();

            var fullTranscript = new StringBuilder();

            // 4. Stream segments as they are decoded
            await foreach (var segment in processor.ProcessAsync(wavStream, cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                //string line = $"[{segment.Start:hh\\:mm\\:ss} -> {segment.End:hh\\:mm\\:ss}] {segment.Text}";
                string line = segment.Text.TrimStart();
                progress?.Report(line);
                fullTranscript.AppendLine(segment.Text);
            }
            progress?.Report("------------********** FIN DEL AUDIO **********------------");
            return fullTranscript.ToString();
        }
    }
}