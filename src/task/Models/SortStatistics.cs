namespace task.Models;

class SortStatistics
{
    public int Comparisons { get; set; } = 0;
    public int Swaps { get; set; } = 0;
    public int RecursiveCalls { get; set; } = 0;
    public TimeSpan ExecutionTime { get; set; } = TimeSpan.Zero;

    public void ResetMetrics()
    {
        Comparisons = 0;
        Swaps = 0;
        RecursiveCalls = 0;
        ExecutionTime = TimeSpan.Zero;
    }
}