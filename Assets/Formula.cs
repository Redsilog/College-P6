using UnityEngine;

public class Formula : MonoBehaviour
{
    [SerializeField] public float cor;
    public Rigidbody rb;
    public float highestBounce = 0f;
    public float startTime;
    public bool hasLanded = false;
    public bool isMoving = true;
    public int stationaryFrames = 0;
    public float firstBounce = 0;
    public int highBounce = 1;

    public float fallTime = 0f;

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
            Debug.Log(gameObject.name + " highest recorded bounce was: " + firstBounce + " meters");
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
                fallTime = Time.time - startTime; 
                Debug.Log(gameObject.name + " fell in " + fallTime + " seconds");
                hasLanded = true;
            }

            highestBounce *= cor;
            if (highBounce == 1)
            {
                firstBounce = highestBounce;
                highBounce++;
            }
        }
    }
}
