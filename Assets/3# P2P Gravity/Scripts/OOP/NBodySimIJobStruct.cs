using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public struct NBodySimIjobStruct : IJobParallelFor
{
    public float dt;
    public float gravityStrength;
    public int ballCount;
    public Vector3 bounds;

    //setting to read only due to read/write buffer error when accessing j index value
   [ReadOnly]public NativeArray<Vector3> ballPositions;
   [ReadOnly] public NativeArray<Vector3> ballVelocities;
   [ReadOnly] public NativeArray<float> ballMass;

    public NativeArray<Vector3> currrentVelocity;
    public NativeArray<Vector3> currrentPosition;

    public void Execute(int i)
    {
        Vector3 pos1 = ballPositions[i];
        Vector3 vel = ballVelocities[i];
        float mass1 = ballMass[i];

        Vector3 totalForce = Vector3.zero;

        for (int j = 0; j < ballCount; j++)
        {
            if (i == j) continue;

            Vector3 pos2 = ballPositions[j];
            Vector3 dif = pos2 - pos1;

            float dist = dif.sqrMagnitude + 0.1f;

            float mass2 = ballMass[j];

            float gravForce = gravityStrength * mass1 * mass2 / dist;

            totalForce += dif.normalized * gravForce;     
        }
        Vector3 acl = totalForce / mass1;
        vel += acl * dt;

        Vector3 pos = pos1 + vel * dt;

        if (pos.x > bounds.x) { vel.x *= -1f; pos.x = bounds.x; }

        else if (pos.x < -bounds.x) { vel.x *= -1f; pos.x = -bounds.x; }

        else if (pos.y > bounds.y) { vel.y *= -1f; pos.y = bounds.y; }

        else if (pos.y < -bounds.y) { vel.y *= -1f; pos.y = -bounds.y; }

        else if (pos.z > bounds.z) { vel.z *= -1f; pos.z = bounds.z; }

        else if (pos.z < -bounds.z) { vel.z *= -1f; pos.z = -bounds.z; }

        currrentPosition[i] = pos;
        currrentVelocity[i] = vel;


    }
}