using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class preFabSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform;
}

public struct preFabEnemyData : IComponentData
{
    public Entity enemyPrefab;
    public float3 playerPos;
}
public class PreFabEnemyBaker : Baker<preFabSpawner>
{
    public override void Bake(preFabSpawner authoring)
    {
        var entityPrefab = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic);
        // var entityPlayer = GetEntity(authoring.playerTransform, TransformUsageFlags.Dynamic);


        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new preFabEnemyData
        {
            enemyPrefab = entityPrefab,
            playerPos = authoring.playerTransform.position,
            // playerObj = entityPlayer

        });
        AddComponent(entity, new playerPositionData
        {
        });

    }
}


