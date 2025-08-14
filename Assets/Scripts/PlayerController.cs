using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private GameObject jetpack;

    public Rigidbody PlayerRb { get; private set; }
    public bool IsOnGround { get; set; } = true;

    private PlayerInputActions _inputActions;
    private Animator _playerAnim;
    private AudioSource _playerAudio;
    private bool _isGameOver = false;
    private GameManager _gameManager;
    private IMovementStrategy _movementStrategy;
    
    public void ChangeMovementStrategy(MovementStrategyEnum newMovementStrategy)
    {
        if (_movementStrategy is not null)
        {
            _movementStrategy.ResetBeforeDestroy(this);
        }
        _movementStrategy = MovementStrategyEnumUtil.GetMovementStrategy(newMovementStrategy);
    }
    
    public void SetAnimationTrigger(string triggerName)
    { 
        _playerAnim.SetTrigger(triggerName);
    }
    
    public void SetAnnimationFloat(string parameterName, float value)
    {
        _playerAnim.SetFloat(parameterName, value);
    }

    public void PlayJumpSound()
    {
        _playerAudio.PlayOneShot(jumpSound, 1);
    }
    
    public void SetJetpackVisibility(bool isVisible)
    {
        jetpack.SetActive(isVisible);
    }
    
    #region Unity Callbacks
    private void Awake()
    {
        _gameManager = Util.FindObjectOfTypeOrLogError<GameManager>();
        _inputActions = new PlayerInputActions();
        _movementStrategy = new JetpackStrategy();
    }
    
    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.PlayerAction.performed += OnActionKeyPress;
        _inputActions.Player.PlayerAction.canceled += OnActionKeyRelease;
    }

    private void OnDisable()
    {
        _inputActions.Player.PlayerAction.performed -= OnActionKeyPress;
        _inputActions.Player.PlayerAction.canceled -= OnActionKeyRelease;
        _inputActions.Player.Disable();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        PlayerRb = Util.GetComponentOrLogError<Rigidbody>(gameObject);
        _playerAnim = Util.GetComponentOrLogError<Animator>(gameObject);
        _playerAudio = Util.GetComponentOrLogError<AudioSource>(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
            dirtParticle.Play();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            OnGameOver();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            dirtParticle.Stop();
            IsOnGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SucessZone") && !_isGameOver)
        {
            _gameManager.OnObstaclePassed();
        }
    }

    private void Update()
    {
        if (_isGameOver) return;
        
        _movementStrategy.Update(this);
    }

    private void OnActionKeyPress(InputAction.CallbackContext context)
    {
        if (_isGameOver) return;
        
        _movementStrategy.OnActionKeyPressed(this);
    }

    private void OnActionKeyRelease(InputAction.CallbackContext context)
    {
        if (_isGameOver) return;
        
        _movementStrategy.OnActionKeyReleased(this);
    }
    #endregion

    private void OnGameOver()
    {
        _gameManager.OnGameOver();
        _isGameOver = true;
        explosionParticle.Play();
        dirtParticle.Stop();
        _playerAnim.SetBool("Death_b", true);
        _playerAnim.SetInteger("DeathType_int", 1);
        _playerAudio.PlayOneShot(crashSound, 1);
        _movementStrategy.ResetBeforeDestroy(this);
    }
}
