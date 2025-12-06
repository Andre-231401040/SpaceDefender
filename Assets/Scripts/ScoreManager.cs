using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public int currentScore;

    public void ChangeScore(int points)
    {
        currentScore += points;
        scoreText.text = currentScore.ToString();
    }

}
