using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private const float Cam_Orth_Size = 50f;
    private const float pipeBody_width = 10f;
    private const float pipeHead_height = 3.75f;
    private const float Pipe_Move_Speed = 25f;
    private const float Pipe_Destroy_xPosition = -100f;
    private const float Pipe_Spawn_xPosition = 100f;
    private const   float Ground_Spawn_xPosition = 100f;
    private const float GroundYposition = 7.5f;
    private float Ground_current_xPosition;
    private float ground_spawn_time;
    private float ground_spawn_timemax = 0.1f;

    private  float ground_destroy_xPosition = -Ground_Spawn_xPosition; 

    private const float Ground_move_speed = 25f;
    private float gapSzie;
    private float Pipe_Spawn_Time;
    private float Pipe_Spawn_TimeMax;
    private float Pipe_Spawned;
    private const float scoreCollider_Yposition=-50f;
    private float Height;
    private  float Game_Restart_Time;

    private Transform scoreCollider;
    private Transform scoreColliderTemp;
    private List<Pipe> pipeList;
    private List<Ground> groundList;
    private static Level instance;
    private Game_State game_state;

    private enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard,
        Impossible,
    }

    private enum Game_State
    {
        waitingto_start,
        Playing,
        BirdDie,
    }

    private void Awake()
    {
        instance = this;
        pipeList = new List<Pipe>();
        SetDifficulty(DifficultyLevel.Easy);
        scoreCollider = GameAssets.GetInstance().scoreCollider;
        game_state = Game_State.waitingto_start;
        Game_Restart_Time = 2f;
        Ground_current_xPosition = Ground_Spawn_xPosition;
    }
    

    private void Start()
    {
        Bird.GetInstance().OnBirdDie += StopPipe_Movement;

        Bird.GetInstance().OnPlayingStarted += PipeStart_Moving_at_GameStart;
    }
    private void StopPipe_Movement(object sender ,System.EventArgs e)
    {
        game_state = Game_State.BirdDie;
      
     //   Debug.Log("Bird Die");
    }
    private void PipeStart_Moving_at_GameStart(object sender, System.EventArgs e)
    {
        game_state = Game_State.Playing;
    }
   
    private void Update()
    {
      //  Debug.Log(groundList.Count);
        if (game_state == Game_State.Playing)
        {
             HandlePipeMovement();
               HandlePipeSpawn();
            HandleGroundSpawn();
            HandleGroundMovement();
          
            
        }
    }

    public static Level GetInstance()
    {
        return instance;
    }

    private void HandlePipeSpawn()
    { Pipe_Spawn_Time -= Time.deltaTime;
        
        if (Pipe_Spawn_Time < 0)
        {
            //Spawn a new Pipe
            Pipe_Spawn_Time += Pipe_Spawn_TimeMax;
            float HeightedgeLimit = 10f;
            float minHeight = gapSzie * 0.5f + HeightedgeLimit;
            float totalHeight = Cam_Orth_Size * 2;
            float maxHeight = totalHeight - gapSzie * 0.5f - HeightedgeLimit;
            Height = Random.Range(minHeight, maxHeight);
            CreatePipe_Gap(Height, gapSzie, Pipe_Spawn_xPosition);
           




        }
    }
 private void HandleGroundSpawn()
    {

        ground_spawn_time -= Time.deltaTime;
        if(ground_spawn_time<0)
        {
            ground_spawn_time = ground_spawn_timemax;
            CreateGround(Ground_Spawn_xPosition,-46);
        }
    }
  
    private void HandlePipeMovement()
    {
        for (int i = 0; i < pipeList.Count; i++)
        {
            Pipe pipe = pipeList[i];
            pipe.Move_pipe();
            if (pipe.GetPipe_X_Position() < Pipe_Destroy_xPosition)
            {
                //Destro Pipe
                pipe.Destroy_Pipe_scoreCollider();
                pipeList.Remove(pipe);
                i--;
            }
        }
    }
    private void HandleGroundMovement()
    {
        for(int i=0;i<groundList.Count;i++)
        {
            Ground ground = groundList[i];
            ground.Move_ground();
            if(ground.Get_Ground_xPosition()> ground_destroy_xPosition)
            {
                ground.Destroy_ground();
                groundList.Remove(ground);
                i--;
            }
        }
    }
    private void SetDifficulty(DifficultyLevel difficulty)
    {
        switch(difficulty)
        {
            case DifficultyLevel.Easy:
                gapSzie = 50f;
                Pipe_Spawn_TimeMax = 1.4f;
                break;
            case DifficultyLevel.Medium:
                gapSzie = 40f;
                Pipe_Spawn_TimeMax = 1.2f;
                break;
            case DifficultyLevel.Hard:
                gapSzie = 35f;
                Pipe_Spawn_TimeMax = 1f;
                break;
            case DifficultyLevel.Impossible:
                gapSzie = 25f;
                Pipe_Spawn_TimeMax = 0.9f;
                break;

        }
    }

    private  DifficultyLevel GetDifficulty()
        {
        if (Pipe_Spawned >= 10) return DifficultyLevel.Medium;
        if (Pipe_Spawned >= 20) return DifficultyLevel.Hard;
        if (Pipe_Spawned >= 30) return DifficultyLevel.Impossible;
        return DifficultyLevel.Easy;
    }
    private void CreatePipe_Gap(float gapY , float gapSize , float xPosition)
    {
        CreatePipe(gapY - gapSize * .5f, xPosition, true);
        CreatePipe(Cam_Orth_Size*2f-gapY-gapSize*.5f,xPosition,false);

        Pipe_Spawned++;
      
        SetDifficulty(GetDifficulty());
    }

    private void CreateGround(float xPosition,float height)
    {
        Debug.Log("Ground Created");
        Transform   ground_instantiate = Instantiate(GameAssets.GetInstance().pfGround);

        ground_instantiate.position = new Vector2(xPosition,height);

        Ground ground = new Ground(ground_instantiate);
        groundList.Add(ground);
        Debug.Log(groundList.Count);
    }

   
    private void CreatePipe(float height, float xPosition, bool createBottom)
    {
      
            CreateScoreCollider();

     
        //Pipe_Head Setting
        Transform pipeHead= Instantiate(GameAssets.GetInstance().pfPipeHead);
       
        float PipeHeadYposition;
     
        if (createBottom)
        {
            PipeHeadYposition = -Cam_Orth_Size + GroundYposition + height - pipeHead_height * .5f;
        }
        else
        {
            PipeHeadYposition = +Cam_Orth_Size - height + pipeHead_height * .5f;

        }
        pipeHead.position = new Vector3(xPosition, PipeHeadYposition);

       

        //Pipe_Body Setting
        Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);

        float pipeBodyYposition;
        if(createBottom)
        {
            pipeBodyYposition = -Cam_Orth_Size+GroundYposition;
            
        }
        else
        {
           pipeBodyYposition = +Cam_Orth_Size;
            pipeBody.localScale = new Vector3(1, -1, 1);
        }
        pipeBody.position = new Vector3(xPosition, pipeBodyYposition);
        SpriteRenderer pipeBody_spriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
        pipeBody_spriteRenderer.size = new Vector2(pipeBody_width, height);
       BoxCollider2D pipeBody_boxCollider= pipeBody.GetComponent<BoxCollider2D>();
        pipeBody_boxCollider.size = new Vector2(pipeBody_width,height);
        pipeBody_boxCollider.offset = new Vector2(0f, height * 0.5f);

       

        Pipe pipe = new Pipe(pipeHead,pipeBody, scoreColliderTemp);
        pipeList.Add(pipe);

    }

    private void CreateScoreCollider()
    {
        scoreColliderTemp = Instantiate(scoreCollider);
        scoreColliderTemp.localScale = new Vector3(pipeBody_width, gapSzie);
        scoreColliderTemp.position = new Vector3(Pipe_Spawn_xPosition, scoreCollider_Yposition + Height);
     
    }

  


    private class Pipe
    {
      private Transform  scoreColliderTemp;
        private Transform pipe_Head_transform;
        private Transform pipe_Body_transform;
      public  Pipe(Transform pipe_Head_transform, Transform pipe_Body_transform, Transform scoreColliderTemp)
        {
            this.pipe_Head_transform = pipe_Head_transform;
            this.pipe_Body_transform = pipe_Body_transform;
            this.scoreColliderTemp = scoreColliderTemp;
        }

        public void Move_pipe()
        {
            pipe_Head_transform.position += new Vector3(-1, 0, 0) * Pipe_Move_Speed * Time.deltaTime; ;
            pipe_Body_transform.position += new Vector3(-1, 0, 0) * Pipe_Move_Speed * Time.deltaTime;
            scoreColliderTemp.position += new Vector3(-1, 0, 0) * Pipe_Move_Speed * Time.deltaTime;
        }

        public float GetPipe_X_Position()
        {
           return pipe_Head_transform.position.x;
        }
        public void Destroy_Pipe_scoreCollider()
        {
            Destroy(pipe_Head_transform.gameObject);
            Destroy(pipe_Body_transform.gameObject);
            Destroy(scoreColliderTemp.gameObject);

        }
    }


   private class Ground
    {
        private Transform ground_transform;
       
        public Ground(Transform ground_transform)
        {

            this.ground_transform = ground_transform;
        }

        public void Move_ground()
        {
            ground_transform.position += new Vector3(-1, 0, 0)* Ground_move_speed *Time.deltaTime;
        }

        public float Get_Ground_xPosition()
        {
            return ground_transform.position.x;
        }
        public void Destroy_ground()
        {
            Destroy(ground_transform.gameObject);
        }
    }
}


