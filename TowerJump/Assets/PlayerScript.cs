using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript script;
    public AudioSource audioSourceBg;
    public AudioSource audioSourceJump;
    Rigidbody rg;
    Transform myTransform;

    Vector3 ySpeed = Vector3.zero;
    public float jumpForce;
    
    int jumpCount = 1;

    public float forceDuration = 1;
    public bool canStart = false;
    public bool canRestart = false;
    
    bool tapped = false;
    private void Awake()
    {
        script = this;
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 1;
        myTransform = transform;
    }
   /* private void FixedUpdate()
    {
        transform.Rotate(Vector3.right * PlatformMovement.platformSpeed * Time.deltaTime * 100);
    }*/
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        myTransform.Rotate(Vector3.right * PlatformMovement.platformSpeed * Time.deltaTime * 100);
        if (tapped)
        {
            if (forceDuration < 1.8f)
            {
                forceDuration += Time.deltaTime*1.3f;
            }
        }
        else
        {
            if(transform.position.y >= 5.7f)
            {
                rg.velocity = new Vector3(0, 0, 0);
            }
        }
        
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            jumpCount = 1;
            forceDuration = 1f;
            SpawnScript.script.Spawn();
            StateHud.script.SetScore(1);

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            canStart = false;
            PlatformMovement.platformSpeed = 0;
            StartCoroutine("Lose");
        }
        if (other.gameObject.CompareTag("GravityUp"))
        {
            StateHud.script.gravityUpCount++;
            Destroy(other.gameObject);
            StateHud.script.CheckButton();
        }
        if (other.gameObject.CompareTag("GravityDown"))
        {
            StateHud.script.gravityDownCount++;
            StateHud.script.CheckButton();
            Destroy(other.gameObject);
        }
    }

    public void DurationForce()
    {
        tapped = true;
    }
    public void Jump()
    {
        audioSourceJump.Play();
        forceDuration = 1f;
        tapped = false;
        if (jumpCount < 4)
        {
            jumpCount++;
            Vector3 ySpeed = new Vector3(0, jumpForce / (jumpCount * (forceDuration-0.05f)), 0);
            rg.velocity += ySpeed;   
        }
    }


    public void PlayGame()
    {
        canStart = true;
        jumpCount++;
        StateHud.script.GoCalculateMeters();
        audioSourceJump.Play();
        PlatformMovement.platformSpeed = 2;
        rg.velocity += new Vector3(0, jumpForce / (jumpCount * forceDuration), 0);

    }

    public void SlowDownForButton()
    {
        if (StateHud.script.gravityUpCount > 0)
        {
            StateHud.script.gravityUpCount--;
            StartCoroutine("SlowDown");
        }
        else
        {
            StateHud.script.gravityUp.interactable = false;
        }
        StateHud.script.CheckButton();
    }

   

    IEnumerator SlowDown()
    {
        StateHud.script.gravityUp.interactable = false;
        Physics.gravity = new Vector3(0, -7f, 0);
        yield return new WaitForSeconds(1.8f);
        Physics.gravity = new Vector3(0, -9.81f, 0);
        StateHud.script.gravityUp.interactable = true;
    }

    public void DownForButton()
    {
        if (StateHud.script.gravityDownCount > 0)
        {
            StateHud.script.gravityDownCount--;
            StartCoroutine("Down");
        }
        else
        {
            StateHud.script.gravityDown.interactable = false;
        }
        StateHud.script.CheckButton();
    }
    IEnumerator Down()
    {
        StateHud.script.gravityDown.interactable = false;
        Physics.gravity = new Vector3(0, -15f, 0);
        yield return new WaitForSeconds(0.5f);
        Physics.gravity = new Vector3(0, -9.81f, 0);
        StateHud.script.gravityDown.interactable = true;
    }
    IEnumerator Lose()
    {
        canRestart = true;
        StateHud.script.loseScreen.SetActive(true);
        StateHud.script.gameScreen.SetActive(false);
        CheckChallenge();
        SetHighScoreAndMeter();
        canStart = false;
        Physics.gravity = new Vector3(0, -9.81f, 0);
        SaveLoad.script.Save();

        yield return new WaitForSeconds(3f);

        if (canRestart)
        {
            StateHud.script.EndGame();
        }
    }



    public void SetHighScoreAndMeter()
    {
        if (StateHud.script.meters > StateHud.script.highMeters)
        {
            StateHud.script.highMeters = StateHud.script.meters;
            StateHud.script.highMeterNotif.SetActive(true);
        }
        if (StateHud.script.score > StateHud.script.highScore)
        {
            StateHud.script.highScore = StateHud.script.score;
            StateHud.script.highScoreNotif.SetActive(true);
        }
    }
    public void CheckChallenge()
    {
        switch (ChallengeManager.script.currentChallenge.typeOfChallenge)
        {
            case 's':
                StateHud.script.currentAmount += StateHud.script.score;
                break;
            case 'm':
                StateHud.script.currentAmount += StateHud.script.meters;
                break;
            case 'S':
                StateHud.script.currentAmount = StateHud.script.score;
                break;
            case 'M':
                StateHud.script.currentAmount = StateHud.script.meters;
                break;
            default:
                break;
        }

        if (ChallengeManager.script.currentChallenge.destination <= StateHud.script.currentAmount)
        {
            StateHud.script.challangeComplited.gameObject.SetActive(true);
            ChallengeManager.script.RemovePassedChallenge();
            ChallengeManager.script.currentChallenge = ChallengeManager.script.challenges[ChallengeManager.script.challenges.Length-1];
            StateHud.script.currentAmount = 0;
        }
    }
}
