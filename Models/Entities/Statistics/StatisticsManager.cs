using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymBro.Models.Entities.Statistics
{
    public class StatisticsManager
    {
        public async Task<List<StatisticsExerciseStatus>> GenerateStatisticsAsync(List<ExerciseStatus> exerciseStatuses, IProgress<string> progressReporter, CancellationToken cancellationToken)
        {
            var statistics = new List<StatisticsExerciseStatus>();

            try
            {
                // Tutaj rozpocznij obliczenia w wielu wątkach
                await Task.Run(() =>
                {
                    foreach (var exerciseStatus in exerciseStatuses)
                    {
                        // Sprawdzamy, czy został zażądany cancel
                        if (cancellationToken.IsCancellationRequested)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                        }

                        // Symulujemy obliczenia
                        var statisticsExerciseStatus = new StatisticsExerciseStatus(exerciseStatus, DateTime.Now.Date);
                        statistics.Add(statisticsExerciseStatus);

                        // Raportowanie postępu
                        progressReporter.Report($"Obliczenia dla {exerciseStatus.Exercise.Name} zakończone.");
                    }
                }, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // Obsługa anulowania
                progressReporter.Report("Obliczenia zostały anulowane.");
            }
            catch (Exception ex)
            {
                // Obsługa innych wyjątków
                progressReporter.Report($"Wystąpił błąd: {ex.Message}");
            }

            return statistics;
        }
    }
}
