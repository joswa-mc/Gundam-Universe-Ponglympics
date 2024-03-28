using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float InitialSpeed = 10;
    [SerializeField] private float SpeedIncrease = 0.25f;
    [SerializeField] private Text PlayerScore;
    [SerializeField] private Text AIScore;
    private int HitCounter;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, InitialSpeed + (SpeedIncrease * HitCounter));
    }
    private void StartBall()
    {
        rb.velocity = new Vector2(-1, 0) * (InitialSpeed + SpeedIncrease * HitCounter);
    }
    private void ResetBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        HitCounter = 0;
        Invoke("StartBall", 2f);
    }
    private void playerBounce(Transform myObject)
    {
        HitCounter++;
        Vector2 BallPos = transform.position;
        Vector2 PlayerPos = myObject.position;

        float xDirection, yDirection;
        if(transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }
        yDirection = (BallPos.y - PlayerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if(yDirection == 0)
        {
            yDirection = 0.25f;
        }
        rb.velocity = new Vector2(xDirection, yDirection) * (InitialSpeed + (SpeedIncrease * HitCounter));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player" || collision.gameObject.name == "AI")
        {
            playerBounce(collision.transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.position.x > 0)
        {
            ResetBall();
            PlayerScore.text = (int.Parse(PlayerScore.text) + 1).ToString();
        }
        else if(transform.position.x  < 0)
        {
            ResetBall();
            AIScore.text = (int.Parse(AIScore.text) + 1).ToString();
        }
    }
}
