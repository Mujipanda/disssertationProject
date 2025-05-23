using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
/// <summary>
/// the System for handling the orbital movement of the sun
/// </summary>
partial struct orbitSystem : ISystem
{

    float elapsedTime;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<gravityCompData>();

    }

 
    public void OnUpdate(ref SystemState state)
    {

        elapsedTime += 0.1f * SystemAPI.Time.DeltaTime;

        float x = math.cos(elapsedTime) * 0.5f;
        float y = math.sin(elapsedTime) * 0.5f;
        float3 pos = new float3(x, y, y);

        var gravityData = SystemAPI.GetSingletonRW<gravityCompData>();

        gravityData.ValueRW.gravityCentre = pos;

        foreach (var (transform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<SunTag>().WithEntityAccess())
        {
            transform.ValueRW.Position = pos;
        }
       
    }


    public void OnDestroy(ref SystemState state)
    {

    }
}
