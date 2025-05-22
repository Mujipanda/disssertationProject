using Unity.Entities;
using Unity.Mathematics;

partial struct ballGravitySystemJob : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<gravityCompData>();
        state.RequireForUpdate<JobSceneTag>();
    }

    public void OnUpdate(ref SystemState state)
    {

        var gravityData = SystemAPI.GetSingleton<gravityCompData>();

       
        float3 gravityCentre = gravityData.gravityCentre;

        float gravityStrength = gravityData.gravityStrength;
        float radius = gravityData.centreRadius;

        float3 bounds = gravityData.simulationBounds;

        float dt = SystemAPI.Time.DeltaTime;

        var job = new ballGravityEntityIJob
        {
            gravityCentre = gravityCentre,
            gravityStrength = gravityStrength,
            radius = radius,
            bounds = bounds,
            dt = dt,
        };

        job.ScheduleParallel();
    }

    public void OnDestroy(ref SystemState state)
    {

    }
}
