using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

/// <summary>
/// the System for spawning in the sun entity
/// </summary>
partial struct sunSpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<gravityCompData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        var gravityData = SystemAPI.GetSingleton<gravityCompData>();

        var sunPrefab = gravityData.sunPrefab;

        //var instances = state.EntityManager.Instantiate(sunPrefab, 1, Allocator.Temp);
        var sunEntity = state.EntityManager.Instantiate(sunPrefab);

        state.EntityManager.SetComponentData(sunEntity, new LocalTransform
        {
            Position = gravityData.gravityCentre,
            Scale = 0.1f,
            Rotation = quaternion.identity
        });
    }
}
    