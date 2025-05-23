using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

/// <summary>
/// the System for handling the spawning of all the balls 
/// </summary>
partial struct NBodySpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<NBodySpawnerCompData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        var NBodySpawner = SystemAPI.GetSingleton<NBodySpawnerCompData>();

        var prefab = NBodySpawner.entityPrefab;

        var instances = state.EntityManager.Instantiate(prefab, NBodySpawner.numBalls, Allocator.Temp);

        var random = new Random(88);

        var bounds = NBodySpawner.bounds;

        var sizeRange = NBodySpawner.ballSizeRrange;

        var massRange = NBodySpawner.massRange;

       
        

       foreach ( var instance in instances )
       {
            var mass = random.NextFloat(massRange.x, massRange.y);

            var ballData = new NBodyBallCompData
            {
                ballVelocity = float3.zero,
                ballMass = mass,
            };

            state.EntityManager.AddComponentData( instance, ballData );

            var pos = random.NextFloat2(bounds, -bounds);
            var size = random.NextFloat(sizeRange.x, sizeRange.y);

            state.EntityManager.SetComponentData(instance, new LocalTransform
            {
                Position = new float3(pos.x, 0, pos.y),
                Scale = size,
                Rotation = quaternion.identity
            });
       }
        
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }
}
