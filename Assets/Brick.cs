using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitsToBreak = 1; 
    private int hitCount = 0;
    public int scoreValue = 100; 
    public SpriteRenderer sr;
    private Color originalColor;
    public float darkeningFactor = 0.2f;

    private bool isDestroyed = false;
    private float currentSpeed = 2.0f;

    public scoremanager scoremanagerobject;
    private Rigidbody2D rb;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        rb = GetComponent<Rigidbody2D>();
       // rb.isKinematic = true;
    }

    void Update()
    {
        if (isDestroyed)
        {
            transform.position += Vector3.down * currentSpeed * Time.deltaTime;
            currentSpeed *= 1.003f;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Paddle"))
        {
            scoremanagerobject.score += scoreValue;

            Debug.Log("MOENYYYYY");
            Destroy(gameObject);
            return;
        }
        if (collision.gameObject.CompareTag("KillWall"))
        {
            Debug.Log("DSWRETRYYY");
            Destroy(gameObject);
            return;
        }
        if (collision.gameObject.CompareTag("Ball"))
        {
            hitCount++;
            if ((hitCount == hitsToBreak) && (!isDestroyed))
            {

                isDestroyed = true;
                Collider2D ballCollider = collision.gameObject.GetComponent<Collider2D>();
                Collider2D targetCollider = GetComponent<Collider2D>();

                if (ballCollider != null && targetCollider != null)
                {
                    Physics2D.IgnoreCollision(ballCollider, targetCollider, true);
                }
            }
            else
            {
                DarkenBrick();
            }
        }
    }

    void DestroyBrick()
    {
        //Destroy(gameObject);
        //isDestroyed = true;

        //int ballLayer = LayerMask.NameToLayer("ball");
        //int noBallCollisionLayer = LayerMask.NameToLayer("noBall");

        //Physics2D.IgnoreLayerCollision(ballLayer, noBallCollisionLayer, true);
    }

    void DarkenBrick()
    {
        Color newColor = originalColor * (1f - darkeningFactor * hitCount);

        newColor.a = originalColor.a;
        sr.color = newColor;
    }
}
