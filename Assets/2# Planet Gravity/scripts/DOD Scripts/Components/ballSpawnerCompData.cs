using Unity.Entities;
using Unity.Mathematics;

public struct ballSpawnerCompData : IComponentData
{
    public Entity entityPrefab;
    public float2 bounds;
    public float2 ballSizeRange;
    public int numBalls;
}
