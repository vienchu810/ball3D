using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool smash;
    [SerializeField] private bool invincible;

    private float currentTime;
    private int currentBrokenStacks=1;
    private int totalStacks;
    // public static Ball instance;
    public enum BallState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }
    [HideInInspector] public BallState ballState=BallState.Prepare;
    public AudioClip bounceOffClip, deadClip, winclip, destroyClip, iDestroyClip;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentBrokenStacks = 0;
        // instance=this;
    }
    private void Start()
    {
        totalStacks = FindObjectsOfType<StackController>().Length;
    }
    private void Update()
    {
        if(ballState==BallState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                smash = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                smash = false;
            }
            if (invincible)
            {
                currentTime -= Time.deltaTime * 0.35f;
            }
            else
            {
                if (smash)
                {
                    currentTime += Time.deltaTime * 0.8f;
                }
                else
                {
                    currentTime -= Time.deltaTime * 0.5f;
                }
            }
            if (currentTime >= 1)
            {
                currentTime = 1;
                invincible = true;
            }
            else if (currentTime <= 0) 
            {
                currentTime = 0;
                invincible = false;
            }
        }
        if(ballState==BallState.Prepare)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ballState = BallState.Playing;
            }
        }
        if(ballState==BallState.Finish)
        {
            if(Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().NextLevel();
            }    
        }
        

    }
    private void FixedUpdate()
    {
        
        if (ballState==BallState.Playing)
        {
            if (Input.GetMouseButton(0))
            {
                smash = true;
                rb.velocity = new Vector3(0f, -100 * Time.deltaTime * 7, 0f);
            }    
                
        }
        if(rb.velocity.y>5f)
        {
            rb.velocity = new Vector3(rb.velocity.x, 5f, rb.velocity.z);
        }    
    }
    public void IncreaseBrokenStacks()
    {
        currentBrokenStacks++;
        if(!invincible)
        {
            ScoreManager.instance.AddScore(1);
            SoundManager.instance.PlaySoundFX(destroyClip, 0.5f);
        }
        else
        {
            ScoreManager.instance.AddScore(2);
            SoundManager.instance.PlaySoundFX(iDestroyClip, 0.5f);
        }    
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (!smash)
        {
            rb.velocity=new Vector3(0f,50* Time.deltaTime*5, 0f);
            SoundManager.instance.PlaySoundFX(bounceOffClip, 0.5f);
        }
        else
        {
            if(invincible)
            {
                if (collision.gameObject.CompareTag("enemy") == true || collision.gameObject.CompareTag("plane") == true)
                {
                    collision.transform.parent.GetComponent<StackController>().ShatterAllParts();
                    SoundManager.instance.PlaySoundFX(bounceOffClip, 0.5f);
                    currentBrokenStacks++;
                }    
            }
            else
            {
                if (collision.gameObject.CompareTag("enemy") == true)
                {
                    collision.transform.parent.GetComponent<StackController>().ShatterAllParts();
                    SoundManager.instance.PlaySoundFX(bounceOffClip, 0.5f);
                    currentBrokenStacks++;
                }
                if (collision.gameObject.CompareTag("plane") == true)
                {
                    Debug.Log("Game Over");
                    ScoreManager.instance.ResetScore();
                    SoundManager.instance.PlaySoundFX(deadClip, 0.5f);
                    ballState=BallState.Died;
                    StartCoroutine(LoseGame(0.5f));
                    
                }
            }
           
        }
        FindObjectOfType<GameUI>().LevelSliderFill(currentBrokenStacks / (float)totalStacks);
        if(collision.gameObject.CompareTag("Finish") && ballState==BallState.Playing)
        {
            ballState = BallState.Finish;
            SoundManager.instance.PlaySoundFX(winclip, 0.7f);
        }    

    }
    private void OnCollisionStay(Collision collision)
    {
        if (!smash || collision.gameObject.CompareTag("Finish")==true)
        {

            rb.velocity = new Vector3(0f, 50 * Time.deltaTime * 5, 0f);
        }
    }
    IEnumerator LoseGame(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);
    }
}
