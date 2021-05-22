using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Challenges {
    public static int index;

    public string description;

    public char typeOfChallenge;

    public int destination;

    public int prize = 10;

    // s -> Challange Score With Save
    // S -> Challange Score WithOut Save
    // m -> Challange Meter With Save
    // M -> Challange Meter WithOut Save
    // c -> Challange Coin With Save
    // C -> Challange Coin WithOut Save
    // e -> end

    public Challenges()
    {
        index++;
    }

}
