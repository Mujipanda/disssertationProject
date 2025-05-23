using UnityEngine;

public class BallSpawner : MonoBehaviour
{
   /// <summary>
   /// Ball spawner creates an array of balls along the x and z axis
   /// </summary>
    public static Transform[] ballTransforms;
    public static Vector3[] ballVelocities;
    public static float gravityStrength;
    public static Transform gravityCentre;
    public static Vector3 simulationBounds;
    public static float radius;
    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private int numOfBalls;

    [SerializeField]
    private Vector2 Bounds;

    [SerializeField]
    private Vector2 ballSizeRange;

    [SerializeField]
    private Transform GravityCentre;

    [SerializeField]
    private float GravityStrength;

    [SerializeField]
    private Vector3 simBounds;

    private float time;

    private float orbitSpeed = 0.1f;

    public float centreRadius;

    private void Awake()
    {
        ballTransforms = new Transform[numOfBalls];
        ballVelocities = new Vector3[numOfBalls];
        simulationBounds = simBounds;
        radius = centreRadius;
    }

    private void Start()
    {
        Random.InitState(45);
        gravityCentre = GravityCentre;
        gravityStrength = GravityStrength;

        for (int i = 0; i < numOfBalls; i++)
        {
            GameObject ball = Instantiate(ballPrefab, transform);

            ball.transform.localPosition = new Vector3
                (Random.Range(-Bounds.x, Bounds.x), 0, Random.Range(-Bounds.y, Bounds.y));

            float randScale = Random.Range(ballSizeRange.x, ballSizeRange.y);
            ball.transform.localScale = new Vector3(randScale, randScale, randScale);

            ballTransforms[i] = ball.transform;
            ballVelocities[i] = Vector3.zero;
        }
    }

    private void Update()
    {
        time += orbitSpeed * Time.deltaTime;
        float x = Mathf.Cos(time) * 0.5f;
        float y = Mathf.Sin(time) * 0.5f;

        GravityCentre.position = new Vector3(x, y, y);
    }

    private void Reset()
    {
        numOfBalls = 10000;
        Bounds = new Vector2(4, 4);
        ballSizeRange = new Vector2(0.01f, 0.02f);
        gravityStrength = 0.03f;
        simBounds = new Vector3(4, 4, 4);
        centreRadius = 0.1f;
    }
}
