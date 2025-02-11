using UnityEngine;

public class FreefallTracker : MonoBehaviour
{
    private Rigidbody rb;
    private float highestBounce = 0f;
    float bounceHeight;
    private float startTime;
    private bool hasLanded = false;
    private bool isMoving = true;
    private int stationaryFrames = 0;
    [SerializeField] public float cor = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
        highestBounce = transform.position.y;
    }

    void Update()
    {
        if (rb.linearVelocity.magnitude < 0.05f)
        {
            stationaryFrames++;
        }
        else
        {
            stationaryFrames = 0;
        }

        if (stationaryFrames > 100 && isMoving)
        {
            Debug.Log(gameObject.name + " highest bounce was: " + bounceHeight + " meters");
            isMoving = false;
            Destroy(this);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!hasLanded)
            {
                float fallTime = Time.time - startTime;
                Debug.Log(gameObject.name + " fell in " + fallTime + " seconds");
                hasLanded = true;
            }

            bounceHeight = highestBounce * cor;
        }
    }
}
