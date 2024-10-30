using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10f;
    public float boundary = 8f;

    private Vector2 paddlePosition;
    public float hVel;

    void Update()
    {
        float input = 0;
        if (Input.GetKey(KeyCode.A))
        {
            input = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            input = 1;
        }

        int isRotating = 0;
        if (Input.GetKey(KeyCode.E))
        {
            isRotating += 1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            isRotating += -1;
        }
        if (isRotating != 0)
        {
            transform.Rotate(0, 0, (800f * isRotating) * Time.deltaTime);
        }

        if (isRotating == 0)
        {
            transform.rotation = Quaternion.identity;
        }



        paddlePosition = transform.position;
        paddlePosition.x += input * speed * Time.deltaTime;

        paddlePosition.x = Mathf.Clamp(paddlePosition.x, -boundary, boundary);

        transform.position = paddlePosition;

        hVel = input * speed * Time.deltaTime;
    }
}
