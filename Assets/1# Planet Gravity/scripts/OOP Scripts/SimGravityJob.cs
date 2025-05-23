using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class SimGravityJob : MonoBehaviour
{
    /// <summary>
    /// The class that calls upon the Job struct containting the variables that will be based to the job.
    /// </summary>
    private NativeArray<Vector3> ballPositions;
    private NativeArray<Vector3> ballVelocities;

    private int ballCount;

    private void Start()
    {
        ballCount = BallSpawner.ballTransforms.Length;

        ballPositions = new NativeArray<Vector3>(ballCount, Allocator.Persistent);
        ballVelocities = new NativeArray<Vector3>(ballCount, Allocator.Persistent);
    }

    private void OnDestroy()
    {
        ballPositions.Dispose();
        ballVelocities.Dispose();
    }

    private void Update()
    {
        for (int i = 0; i < ballCount; i++)
        {
            ballPositions[i] = BallSpawner.ballTransforms[i].position;
            ballVelocities[i] = BallSpawner.ballVelocities[i];
        }

        gravityIJobStruct job = new gravityIJobStruct
        {
            dt = Time.deltaTime,
            gravityStrength = BallSpawner.gravityStrength,
            ballCount = ballCount,
            gravityCentre = BallSpawner.gravityCentre.position,
            ballPositions = ballPositions,
            ballVelocities = ballVelocities,
            bounds = BallSpawner.simulationBounds,
            radius = BallSpawner.radius
        };

        JobHandle findHandle = job.Schedule(ballCount, 100);
        findHandle.Complete();

        for (int i = 0; i < ballCount; i++)
        {
            BallSpawner.ballTransforms[i].position = ballPositions[i];
            BallSpawner.ballVelocities[i] = ballVelocities[i];
        }
    }

}
