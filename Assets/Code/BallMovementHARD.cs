using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BallMovementHARD : MonoBehaviour
{
    [SerializeField] private float InitialSpeed = 10;
    [SerializeField] private float SpeedIncrease = 0.5f;
    [SerializeField] private Text PlayerScore;
    [SerializeField] private Text AIScore;
    public float RadNum = 0f;
    private float rotZ;
    public bool CWRot;
    private int HitCounter;
    private Rigidbody2D rb;
    public AudioSource audioPlayer;
    void Start()
    {//ball go
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }
    private void FixedUpdate()
    {//rotation
        if (CWRot == false)
        {
            rotZ = Time.deltaTime * (InitialSpeed + SpeedIncrease);
        }
        else
        {
            rotZ += -Time.deltaTime * (InitialSpeed + SpeedIncrease);
        }
        transform.rotation = Quaternion.Euler(0, 0, rotZ);


        rb.velocity = Vector2.ClampMagnitude(rb.velocity, InitialSpeed + (SpeedIncrease * HitCounter)); //speed up
        if (int.Parse(PlayerScore.text) == 10)
        {//win
            SceneManager.LoadSceneAsync(4);
        }
        else if (int.Parse(AIScore.text) == 10)
        {//lose
            SceneManager.LoadSceneAsync(7);
        }
    }
    private void StartBall()
    {//where ball go
        RadNum = UnityEngine.Random.Range(1, 20);
        Debug.Log(RadNum);
        if (RadNum % 2 == 0)
        {//ball go left
            rb.velocity = new Vector2(-1, 0) * (InitialSpeed + SpeedIncrease * HitCounter);
        }
        if (RadNum % 2 == 1)
        {//ball go right
            rb.velocity = new Vector2(1, 0) * (InitialSpeed + SpeedIncrease * HitCounter);
        }
    }
    private void ResetBall()
    {//reset ball
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        HitCounter = 0;
        Invoke("StartBall", 2f);
    }
    private void playerBounce(Transform myObject)
    {//ball bounce
        HitCounter++;
        Vector2 BallPos = transform.position;
        Vector2 PlayerPos = myObject.position;

        float xDirection, yDirection;
        if (transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }
        yDirection = (BallPos.y - PlayerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if (yDirection == 0)
        {
            yDirection = 0.25f;
        }
        rb.velocity = new Vector2(xDirection, yDirection) * (InitialSpeed + (SpeedIncrease * HitCounter));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {//detections and sfx
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "AI")
        {
            playerBounce(collision.transform);
            audioPlayer.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            ResetBall();
            PlayerScore.text = (int.Parse(PlayerScore.text) + 1).ToString();
        }
        else if (transform.position.x < 0)
        {
            ResetBall();
            AIScore.text = (int.Parse(AIScore.text) + 1).ToString();
        }
    }
}
