using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class SaveLoad : MonoBehaviour
{
    public static SaveLoad script;
    private void Awake()
    {
        script = this;
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/Data.dat",FileMode.Create);
        Data value = new Data();

        value.music = StateHud.script.musicBool;
            value.platform = ShopManager.script.selectedPlatformSkin;
            value.bg = ShopManager.script.selectedBgSkin;
            value.ball = ShopManager.script.selectedBallSkin;
            value.challenges = ChallengeManager.unPassedChallenges.ToArray();
            value.gravityDownCount = StateHud.script.gravityDownCount;
            value.gravityUpCount = StateHud.script.gravityUpCount;
            value.score = StateHud.script.highScore;
            value.meters = StateHud.script.highMeters;
            value.currentAmount = StateHud.script.currentAmount;

        bf.Serialize(file, value);
        file.Close();

    }
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/Data.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Data.dat", FileMode.OpenOrCreate);
            Data value = bf.Deserialize(file) as Data;

            StateHud.script.musicBool = value.music;
            ShopManager.script.selectedPlatformSkin = value.platform;
            ShopManager.script.selectedBallSkin = value.ball;
            ShopManager.script.selectedBgSkin = value.bg;
            StateHud.script.highScore = value.score;
            StateHud.script.highMeters = value.meters;
            StateHud.script.currentAmount = value.currentAmount;
            StateHud.script.gravityDownCount = value.gravityDownCount;
            StateHud.script.gravityUpCount = value.gravityUpCount;

            ChallengeManager.unPassedChallenges = value.challenges.ToList();

            file.Close();
        }
    }
}
[System.Serializable]
public class Data
{
    public int score;
    public int meters;
    public int gravityUpCount;
    public int gravityDownCount;
    public int currentAmount;
    public int bg;
    public int ball;
    public int platform;
    public int money;
    public bool music;
    public Challenges[] challenges;
}