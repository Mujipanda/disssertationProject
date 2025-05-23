using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// holds data for each gravity  
/// </summary>
public struct gravityCompData : IComponentData
{
    public Entity sunPrefab;
    public float gravityStrength;
    public float3 gravityCentre;
    public float3 simulationBounds;
    public float centreRadius;
}
