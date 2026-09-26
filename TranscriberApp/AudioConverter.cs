using Microsoft.VisualBasic;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace TranscriberApp
{
    public static class AudioConverter
    {
        public static MemoryStream ConvertMp3To16KhzMonoWav(string mp3FilePath)
        {
            using var reader = new AudioFileReader(mp3FilePath);

            // Resample and convert channels to 16 kHz, Mono
            var targetFormat = new WaveFormat(16000, 16, 1);
            using var resampler = new MediaFoundationResampler(reader, targetFormat)
            {
                ResamplerQuality = 60 // High quality resampling
            };

            var outputStream = new MemoryStream();
            WaveFileWriter.WriteWavFileToStream(outputStream, resampler);
            outputStream.Position = 0;
            return outputStream;
        }
    }
}
