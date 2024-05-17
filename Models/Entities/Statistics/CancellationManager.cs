using System.Threading;

namespace GymBro.Models.Entities.Statistics
{
    public class CancellationManager
    {
        private CancellationTokenSource _cancellationTokenSource;

        public CancellationToken GetCancellationToken()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            return _cancellationTokenSource.Token;
        }

        public void Cancel()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}
