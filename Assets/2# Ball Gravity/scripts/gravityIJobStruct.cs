using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]
public struct gravityIJobStruct : IJobParallelFor
{
    public float dt;
    public float gravityStrength;
    public int ballCount;
    public Vector3 gravityCentre;
    public Vector3 bounds;
    public float radius;

    public NativeArray<Vector3> ballPositions;
    public NativeArray<Vector3> ballVelocities;

    public void Execute(int i)
    {
        Vector3 pos = ballPositions[i];
        Vector3 vel = ballVelocities[i];
        Vector3 dist = gravityCentre - pos;

        if (dist.magnitude > radius)
        {
            Vector3 gravityDir = dist.normalized * (gravityStrength / (dist.magnitude * dist.magnitude));

            vel += gravityDir * dt;
        }

        if (pos.x > bounds.x) { vel.x *= -1f; pos.x = bounds.x; }

        else if (pos.x < -bounds.x) { vel.x *= -1f; pos.x = -bounds.x; }

        else if (pos.y > bounds.y) { vel.y *= -1f; pos.y = bounds.y; }

        else if (pos.y < -bounds.y) { vel.y *= -1f; pos.y = -bounds.y; }

        else if (pos.z > bounds.z) { vel.z *= -1f; pos.z = bounds.z; }

        else if (pos.z < -bounds.z) { vel.z *= -1f; pos.z = -bounds.z; }


        pos += vel * dt;
        ballPositions[i] = pos;
        ballVelocities[i] = vel;

    }

}
