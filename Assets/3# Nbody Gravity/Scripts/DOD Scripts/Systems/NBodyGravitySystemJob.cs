using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

partial struct NBodyGravitySystemJob : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<NBodyGravityCompData>();
        state.RequireForUpdate<NBodyJobSceneTag>();
        

    }


    public void OnUpdate(ref SystemState state)
    {
        var NBodyGravityData = SystemAPI.GetSingleton<NBodyGravityCompData>();
        float dt = SystemAPI.Time.DeltaTime;

        float gravityStrength = NBodyGravityData.gravityStrength;
        var bounds = NBodyGravityData.simulationBounds;

        var arrayBuilder = SystemAPI.QueryBuilder().WithAll<LocalTransform, NBodyBallCompData>().Build();

        var entityCount = arrayBuilder.CalculateEntityCount();

        var ballPos = new NativeArray<float3>(entityCount, Allocator.Persistent);
        var ballVel = new NativeArray<float3>(entityCount, Allocator.Persistent);
        var ballMass = new NativeArray<float>(entityCount, Allocator.Persistent);

        var newPos = new NativeArray<float3>(entityCount, Allocator.Persistent);
        var newVel = new NativeArray<float3>(entityCount, Allocator.Persistent);

        int index = 0;
        foreach (var (transform, balldata, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<NBodyBallCompData>>().WithAll<NBodyBallTag>().WithEntityAccess())
        {
            ballPos[index] = transform.ValueRO.Position;
            ballVel[index] = balldata.ValueRO.ballVelocity;
            ballMass[index] = balldata.ValueRO.ballMass;

            index++;
        }

        NBodyGravityIjob job = new NBodyGravityIjob
        {
            ballPos = ballPos,
            ballVel = ballVel,
            ballMass = ballMass,
            newPos = newPos,
            newVel = newVel,
            ballCount = entityCount,
            bounds = bounds,
            dt = dt,
            gravityStrength = gravityStrength
        };
        var JobHandle = job.Schedule(entityCount, 64, state.Dependency);
        JobHandle.Complete();

        int index2 = 0;
        foreach (var (transform, balldata, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<NBodyBallCompData>>().WithAll<NBodyBallTag>().WithEntityAccess())
        {
            transform.ValueRW.Position = newPos[index2];
            balldata.ValueRW.ballVelocity = newVel[index2];
            index2++;

        }
        ballPos.Dispose();
        ballVel.Dispose();
        ballMass.Dispose();
        newPos.Dispose();
        newVel.Dispose();
    }



    public void OnDestroy(ref SystemState state)
    {
        
    }
}
