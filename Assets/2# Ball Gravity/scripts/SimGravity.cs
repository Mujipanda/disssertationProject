using UnityEngine;

public class SimGravity : MonoBehaviour
{
    private int ballCount;
    private float radius;
    private Vector3 bounds;

    private void Start()
    {
        ballCount = BallSpawner.ballTransforms.Length;
        radius = BallSpawner.radius;
        bounds = BallSpawner.simulationBounds;
    }
    private void Update()
    {
        for (int i = 0; i < ballCount; i++)
        {
            Vector3 pos = BallSpawner.ballTransforms[i].position;
            Vector3 velocity = BallSpawner.ballVelocities[i];
            Vector3 gravityCentre = BallSpawner.gravityCentre.position;
            float gravityStrength = BallSpawner.gravityStrength;

            Vector3 dist = gravityCentre - pos;

            if (dist.magnitude > radius)
            {
                Vector3 gravityDir = dist.normalized * (gravityStrength / (dist.magnitude * dist.magnitude));
                velocity += gravityDir * Time.deltaTime;
            }

            if (pos.x > bounds.x) { velocity.x *= -1f; pos.x = bounds.x; }

            else if (pos.x < -bounds.x) { velocity.x *= -1f; pos.x = -bounds.x; }

            else if (pos.y > bounds.y) { velocity.y *= -1f; pos.y = bounds.y; }

            else if (pos.y < -bounds.y) { velocity.y *= -1f; pos.y = -bounds.y; }

            else if (pos.z > bounds.z) { velocity.z *= -1f; pos.z = bounds.z; }

            else if (pos.z < -bounds.z) { velocity.z *= -1f; pos.z = -bounds.z; }

            pos += velocity * Time.deltaTime;

            BallSpawner.ballTransforms[i].position = pos;
            BallSpawner.ballVelocities[i] = velocity;
        }
    }
}
