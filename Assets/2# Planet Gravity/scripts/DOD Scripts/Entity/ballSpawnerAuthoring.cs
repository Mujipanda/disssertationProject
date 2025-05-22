using Unity.Entities;
using UnityEngine;

public class ballSpawnerAuthoring : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject sunPrefab;
    public int numOfBalls = 10000;
    public Vector2 bounds = new Vector2(4, 4);
    public Vector2 ballSizeRange = new Vector2(0.01f, 0.02f);
    public float gravityStrength = 0.03f;
    public Vector3 simulationBounds = new Vector3(4, 4, 4);
    public float centreRadius = 0.1f;
}

public class ballSpawnBaker : Baker<ballSpawnerAuthoring>
{
    public override void Bake(ballSpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);


        var sunEntity = GetEntity(authoring.sunPrefab, TransformUsageFlags.Dynamic);
        var gravitySettings = new gravityCompData
        {
            sunPrefab = sunEntity,
            gravityStrength = authoring.gravityStrength,
            gravityCentre = authoring.sunPrefab.transform.position,
            simulationBounds = authoring.simulationBounds,
            centreRadius = authoring.centreRadius,
        };
        AddComponent(entity, gravitySettings);


        var ballEntity = GetEntity(authoring.ballPrefab,TransformUsageFlags.Dynamic);
        var spawner = new ballSpawnerCompData
        {
            entityPrefab = ballEntity,
            bounds = authoring.bounds,
            ballSizeRange = authoring.ballSizeRange,
            numBalls = authoring.numOfBalls
        };
        AddComponent(entity, spawner);
    }

}
