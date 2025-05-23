using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

/// <summary>
/// Job struct containing the logic for the gravity calculation for the Unity Entity system
/// </summary>
public partial struct ballGravityEntityIJob : IJobEntity
{
    public float3 gravityCentre;
    public float gravityStrength;
    public float radius;
    public float3 bounds;
    public float dt;

    public void Execute(ref LocalTransform transform, ref BallCompData ballData)
    {
        float3 pos = transform.Position;
        float3 vel = ballData.ballVelocity;
        float3 dist = gravityCentre - pos;
        float distMag = math.length(dist);

        if(distMag > radius)
        {
            float3 gravityDir = math.normalize(dist) * (gravityStrength / (distMag * distMag));
            vel += gravityDir * dt;
        }

        if (pos.x > bounds.x) { vel.x *= -1f; pos.x = bounds.x; }

        else if (pos.x < -bounds.x) { vel.x *= -1f; pos.x = -bounds.x; }

        else if (pos.y > bounds.y) { vel.y *= -1f; pos.y = bounds.y; }

        else if (pos.y < -bounds.y) { vel.y *= -1f; pos.y = -bounds.y; }

        else if (pos.z > bounds.z) { vel.z *= -1f; pos.z = bounds.z; }

        else if (pos.z < -bounds.z) { vel.z *= -1f; pos.z = -bounds.z; }

        pos += vel * dt;

        transform.Position = pos;
        ballData.ballVelocity = vel;

    }
}