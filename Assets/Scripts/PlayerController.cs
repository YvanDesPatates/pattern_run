using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    
    private PlayerInputActions _inputActions;
    private Rigidbody _playerRb;
    private bool _isOnGround = true;
    private bool _gameOver = false;
    private Animator _playerAnim;
    private AudioSource _playerAudio;
    private bool _actionKeyIsPressed = false;
    private GameManager _gameManager;
    
    private static Vector3 _initialGravity = Physics.gravity;
    
    private void Awake()
    {
        _gameManager = Util.FindObjectOfTypeOrLogError<GameManager>();
        _inputActions = new PlayerInputActions();
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
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        Debug.Log(Physics.gravity);
        Physics.gravity = _initialGravity * gravityModifier;
        _playerAnim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameOver) return;
        
        
        if (_actionKeyIsPressed && _isOnGround)
        {
            _playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _isOnGround = false;
            _playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            _playerAudio.PlayOneShot(jumpSound, 1);
        }   
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
            dirtParticle.Play();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            OnGameOver();
        }
    }
    
    private void OnActionKeyPress(InputAction.CallbackContext context)
    {
        _actionKeyIsPressed = true;
    }

    private void OnActionKeyRelease(InputAction.CallbackContext context)
    {
        _actionKeyIsPressed = false;
    }

    private void OnGameOver()
    {
        _gameManager.OnGameOver();
        _gameOver = true;
        explosionParticle.Play();
        dirtParticle.Stop();
        _playerAnim.SetBool("Death_b", true);
        _playerAnim.SetInteger("DeathType_int", 1);
        _playerAudio.PlayOneShot(crashSound, 1);
    }
}
