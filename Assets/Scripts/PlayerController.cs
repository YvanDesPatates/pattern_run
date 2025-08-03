using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    
    private Rigidbody _playerRb;
    private bool _isOnGround = true;
    private bool _gameOver = false;
    private Animator _playerAnim;
    private AudioSource _playerAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        _playerAnim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0) && _isOnGround && !_gameOver)
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
            GameOver();
        }
    }

    private void GameOver()
    {
        _gameOver = true;
        explosionParticle.Play();
        dirtParticle.Stop();
        _playerAnim.SetBool("Death_b", true);
        _playerAnim.SetInteger("DeathType_int", 1);
        _playerAudio.PlayOneShot(crashSound, 1);
    }

    public bool IsGameOver()
    {
        return _gameOver;
    }
}
