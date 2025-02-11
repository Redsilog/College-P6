using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Formula : MonoBehaviour
{
    [SerializeField] public float cor;
    [SerializeField] public Vector3 initialVelocityScene2;
    public Rigidbody rb;
    public float highestBounce = 0f;
    public float startTime;
    public bool hasLanded = false;
    public bool isMoving = true;
    public int stationaryFrames = 0;
    public float firstBounce = 0;
    public int highBounce = 1;
    public bool gameStarted = false;
    public float fallTime = 0f;

    void Start()
{
    rb = GetComponent<Rigidbody>();
    rb.useGravity = false;
    rb.isKinematic = true;
    highestBounce = transform.position.y;

    Debug.Log("Press Space to Start Experiment");

    if (SceneManager.GetActiveScene().name == "Scene2")
    {
        rb.linearVelocity = initialVelocityScene2;  // ✅ Use velocity instead of linearVelocity
    }
}

void StartGame()
{
    gameStarted = true;
    rb.isKinematic = false;  // ✅ Enable physics
    rb.useGravity = true;
    
    if (SceneManager.GetActiveScene().name == "Scene2")
    {
        rb.linearVelocity = initialVelocityScene2;  // ✅ Reapply velocity when game starts
    }

    startTime = Time.time;
    Debug.Log("Game started!");
}


    void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
            return;
        }

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
        }

        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Scene2");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!gameStarted) return;

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