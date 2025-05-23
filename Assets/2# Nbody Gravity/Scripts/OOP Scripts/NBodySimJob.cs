using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class NBodySimJob : MonoBehaviour
{
    private NativeArray<Vector3> ballPositions;
    private NativeArray<Vector3> ballVelocities;
    private NativeArray<Vector3> currrentPosition;
    private NativeArray<Vector3> currrentVelocity;
    private NativeArray<float> ballMass;

    private Vector3 bounds;
    private int ballCount;
    private float gravityStrength;

    private void Start()
    {
        ballCount = NBodySimSpawner.ballTransforms.Length;
        gravityStrength = NBodySimSpawner.GravityStrength;

        ballPositions = new NativeArray<Vector3>(ballCount, Allocator.Persistent);
        ballVelocities = new NativeArray<Vector3>(ballCount, Allocator.Persistent);
        currrentPosition = new NativeArray<Vector3>(ballCount, Allocator.Persistent);
        currrentVelocity = new NativeArray<Vector3>(ballCount, Allocator.Persistent);
        ballMass = new NativeArray<float>(ballCount, Allocator.Persistent);

        bounds = NBodySimSpawner.simulationBounds;
     
    }

    private void OnDestroy()
    {
        ballPositions.Dispose();
        ballVelocities.Dispose();
        currrentPosition.Dispose();
        currrentVelocity.Dispose();
        ballMass.Dispose();

    }

    private void Update()
    {
        for (int i = 0; i < ballCount; i++)
        {
            ballPositions[i] = NBodySimSpawner.ballTransforms[i].position;
            ballVelocities[i] = NBodySimSpawner.ballVelocities[i];
            ballMass[i] = NBodySimSpawner.ballMass[i];
        }

        NBodySimIjobStruct job = new NBodySimIjobStruct
        {
            dt = Time.deltaTime,
            gravityStrength = gravityStrength,
            ballPositions = ballPositions,
            ballVelocities = ballVelocities,
            currrentPosition = currrentPosition,
            currrentVelocity = currrentVelocity,
            ballMass = ballMass,
            bounds = bounds,
            ballCount = ballCount,
        };

        JobHandle findHandle = job.Schedule(ballCount, 100);
        findHandle.Complete();

        for (int i = 0; i < ballCount;i++)
        {
            //currrentPosition[i] = ballPositions[i];
            //currrentVelocity[i] = ballVelocities[i];
            ballPositions[i]  = currrentPosition[i];
            ballVelocities[i] = currrentVelocity[i];

            NBodySimSpawner.ballTransforms[i].position = currrentPosition[i];
            NBodySimSpawner.ballVelocities[i] = currrentVelocity[i];

        }
    }
}
