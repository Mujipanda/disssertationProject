using Unity.Entities;
using UnityEngine;
/// <summary>
/// the entity baker for the ball spawner 
/// </summary>
class NBodySpawnerAuthoring : MonoBehaviour
{
    public GameObject ballPrefab;
    public int numOfBalls;
    public float gravityStrength;
    public Vector2 Bounds;
    public Vector3 simBounds;
    public Vector2 massRange;
    public Vector2 ballSizeRange;
}

class NBodySpawnerBaker : Baker<NBodySpawnerAuthoring>
{
    public override void Bake(NBodySpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        var ballEntity = GetEntity(authoring.ballPrefab, TransformUsageFlags.Dynamic);
        var SpawnerData = new NBodySpawnerCompData
        {
            entityPrefab = ballEntity,
            bounds = authoring.Bounds,
            numBalls = authoring.numOfBalls,
            ballSizeRrange = authoring.ballSizeRange,
            massRange = authoring.massRange,
        };
        AddComponent(entity, SpawnerData);

        var NBodyGravityData = new NBodyGravityCompData
        {
            gravityStrength = authoring.gravityStrength,
            simulationBounds = authoring.simBounds,
        };
        AddComponent(entity, NBodyGravityData);
    }
}
