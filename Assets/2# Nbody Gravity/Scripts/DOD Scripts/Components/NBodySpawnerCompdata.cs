
using System.Numerics;
using Unity.Entities;
using Unity.Mathematics;

public struct NBodySpawnerCompData : IComponentData
{
    public Entity entityPrefab;
    public int numBalls;
    public float2 bounds;
    public float2 ballSizeRrange;
    public float2 massRange;
    
}