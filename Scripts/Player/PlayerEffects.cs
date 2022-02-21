using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dust;
    [SerializeField] private ParticleSystem _hit;
    [SerializeField] private ParticleSystem _soulGrab;
    private bool _isDustPlayed;

    private void Start()
    {
        _dust.Stop();
        _hit.Stop();
        _soulGrab.Stop();
    }

    private void OnEnable()
    {
        PlayerController.IsDeadEvent += PlayerDied;
        PlayerController.GroundedEvent += PlayerGrounded;
        PlayerController.AddScoreEvent += SoulGrab;
        PlayerController.JumpedEvent += PlayerJumped;
    }

    private void OnDisable()
    {
        PlayerController.IsDeadEvent -= PlayerDied;
        PlayerController.GroundedEvent -= PlayerGrounded;
        PlayerController.AddScoreEvent -= SoulGrab;
        PlayerController.JumpedEvent -= PlayerJumped;
    }

    private void PlayerDied()
    {
        _hit.Play();
    }

    private void PlayerJumped()
    {
        _isDustPlayed = false;
    }

    private void PlayerGrounded()
    {
        if (!_isDustPlayed)
        {
            _dust.Play();
            _isDustPlayed = true;
        }
    }

    private void SoulGrab()
    {
        _soulGrab.Play();
    }
}
