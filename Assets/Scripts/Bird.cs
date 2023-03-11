using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
     
public class Bird : MonoBehaviour
{
    private const float Jump_Amount = 90f;
    private Rigidbody2D Birdrigidbody2d;
    private static Bird instance;
    public event EventHandler OnBirdDie;
    public event EventHandler OnPlayingStarted;
    private Transform bird_YPosition;

    private Start_State start_state;
    private enum Start_State 
    {
        waitingTo_start,
        Playing,
        Died,
    }


    private void Awake()
    {
        instance = this;
        Birdrigidbody2d = GetComponent<Rigidbody2D>();
         start_state = Start_State.waitingTo_start;
        Birdrigidbody2d.bodyType = RigidbodyType2D.Static;
        bird_YPosition = transform;
    }
  

    private void Update()
    {
        

        switch (start_state)
        {
           
            case Start_State.waitingTo_start:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    Birdrigidbody2d.bodyType = RigidbodyType2D.Dynamic;
                   start_state= Start_State.Playing;
                    OnPlayingStarted?.Invoke(this, EventArgs.Empty);
                    Jump();
                }
                break;
            case Start_State.Playing:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    Jump();
                }
                break;
            case Start_State.Died:
                break;
        }

        if(bird_YPosition.position.y >=55 || bird_YPosition.position.y <=-55)
        {
            OnBirdDie?.Invoke(this, EventArgs.Empty);
            Birdrigidbody2d.bodyType = RigidbodyType2D.Static;

        }

    }

    private void Jump()
    {
        Birdrigidbody2d.velocity = Vector2.up*Jump_Amount;
        SoundManager.PlaySound(SoundManager.Sound.BirdJump);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Score_Counter(collider);
        Bird_Die(collider);
    }

    public void Score_Counter(Collider2D col)
    {
      

        if (col.CompareTag("ScoreCollider"))
        {
            SoundManager.PlaySound(SoundManager.Sound.Score);
            ScoreWindow.GetInstance().current_score = ScoreWindow.GetInstance().current_score+0.5f;
            ScoreWindow.GetInstance().scoreText.text = ScoreWindow.GetInstance().current_score.ToString("0");
        }
       

    }
    public void Bird_Die(Collider2D col)
    {

        if (col.CompareTag("Pipe"))
        {
            SoundManager.PlaySound(SoundManager.Sound.Loose);

            Birdrigidbody2d.bodyType = RigidbodyType2D.Static;
            OnBirdDie?.Invoke(this, EventArgs.Empty);
            
        }
    }

    public static Bird GetInstance()
    {
        return instance;
    }

}
