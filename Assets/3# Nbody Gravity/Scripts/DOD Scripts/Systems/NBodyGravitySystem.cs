using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine;
using UnityEditor.VersionControl;
using UnityEngine.UIElements;

partial struct NBodyGravitySystem : ISystem
{
    public void OnCreate(ref SystemState state) 
    {
        state.RequireForUpdate<NBodyGravityCompData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var NBodyGravityData = SystemAPI.GetSingleton<NBodyGravityCompData>();
        float dt = SystemAPI.Time.DeltaTime;

        float gravityStrength = NBodyGravityData.gravityStrength;
        var bounds = NBodyGravityData.simulationBounds;

        var arrayBuilder = SystemAPI.QueryBuilder().WithAll<LocalTransform, NBodyBallCompData>().Build();

        var entityCount = arrayBuilder.CalculateEntityCount();

        var ballPos = new NativeArray<float3>(entityCount, Allocator.Temp);
        var ballVel = new NativeArray<float3>(entityCount, Allocator.Temp);
        var ballMass = new NativeArray<float>(entityCount, Allocator.Temp);

        var newPos = new NativeArray<float3>(entityCount, Allocator.Temp);
        var newVel = new NativeArray<float3>(entityCount, Allocator.Temp);

        int index = 0;
        foreach (var (transform, balldata, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<NBodyBallCompData>>().WithEntityAccess())
        {
            ballPos[index] = transform.ValueRO.Position;
            ballVel[index] = balldata.ValueRO.ballVelocity;
            ballMass[index] = balldata.ValueRO.ballMass;
        
            index++;
        }

        for (int i = 0; i < entityCount; i++)
        {
            float3 totalForce = float3.zero;
            float3 pos1 = ballPos[i];
            float3 vel = ballVel[i];
            float mas1 = ballMass[i];

            for (int j = 0; j < entityCount; j++)
            {
                if (i == j) continue;
                
                float3 pos2 = ballPos[j];
                float3 dif = pos2 - pos1;

                float dist = math.lengthsq(dif) + 0.1f;

                float mas2 = ballMass[j];

                float gravForce = gravityStrength * mas1 * mas2 / dist;

                totalForce += math.normalize(dif) * gravForce;
            }
            float3 acl = totalForce / mas1;
            vel += acl * dt;
            float3 pos = pos1 + vel * dt;

            if (pos.x > bounds.x) { vel.x *= -1f; pos.x = bounds.x; }
            else if (pos.x < -bounds.x) { vel.x *= -1f; pos.x = -bounds.x; }
            else if (pos.y > bounds.y) { vel.y *= -1f; pos.y = bounds.y; }
            else if (pos.y < -bounds.y) { vel.y *= -1f; pos.y = -bounds.y; }
            else if (pos.z > bounds.z) { vel.z *= -1f; pos.z = bounds.z; }
            else if (pos.z < -bounds.z) { vel.z *= -1f; pos.z = -bounds.z; }

            newPos[i] = pos;
            newVel[i] = vel;
        }

        int index2 = 0;
        foreach (var (transform, balldata, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<NBodyBallCompData>>().WithEntityAccess())
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

}