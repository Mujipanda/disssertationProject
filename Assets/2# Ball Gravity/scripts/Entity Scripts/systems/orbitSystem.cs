using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

partial struct orbitSystem : ISystem
{

    float elapsedTime;
    int num;
    Entity sunEntity;
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<gravityCompData>();

        //var prefab = SystemAPI.GetSingleton<gravityCompData>().sunPrefab;

        // sunEntity = state.EntityManager.Instantiate(prefab);
        //var gravityData = SystemAPI.GetSingleton<gravityCompData>();

        //sunEntity = state.EntityManager.Instantiate(gravityData.sunPrefab);
        num = 0;
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
       
        elapsedTime += 0.1f * SystemAPI.Time.DeltaTime;

        float x = math.cos(elapsedTime) * 0.5f;
        float y = math.sin(elapsedTime) * 0.5f;
        float3 pos = new float3(x, y, y);

        var gravityData = SystemAPI.GetSingletonRW<gravityCompData>();

        //var sunEntity = state.EntityManager.Instantiate(gravityData.ValueRO.sunPrefab);

        gravityData.ValueRW.gravityCentre = pos;
 
        if (num == 0)
        {
            sunEntity = state.EntityManager.Instantiate(gravityData.ValueRO.sunPrefab);

            num = 1;
        }

        var transform = SystemAPI.GetComponentRW<LocalTransform>(sunEntity);
        transform.ValueRW.Position = pos;
        gravityData.ValueRW.gravityCentre = pos;

    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {

    }
}
