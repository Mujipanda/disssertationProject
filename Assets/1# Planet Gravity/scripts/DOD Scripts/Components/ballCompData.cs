using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// holds individual data for each ball 
/// </summary>
public struct BallCompData : IComponentData
{
    public float3 ballVelocity;
}
