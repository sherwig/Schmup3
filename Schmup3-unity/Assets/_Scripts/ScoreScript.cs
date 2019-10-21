using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public Text score;
    public int scoreInt;
    public static ScoreScript S;

    // Start is called before the first frame update
    public void Awake()
    {
        scoreInt = 0;
        DisplayScore();
        S = this;

    }

    // Update is called once per frame
    public void UpdateScore()
    {
        //if (kill == 1)
       // {
            scoreInt++;

        //print( scoreInt);
        //}

        DisplayScore();

    }

    public int getScore()
    {
        return (scoreInt);
    }

    private void DisplayScore()
    {
        score.text = scoreInt.ToString();

    }
}
