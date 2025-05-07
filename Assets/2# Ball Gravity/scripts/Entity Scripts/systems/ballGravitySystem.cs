using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

partial struct ballGravitySystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<gravityCompData>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var gravityData = SystemAPI.GetSingleton<gravityCompData>();
        float3 gravityCentre = gravityData.gravityCentre;
        foreach (var (transform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithEntityAccess())
        {
            float3 dir = gravityCentre - transform.ValueRO.Position;
            float3 gravityForce = math.normalize(dir) * 0.01f;
            transform.ValueRW.Position += gravityForce;
        }
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
