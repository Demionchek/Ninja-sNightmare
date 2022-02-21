using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _playerAnimator;

    public Animator PlayerAnimator 
    {
        get
        {
            return _playerAnimator;
        }
        set 
        {
            _playerAnimator.enabled = value;
        }
    }

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerController.GroundedEvent += SetGroundedTrue;
        PlayerController.JumpedEvent += SetJumpTrigger;
        PlayerController.NotGroundedEvent += SetGroundedFalse;
    }

    private void OnDisable()
    {
        PlayerController.GroundedEvent -= SetGroundedTrue;
        PlayerController.JumpedEvent -= SetJumpTrigger;
        PlayerController.NotGroundedEvent -= SetGroundedFalse;
    }

    private void SetGroundedFalse()
    {
        _playerAnimator.SetBool("IsGrounded", false);
    }

    private void SetJumpTrigger()
    {
        _playerAnimator.SetTrigger("Jump");
    }

    private void SetGroundedTrue()
    {
        _playerAnimator.SetBool("IsGrounded", true);
    }

    public void GetInputForTurning()
    {
        if (Input.GetKeyUp(KeyCode.A)) _playerAnimator.SetBool("LeftTurning", false);
        if (Input.GetKeyDown(KeyCode.A)) _playerAnimator.SetBool("LeftTurning", true);

        if (Input.GetKeyUp(KeyCode.D)) _playerAnimator.SetBool("RightTurning", false);
        if (Input.GetKeyDown(KeyCode.D)) _playerAnimator.SetBool("RightTurning", true);
    }
}
