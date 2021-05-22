using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public static SpawnScript script;

    [SerializeField] GameObject[] platform;
    [SerializeField] GameObject[] helps;
    [SerializeField] GameObject tempPlatform;

    float tempPosZ;
    float tempPosY;
    private void Awake()
    {
        script = this;
    }
    void Start()
    {

        for (int i = 0; i < 8; i++)
        {
            Spawn();
        }
        
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }*/
    }

    public void Spawn()
    {
        tempPosZ = Random.Range(1.8f, 3.9f);
        tempPosY = Random.Range(-4f, -2f); 
        float tempHelp = Random.Range(0, 100);
        int x = Random.Range(0, 100);
        int y;
        if(x < 80)
        {
            y = 0;
        }
        else if(x >= 80 && x < 85)
        {
            y = 1;
        }
        else if (x >= 85 && x < 90)
        {
            y = 2;
        }
        else if(x >= 90 && x < 95)
        {
            y = 3;
        }
        else
        {
            y = 4;
            tempPosY = -3.8f;
        }

        tempPlatform = Instantiate(platform[y], new Vector3(0 , tempPosY, tempPlatform.transform.position.z + tempPosZ), Quaternion.identity) as GameObject;


        if (tempHelp >= 92)
        {
            Instantiate(helps[Random.Range(0,helps.Length)], new Vector3(0, tempPlatform.transform.position.y + 4.5f, tempPlatform.transform.position.z), Quaternion.Euler(0,-90,0), tempPlatform.transform);
        }
    }

    public void Respawn()
    {
        PlayerScript.script.canRestart = false;
        StateHud.script.loseScreen.SetActive(false);
        StateHud.script.gameScreen.SetActive(true);
        StateHud.script.Respawn();
        GameObject[] x = GameObject.FindGameObjectsWithTag("Platform");
        for (int i = 0; i < x.Length; i++)
        {
            Destroy(x[i].gameObject);
        }

        tempPlatform = Instantiate(platform[0], new Vector3(0, -3.379f, 5.59f), Quaternion.identity) as GameObject;
        for (int i = 0; i < 8; i++)
        {
            Spawn();
        }
        PlayerScript.script.gameObject.transform.position = new Vector3(0, 0.94f, 5.53f);
        
        StartCoroutine("WaitForSpawn");
    }
    IEnumerator WaitForSpawn()
    {
        StateHud.script.respawnTimer.gameObject.SetActive(true);
        StateHud.script.respawnTimer.text = "3";
        yield return new WaitForSeconds(1f);
        StateHud.script.respawnTimer.text = "2";
        yield return new WaitForSeconds(1f);
        StateHud.script.respawnTimer.text = "1";
        yield return new WaitForSeconds(1f);
        StateHud.script.respawnTimer.gameObject.SetActive(false);

        PlayerScript.script.canStart = true;
        PlatformMovement.platformSpeed = 2;
    }
}
