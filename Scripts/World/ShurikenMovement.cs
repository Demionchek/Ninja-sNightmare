using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 2.5f;
    private bool _max;
    private bool _min;
    private bool _isMoving = true;
    private float platfSpeed;
    private WorldController _worldController;

    public void GetWorldController(WorldController worldController)
    {
        _worldController = worldController;
    }

    void Start()
    {
        _max = false;
        _min = true;
    }

    private void OnEnable()
    {
        PlayerController.IsDeadEvent += StopMoving;
    }

    private void OnDisable()
    {
        PlayerController.IsDeadEvent -= StopMoving;
    }

    private void StopMoving()
    {
        _isMoving = false;
    }

    void Update()
    {
        if (transform.position.x < 4 & !_max)
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            if (transform.position.x > 3)
            {
                _max = true;
                _min = false;
            }
        }

        if (transform.position.x > -4 & !_min)
        {
            transform.Translate(-_speed * Time.deltaTime, 0, 0);
            if (transform.position.x < -3)
            {
                _min = true;
                _max = false;
            }
        }

        platfSpeed = _worldController.WorldSpeed;

        if (gameObject != null & _isMoving)
        {
            transform.position -= Vector3.forward * platfSpeed * Time.deltaTime;
        }
        else if (gameObject != null)
        {
            transform.position -= Vector3.forward * 0;
        }

        if (transform.position.z < -5) Destroy(gameObject);

    }
}
