using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private GameObject jetpack;
    [SerializeField] private GameManager _gameManager;

    public Rigidbody PlayerRb { get; private set; }
    public bool IsOnGround { get; set; } = true;

    private PlayerInputActions _inputActions;
    private Animator _playerAnim;
    private AudioSource _playerAudio;
    private bool _isGameOver = false;
    
    //jump
    private const float JumpForce = 650f;
    private const float JumpGravityModifier = 2f;
    private static readonly Vector3 InitialGravity = Physics.gravity;
    private bool _keyIsBeingPressed = false;
    
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
        _inputActions = new PlayerInputActions();
    }
    
    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.PlayerAction.performed += OnActionKeyPress;
        _inputActions.Player.PlayerAction.canceled += OnActionKeyRelease;
        Physics.gravity = InitialGravity * JumpGravityModifier;
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
            OnPlayerDied();
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
        
        if ( !_keyIsBeingPressed || ! IsOnGround) return;
        
        IsOnGround = false;
        PlayerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        SetAnimationTrigger("Jump_trig"); 
        PlayJumpSound();
    }

    private void OnActionKeyPress(InputAction.CallbackContext context)
    {
        if (_isGameOver) return;
        
        _keyIsBeingPressed = true;

    }

    private void OnActionKeyRelease(InputAction.CallbackContext context)
    {
        if (_isGameOver) return;
        
        _keyIsBeingPressed = false;
    }
    #endregion

    private void OnPlayerDied()
    {
        _isGameOver = true;
        explosionParticle.Play();
        dirtParticle.Stop();
        _playerAnim.SetBool("Death_b", true);
        _playerAnim.SetInteger("DeathType_int", 1);
        _playerAudio.PlayOneShot(crashSound, 1);
        Physics.gravity = InitialGravity;
    }
}
