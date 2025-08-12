using TMPro;
using UnityEngine;

public class ScoreMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
