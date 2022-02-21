using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpHeigh = 1.7f;
    private RagdollController _ragdollController;
    private PlayerAnimations _playerAnimations;
    private CharacterController _cc;
    private BoxCollider _boxCollider;
    private Vector3 currentTransform;
    private Vector3 Velocity;
    private float currentDir = 0f;
    private float gravity = 35f;
    private float charSpeed = 20f;
    private bool is_Grounded;
    private bool alive = true;
    private bool _isInMovement = false;

    public delegate void Player();
    public static event Player IsDeadEvent;
    public static event Player JumpedEvent;
    public static event Player GroundedEvent;
    public static event Player NotGroundedEvent;
    public static event Player AddScoreEvent;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _cc = GetComponent<CharacterController>();
        _ragdollController = GetComponent<RagdollController>();
        _playerAnimations = GetComponent<PlayerAnimations>();
    }


    void Start()
    {
        Velocity = transform.position;
        alive = true;
    }

    void Update()
    {
        _ragdollController.CheckRagdollPos();

        currentTransform = transform.position;

        if (_cc.enabled == true)
            _cc.Move(Velocity * Time.deltaTime);

        is_Grounded = _cc.isGrounded;

        StatesController();

        if (_isInMovement & alive)
        {
            Moving();
        }
    }

    private void StatesController()
    {
        float dir = Input.GetAxisRaw("Horizontal");

        _playerAnimations.GetInputForTurning();

        if (is_Grounded & alive)
        {
            GroundedEvent();
            Velocity.y = -0.2f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                JumpedEvent();
            }
        }
        else if (is_Grounded & alive == false)
        {
            Velocity.y = -0.2f;
        }
        else
        {
            Velocity.y -= gravity * Time.deltaTime;
            NotGroundedEvent();
        }

        if (_isInMovement == false & dir != 0)
        {
            _isInMovement = true;

            currentDir = dir;
        }
    }

    private void Jump()
    {
        Velocity.y += Mathf.Sqrt(jumpHeigh * 3f * gravity);
        _cc.Move(Velocity * Time.deltaTime);
    }

    private void Moving()
    {
        if (currentDir == 0)
        {
            _isInMovement = false;
            return;
        }

        float tmpDist = Time.deltaTime * charSpeed;

        if (currentTransform.x == Mathf.Clamp(currentTransform.x, -3f, 3f))
        {
            _cc.Move(Vector3.right * currentDir * tmpDist);
        }
        if (currentTransform.x == Mathf.Clamp(currentTransform.x, 2.8f, 3.5f) & currentDir < 0)
        {
            _cc.Move(Vector3.right * currentDir * tmpDist);
        }
        if (currentTransform.x == Mathf.Clamp(currentTransform.x, -3.5f, -2.8f) & currentDir > 0)
        {
            _cc.Move(Vector3.right * currentDir * tmpDist);
        }

        currentDir = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _playerAnimations.PlayerAnimator.enabled = false;
            _cc.enabled = false;
            _boxCollider.enabled = false;
            alive = false;
            _ragdollController.SetRagdollFree();
            if (IsDeadEvent != null)
            {
                IsDeadEvent();
            }
        }

        if (other.CompareTag("Soul"))
        {
            AddScoreEvent();
        }
    }
}