namespace Spawners
{
    public interface ISpawner
    {
        int TotalCount { get; }
        int SpawnedCount { get; }
        int AliveCount { get; }
    }
}