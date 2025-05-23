using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// holds the data for each ball Spawner
/// </summary>
public struct ballSpawnerCompData : IComponentData
{
    public Entity entityPrefab;
    public float2 bounds;
    public float2 ballSizeRange;
    public int numBalls;
}
