using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    [SerializeField] private float _worldSpeed = 10f;
    [SerializeField] private WorldBuilder _worldBuilder;
    [SerializeField] private float _minZ = -20;
    private float _currSpeed;
    private float _counter = 2f;
    private bool _isMoving = true;

    public WorldBuilder WBuilder { get { return _worldBuilder; } }
    public float WorldSpeed { get { return _worldSpeed; } }
    public float MinZ { get { return _minZ; } }

    public delegate void TryToDelAndAddPlatform();
    public event TryToDelAndAddPlatform OnPlatformMovement;

    private void OnEnable()
    {
        PlayerController.IsDeadEvent += StopMovement;
    }

    private void OnDisable()
    {
        PlayerController.IsDeadEvent -= StopMovement;
    }

    private void StopMovement()
    {
        _isMoving = false;
    }

    void Start()
    {
        StartCoroutine(OnPlatformMovementCorutine());
    }

    void Update()
    {
        _currSpeed = _worldSpeed;

        if (_isMoving)
        {

            transform.position -= Vector3.forward * _worldSpeed * Time.deltaTime;

            _counter -= Time.deltaTime;
            if (_counter < 0)
            {
                _worldSpeed = _currSpeed + 0.2f;

                _counter = 2f;
            }

        }
        else StopAllCoroutines();
    }

    IEnumerator OnPlatformMovementCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (OnPlatformMovement != null)
                OnPlatformMovement();
        }
    }


}
