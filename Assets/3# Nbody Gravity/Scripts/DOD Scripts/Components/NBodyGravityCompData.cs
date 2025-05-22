
using Unity.Entities;
using Unity.Mathematics;

public struct NBodyGravityCompData : IComponentData
{
    public float gravityStrength;
    public float3 simulationBounds;
}