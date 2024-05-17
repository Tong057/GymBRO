using System;
using System.Threading;

namespace GymBro.Models.Entities.Statistics
{
    public class ProgressReporter : IProgress<string>
    {
        private readonly ProgressBar _progressBar;

        public ProgressReporter(ProgressBar progressBar)
        {
            _progressBar = progressBar;
        }

        public void Report(string message)
        {
            // Wyświetlanie komunikatu
            Console.WriteLine($"[INFO] {message}");

            // Aktualizacja paska postępu
            _progressBar.Update();
        }
    }

    public class ProgressBar
    {
        private int _progress = 0;

        public void Update()
        {
            // Aktualizacja paska postępu
            Interlocked.Increment(ref _progress);
            Console.WriteLine($"Progress: {_progress}%");
        }
    }
}
