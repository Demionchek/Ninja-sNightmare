using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _footstepsSFX;
    [SerializeField] private AudioSource _hitSoundSFX;
    [SerializeField] private AudioSource _jumpSound;
    private bool _isFootstepsOn;

    private void Start()
    {
        _footstepsSFX.Play();
        _hitSoundSFX.Stop();
        _jumpSound.Stop();
    }

    private void OnEnable()
    {
        PlayerController.IsDeadEvent += PlayerIsDead;
        PlayerController.GroundedEvent += PlayerIsGrounded;
        PlayerController.JumpedEvent += PlayerJumped;
        PlayerController.NotGroundedEvent += PlayerNotGrounded;
    }

    private void OnDisable()
    {
        PlayerController.IsDeadEvent -= PlayerIsDead;
        PlayerController.GroundedEvent -= PlayerIsGrounded;
        PlayerController.JumpedEvent -= PlayerJumped;
        PlayerController.NotGroundedEvent -= PlayerNotGrounded;
    }

    private void PlayerNotGrounded()
    {
        _footstepsSFX.Stop();
        _isFootstepsOn = false;
    }

    private void PlayerJumped()
    {
        _footstepsSFX.Stop();
        _jumpSound.Play();
        _isFootstepsOn = false;
    }

    private void PlayerIsGrounded()
    {
        if (!_isFootstepsOn)
        {
            _footstepsSFX.Play();
            _isFootstepsOn = true;
        }
    }

    private void PlayerIsDead()
    {
        _hitSoundSFX.Play();
        _footstepsSFX.Stop();
    }


}
