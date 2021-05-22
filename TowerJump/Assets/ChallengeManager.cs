using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChallengeManager : MonoBehaviour
{

    public static ChallengeManager script;
    public static List<Challenges> unPassedChallenges;
    public Challenges[] challenges;
    public Challenges currentChallenge;
    int randomIndex;

    private void Awake()
    {
        script = this;
    }
    public void FirstRun()
    {
        if (unPassedChallenges == null || unPassedChallenges.Count == 0)
        {
            unPassedChallenges = challenges.ToList<Challenges>();

        }
        currentChallenge = unPassedChallenges[0];
    }
    public void RemovePassedChallenge()
    {
        if (unPassedChallenges.Count > 1)
        {
            unPassedChallenges.RemoveAt(randomIndex);
        }
    }
    void Start()
    {
        FirstRun();
        StateHud.script.SetChallengeText();
    }

    
}
