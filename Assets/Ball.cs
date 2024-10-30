using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{

    public GameObject ballInstance;

    //private TrailRenderer trail;

    public float initialSpeed = 10f;
    public Transform paddle; 
    public float randomnessFactor = 0.2f; 

    private Rigidbody2D rb;
    private bool gameStarted = false;

    public Rigidbody2D paddleRb;
    public float paddleInfluence = 0.5f;

    private float prePaddleInfluence = 0f;

    public scoremanager scoremanagerobject;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;


        //trail = GetComponent<TrailRenderer>();



        ResetBall();
    }

    void Update()
    {

        if (scoremanagerobject.lives <= 0)
        {
            Destroy(this.gameObject);
        }

        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
            gameStarted = true;
        }

        if (!gameStarted)
        {
            ResetBall();
        }
    }

    void LaunchBall()
    {
        rb.velocity = Vector2.up * initialSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("KillWall"))
        {
            HandleBallOutOfPlay();
            return;
        }

        if (collision.gameObject.CompareTag("Paddle"))
        {
            Paddle p = collision.gameObject.GetComponent<Paddle>();

            Debug.Log(p.hVel);

            Vector2 direction = rb.velocity.normalized;
            direction.x = Mathf.Clamp(direction.x, -0.4f, 0.4f);
            direction = direction.normalized;
            //direction.x -= prePaddleInfluence;
            float random = p.hVel * paddleInfluence;
            prePaddleInfluence = random;
            direction.x += random;
            direction.x = Mathf.Clamp(direction.x, -0.4f, 0.3f);
            rb.velocity = direction.normalized * initialSpeed;
        }
        else
        {
            Vector2 direction = rb.velocity.normalized;
            direction.x = Mathf.Clamp(direction.x, -0.3f, 0.3f);
            direction = direction.normalized;
            //direction.x -= prePaddleInfluence;
            float random = Random.Range(-randomnessFactor, randomnessFactor);
            prePaddleInfluence = random;
            direction.x += random;
            direction.x = Mathf.Clamp(direction.x, -0.4f, 0.4f);
            rb.velocity = direction.normalized * initialSpeed;
        }
    }

    void HandleBallOutOfPlay()
    {
        gameStarted = false;
        rb.velocity = Vector2.zero;
        scoremanagerobject.lives -= 1;
        ResetBall();

    }

    void ResetBall()
    {
        //trail.Clear();
        transform.position = new Vector2(paddle.position.x, paddle.position.y + 1.5f);
    }
}
