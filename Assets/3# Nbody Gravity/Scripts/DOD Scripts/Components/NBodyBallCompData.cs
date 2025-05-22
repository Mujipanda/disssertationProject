using Unity.Entities;
using Unity.Mathematics;

public struct NBodyBallCompData : IComponentData
{
    public float3 ballVelocity;
    public float ballMass;
}