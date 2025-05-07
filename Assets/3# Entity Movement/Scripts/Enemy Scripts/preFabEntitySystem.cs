using System;
using System.Threading.Tasks;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial class preFabEntitySystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _EndSimulationEntityCommandBufferSystem;// use to get already existing buffer from scene


    private bool canSpawn = true;

    protected override void OnCreate()
    {
        _EndSimulationEntityCommandBufferSystem = World.GetExistingSystemManaged<EndSimulationEntityCommandBufferSystem>();

        if (_EndSimulationEntityCommandBufferSystem == null)
        {
            UnityEngine.Debug.LogError("EndSimulationEntityCommandBufferSystem is null. Make sure it's correctly set up.");
        }
    }
    protected override void OnUpdate()
    {
        if (canSpawn)
        {
            spawnEnemy();
        }
    }
    private async void spawnEnemy()
    {
        canSpawn = false;
        await Task.Delay(TimeSpan.FromSeconds(3));

        var ecb = _EndSimulationEntityCommandBufferSystem.CreateCommandBuffer();

        foreach (var prefab in SystemAPI.Query<RefRO<preFabEnemyData>>())
        {
            //ERROR playerPos does not change value because it is baked
            float3 playerPos = prefab.ValueRO.playerPos;
            UnityEngine.Debug.Log(playerPos);
            Entity entity = prefab.ValueRO.enemyPrefab;

            ecb.SetComponent(entity, new LocalTransform()
            {
                //seting values of the prefab to be spawned
                Position = RandomCircle(playerPos, 3),
                Rotation = quaternion.identity,
                Scale = 1f
            });

            // adding an entity changes the structure of the memory, This creates a Sync point 
            //Sync points are a waiting point where it waits for all job systems to be competed
            // we use the ecb to buffer commands so we can avoid creating multiple sync points
            // in this case it does not make a big difference becasuse I'm only spawning one
            //I may want to spawn multiple at once in the future

            //entity = ecb.Instantiate(prefab.ValueRO.enemyPrefab);
            ecb.Instantiate(prefab.ValueRO.enemyPrefab);

        }
        canSpawn = true;
    }

    protected override void OnStartRunning()
    {
        /*
        var ecb = _EndSimulationEntityCommandBufferSystem.CreateCommandBuffer();

        foreach (var prefab in SystemAPI.Query<RefRO<preFabEnemyData>>())
        {

            int enemyCount = prefab.ValueRO.enemyCount;

            float3 playerPos = prefab.ValueRO.playerPos;

            Entity entity = prefab.ValueRO.enemyPrefab;

            for (int i = 0; i < enemyCount; i++)
            {
                ecb.SetComponent(entity, new LocalTransform()
                {
                    //Position = new float3(playerPos.x + UnityEngine.Random.Range(-5,-0.5f), 0,playerPos.z + UnityEngine.Random.Range(-5, -2)),
                    //Position = new float3(playerPos.x = math.sqrt( (math.PI * (3*3))/ math.PI), 0, 0),
                    Position = RandomCircle(playerPos, 3),
                    Rotation = quaternion.identity,
                    Scale = 1f,
                });

                entity = ecb.Instantiate(prefab.ValueRO.enemyPrefab);
            }
        }
        */
    }

    private float3 RandomCircle(float3 center, float radius)// calculates random pos in a circle shape
    {
        float ang = UnityEngine.Random.value * 360;
        float3 pos;
        pos.x = center.x - radius * math.sin(ang * UnityEngine.Mathf.Deg2Rad);
        pos.z = center.z - radius * math.cos(ang * UnityEngine.Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }
}
//https://discussions.unity.com/t/spawn-objects-around-a-sphere/213726/2
//https://www.youtube.com/watch?v=XGXAEAvnQNA&t=80s
