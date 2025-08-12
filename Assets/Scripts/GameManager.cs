using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private ScoreMenuController scoreMenuController;
    
    private bool _allowRestartGame;
    private PlayerInputActions _inputActions;
    private int _score;

    public void OnObstaclePassed()
    {
        _score++;
        scoreMenuController.SetScoreText(_score);
    }
    
    public void OnGameOver()
    {
        GameOverPublisher.GetInstance().GameOver();
        gameOverCanvas.SetActive(true);
        StartCoroutine(WaitAndAllowRestart());
    }

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }
    
    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.PlayerAction.performed += OnActionKeyPress;
    }
    
    private void OnDisable()
    {
        _inputActions.Player.PlayerAction.performed -= OnActionKeyPress;
        _inputActions.Player.Disable();
    }

    private IEnumerator WaitAndAllowRestart()
    {
        yield return new WaitForSeconds(1f);
        _allowRestartGame = true;
    }
    
    /// <summary>
    /// reset the game when the action key is pressed, if we are in the game over state
    /// </summary>
    private void OnActionKeyPress(InputAction.CallbackContext context)
    {
        if ( ! _allowRestartGame) return;
        
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);  
    }
}