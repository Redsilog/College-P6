using UnityEngine;

public class Formula : MonoBehaviour
{
    [SerializeField] public float cor;
    private Rigidbody rb;
    private float highestBounce = 0f;
    private float startTime;
    private bool hasLanded = false;
    private bool isMoving = true;
    private int stationaryFrames = 0;
    private float firstBounce = 0;
    private int highBounce = 1;

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
                float fallTime = Time.time - startTime;
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