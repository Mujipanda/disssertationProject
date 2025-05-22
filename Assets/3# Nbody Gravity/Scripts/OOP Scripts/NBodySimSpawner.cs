using UnityEngine;


public class NBodySimSpawner : MonoBehaviour
{
    public static Transform[] ballTransforms;
    public static Vector3[] ballVelocities;
    public static float[] ballMass;
    public static float GravityStrength;
    public static Vector3 simulationBounds;


    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private int numOfBalls;

    [SerializeField]
    private float gravStrength;

    [SerializeField]
    private Vector2 Bounds;

    [SerializeField]
    private Vector3 simBounds;

    [SerializeField]
    private Vector2 massRange;

    [SerializeField]
    private Vector2 ballSizeRange;


    private void Awake()
    {
        ballTransforms = new Transform[numOfBalls];
        ballVelocities = new Vector3[numOfBalls];
        ballMass = new float[numOfBalls];
        simulationBounds = simBounds;
        GravityStrength = gravStrength;
    }


    private void Start()
    {
        Random.InitState(45);

        for (int i = 0; i < numOfBalls; i++)
        {
            GameObject ball = Instantiate(ballPrefab, transform);

            ball.transform.localPosition = new Vector3
                (Random.Range(-Bounds.x, Bounds.x), 0, Random.Range(-Bounds.y, Bounds.y));

            float mass = Random.Range(massRange.x, massRange.y);

            float randScale = Random.Range(ballSizeRange.x, ballSizeRange.y);
            ball.transform.localScale = new Vector3(randScale, randScale, randScale);

            ballTransforms[i] = ball.transform;
            ballVelocities[i] = Vector3.zero;
            ballMass[i] = mass;
        }
    }

}
