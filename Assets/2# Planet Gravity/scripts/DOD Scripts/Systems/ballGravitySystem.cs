using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

partial struct ballGravitySystem : ISystem
{
  
    public void OnCreate(ref SystemState state)
    {
         
        state.RequireForUpdate<gravityCompData>();
        state.RequireForUpdate<NoJobSceneTag>();
    }

    public void OnUpdate(ref SystemState state)
    {
        
        var gravityData = SystemAPI.GetSingleton<gravityCompData>();
        float3 gravityCentre = gravityData.gravityCentre;

        float gravityStrength = gravityData.gravityStrength;
        float radius = gravityData.centreRadius;

        float3 bounds = gravityData.simulationBounds;

        float dt = SystemAPI.Time.DeltaTime;
        foreach (var (transform,balldata,entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BallCompData>>().WithAll<BallTag>().WithEntityAccess())
        {
            float3 pos = transform.ValueRO.Position;
            float3 vel = balldata.ValueRW.ballVelocity;
            float3 dist = gravityCentre - pos;
            float distMag = math.length(dist);

            if(distMag > radius) 
            {
                float3 gravityDir = math.normalize(dist) * (gravityStrength / (distMag * distMag));

                vel += gravityDir * dt;
            }

            if (pos.x > bounds.x) 
            { 
                vel.x *= -1f; pos.x = bounds.x; 
            }
            else if (pos.x < -bounds.x) 
            { 
                vel.x *= -1f; pos.x = -bounds.x;
            }
            if (pos.y > bounds.y) 
            {
                vel.y *= -1f; pos.y = bounds.y;
            }
            else if (pos.y < -bounds.y) 
            { 
                vel.y *= -1f; pos.y = -bounds.y;
            }
            if (pos.z > bounds.z) 
            { 
                vel.z *= -1f; pos.z = bounds.z; }
            else if (pos.z < -bounds.z) 
            { 
                vel.z *= -1f; pos.z = -bounds.z; 
            }

            pos += vel * dt;
            transform.ValueRW.Position = pos;
            balldata.ValueRW.ballVelocity = vel;
        }
        
    }

    public void OnDestroy(ref SystemState state)
    {
        
    }
}
