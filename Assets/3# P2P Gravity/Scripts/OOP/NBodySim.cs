using UnityEngine;

public class NBodySim : MonoBehaviour
{
    private int ballCount;
    private Vector3 bounds;
    private float gravityStrength;

    private void Start()
    {
        ballCount = NBodySimSpawner.ballTransforms.Length;
        bounds = NBodySimSpawner.simulationBounds;
        gravityStrength = NBodySimSpawner.GravityStrength;
    }

    private void Update()
    {
        
        for (int i = 0; i < ballCount; i++)
        {
            Vector3 totalForce = Vector3.zero;

            Vector3 pos1 = NBodySimSpawner.ballTransforms[i].position;
            Vector3 vel = NBodySimSpawner.ballVelocities[i];
            float mas1 = NBodySimSpawner.ballMass[i];

            for (int j = 0; j < ballCount; j++)
            {
                if (i == j) continue;
                Vector3 pos2 = NBodySimSpawner.ballTransforms[j].position;
                Vector3 dif = pos1 - pos2;
                 
                float dist = dif.sqrMagnitude + 0.1f;

                float mas2 = NBodySimSpawner.ballMass[j];

                float gravForce = gravityStrength * mas1 * mas2 / dist;
               
                totalForce += dif.normalized * gravForce;
            }
            Vector3 acl = totalForce / mas1;
            vel += acl * Time.deltaTime;

            Vector3 pos = pos1 + vel * Time.deltaTime;

            if (pos.x > bounds.x) { vel.x *= -1f; pos.x = bounds.x; }

            else if (pos.x < -bounds.x) { vel.x *= -1f; pos.x = -bounds.x; }

            else if (pos.y > bounds.y) { vel.y *= -1f; pos.y = bounds.y; }

            else if (pos.y < -bounds.y) { vel.y *= -1f; pos.y = -bounds.y; }

            else if (pos.z > bounds.z) { vel.z *= -1f; pos.z = bounds.z; }

            else if (pos.z < -bounds.z) { vel.z *= -1f; pos.z = -bounds.z; }

            NBodySimSpawner.ballTransforms[i].position += vel * Time.deltaTime;
        }
      
    }
}
