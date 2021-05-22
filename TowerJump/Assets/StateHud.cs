using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateHud : MonoBehaviour
{
    public static StateHud script;

    [SerializeField] TMPro.TMP_Text scoreText;
    [SerializeField] TMPro.TMP_Text metersText;
    [SerializeField] TMPro.TMP_Text discriptionText;
    [SerializeField] TMPro.TMP_Text prizeText;
    public TMPro.TMP_Text respawnTimer;

    public GameObject challangeComplited;
    public GameObject highScoreNotif;
    public GameObject highMeterNotif;

    public GameObject loseScreen;
    public GameObject gameScreen;

    public Button gravityDown;
    public Button gravityUp;

    public Button musicButton;
    public Sprite[] musicSprites;
    public bool musicBool = false;
    public AudioSource audio;

    public int score;
    public int highScore;
    public int meters;
    public int highMeters;

    public int gravityDownCount = 2;
    public int gravityUpCount = 2;

    public int currentAmount;

    public bool removeChallenge = false;
    private void Awake()
    {
        script = this;
        Application.targetFrameRate = 60;
        //QualitySettings.vSyncCount = 0;
    }
    private void Start()
    {
        SaveLoad.script.Load();
        SetRecordText();
        CheckButton();

        
    }


    public void SetChallengeText()
    {
        if (ChallengeManager.unPassedChallenges.Count > 1)
        {
            discriptionText.text = ChallengeManager.script.currentChallenge.description;
            prizeText.text = "Prize: " + ChallengeManager.script.currentChallenge.prize.ToString();
        }
        else
        {
            discriptionText.text = "End";
            prizeText.text = "";
        }
    }
    public void GoCalculateMeters()
    {
       InvokeRepeating("AddMeter", 1, 0.5f);
    }
    private void AddMeter()
    {
        if (PlayerScript.script.canStart)
        {
            metersText.text = meters++.ToString() + "m";
        }
    }
    public void SetScore(int x)
    {
        score += x;
        scoreText.text = score.ToString() + "p";
    }

    public void SetRecordText()
    {
        scoreText.text = highScore.ToString();
        metersText.text = highMeters.ToString() + " m";
    }

    public void ChangeHelpText()
    {
        gravityDown.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = gravityDownCount.ToString();
        gravityUp.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = gravityUpCount.ToString();
    }
    public void CheckButton()
    {
        if (StateHud.script.gravityDownCount == 0)
        {
            gravityDown.interactable = false;
        }
        else
        {
            gravityDown.interactable = true;
        }

        if (StateHud.script.gravityUpCount == 0)
        {
            gravityUp.interactable = false;
        }
        else
        {
            gravityUp.interactable = true;
        }
        ChangeHelpText();

    }

    public void Music()
    {
        if (!musicBool)
        {
            musicButton.image.sprite = musicSprites[1];
            musicBool = true;
            audio.Stop();
        }
        else
        {
            musicButton.image.sprite = musicSprites[0];
            musicBool = false;
            audio.Play();
        }
    }
    public void MusicLoad()
    {
        if (!musicBool)
        {
            musicButton.image.sprite = musicSprites[0];
            audio.Play();
        }
        else
        {
            musicButton.image.sprite = musicSprites[1];
            audio.Stop();
        }
    }
    public void Respawn()
    {
        challangeComplited.SetActive(false);
        highScoreNotif.SetActive(false);
        highMeterNotif.SetActive(false);
    }
    public void EndGame()
    {
        Application.LoadLevel(0);
    }
}
