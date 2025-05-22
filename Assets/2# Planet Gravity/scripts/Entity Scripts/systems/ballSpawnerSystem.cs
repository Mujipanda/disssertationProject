//using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

//[UpdateInGroup(typeof(InitializationSystemGroup))]
partial struct ballSpawnerSystem : ISystem
{
    //[BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ballSpawnerCompData>();
    }

    //[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        
        state.Enabled = false;
        
        var ballSpawner = SystemAPI.GetSingleton<ballSpawnerCompData>();

        var prefab = ballSpawner.entityPrefab;

        var instances = state.EntityManager.Instantiate(prefab, ballSpawner.numBalls, Allocator.Temp);
        
        var random = new Random(88);

        var bounds = ballSpawner.bounds;
        var sizeRange = ballSpawner.ballSizeRange;

        var balldata = new BallCompData { ballVelocity = float3.zero };

        foreach (var ball in instances)
        {
            state.EntityManager.AddComponentData(ball, balldata);

            var pos = random.NextFloat2(bounds, -bounds);
            var size = random.NextFloat(sizeRange.x, sizeRange.y);

            state.EntityManager.SetComponentData(ball, new LocalTransform
            {
                Position = new float3(pos.x, 0, pos.y),
                Scale = size,
                Rotation = quaternion.identity
            });
        }
        
        
    }

    //[BurstCompile]
    public void OnDestroy(ref SystemState state)
    {

    }
}
