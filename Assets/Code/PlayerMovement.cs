using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [SerializeField] private bool IsAI;
    [SerializeField] private GameObject Ball;
    private Rigidbody2D rb;
    private Vector2 PlayerMove;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(IsAI)
        {
            AIControl();
        }
        else
        {
            PlayerControl();
        }
    }
    private void PlayerControl()
    {
        PlayerMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }
    private void AIControl()
    {
        if(Ball.transform.position.y > transform.position.y + 0.5f)
        {
            PlayerMove = new Vector2(0, 1);
        }
        else if(Ball.transform.position.y < transform.position.y - 0.5f)
        {
            PlayerMove = new Vector2(0, -1);
        }
        else
        {
            PlayerMove = new Vector2(0, 0);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = PlayerMove * MovementSpeed;
    }
}
