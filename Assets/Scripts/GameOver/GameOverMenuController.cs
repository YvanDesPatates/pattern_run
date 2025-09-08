using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuController : MonoBehaviour
{
    [SerializeField] private int backgroundAlphaTarget = 200;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Image backgroundImage;

    private void Awake()
    {
        SetTextAlpha(0);
        SetBackgroundAlpha(0);
    }

    private void Update()
    {
        // fade in the Game Over text and background
        if (gameOverText.color.a < 1.0f)
        {
            float alpha = Mathf.Min(gameOverText.color.a + (Time.deltaTime / fadeDuration), 1.0f);
            SetTextAlpha(alpha);
        }

        if (backgroundImage.color.a < backgroundAlphaTarget / 255f)
        {
            float alpha = Mathf.Min(backgroundImage.color.a + (Time.deltaTime / fadeDuration), backgroundAlphaTarget / 255f);
            SetBackgroundAlpha(alpha);
        }
    }
    
    private void SetTextAlpha(float alpha)
    {
        Color textColor = gameOverText.color;
        textColor.a = alpha;
        gameOverText.color = textColor;
    }
    
    private void SetBackgroundAlpha(float alpha)
    {
        Color bgColor = backgroundImage.color;
        bgColor.a = alpha;
        backgroundImage.color = bgColor;
    }
}
