using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    public TMP_Text scoreText;

    private void Update()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.GetScore();
    }
}
