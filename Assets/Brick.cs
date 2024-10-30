using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitsToBreak = 1; // How many hits the brick can take
    private int hitCount = 0;
    public int scoreValue = 100; // Score given for breaking the brick
    public SpriteRenderer sr;
    private Color originalColor;
    public float darkeningFactor = 0.2f;

    public scoremanager scoremanagerobject;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitCount++;
        DarkenBrick();
       // if (collision.gameObject.tag = "Balls")
       // {
            //ba
          //  Instantiate(collision.gameObject.ball.ballInstance);
       // }

        if (hitCount == hitsToBreak)
        {
            scoremanagerobject.score += scoreValue;
            DestroyBrick();
        }
    }

    void DestroyBrick()
    {
        Destroy(gameObject);
    }

    void DarkenBrick()
    {
        Color newColor = originalColor * (1f - darkeningFactor * hitCount); // Darken the original color

        newColor.a = originalColor.a;
        sr.color = newColor;
    }
}
