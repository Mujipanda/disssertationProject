using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

public partial struct NBodyGravityIjob : IJobParallelFor
{
    [ReadOnly] public NativeArray<float3> ballPos;
    [ReadOnly] public NativeArray<float3> ballVel;
    [ReadOnly] public NativeArray<float> ballMass;

    public NativeArray<float3> newPos;
    public NativeArray<float3> newVel;

    public float gravityStrength;
    public float dt;
    public float3 bounds;
    public int ballCount;
    public void Execute(int i)
    {
        float3 pos1 = ballPos[i];
        float3 vel = ballVel[i];

        float mas1 = ballMass[i];

        float3 totalForce = float3.zero;

        for (int j = 0; j < ballCount; j++)
        {
            if (i == j) continue;

            float3 pos2 = ballPos[j];
            float3 dif = pos2 - pos1;

            float dist = math.lengthsq(dif) + 0.1f;

            float mas2 = ballMass[j];

            float gravForce = gravityStrength * mas1 * mas2 /dist;

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
}
