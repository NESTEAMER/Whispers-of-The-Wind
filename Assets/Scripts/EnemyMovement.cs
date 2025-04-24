using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float changeDirectionTime = 2f;

    private bool movingRight = true;
    private float timer;
    private Rigidbody2D rb;
    private bool hasCollided = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = changeDirectionTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            movingRight = !movingRight;
            timer = changeDirectionTime;
        }

        rb.velocity = movingRight ? Vector2.right * moveSpeed : Vector2.left * moveSpeed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBoundary"))
        {
            movingRight = !movingRight;
            timer = changeDirectionTime;
        }

        if (collision.gameObject.CompareTag("Player") && !hasCollided)
        {
            Debug.Log("Collided with player");
            Timer.instance.SetTimeAndIncrement(19f);
            hasCollided = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollided = false;
        }
    }
}