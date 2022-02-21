using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMovement : MonoBehaviour
{
    private float platfSpeed;
    private bool _isMoving = true;
    private WorldController _worldController;

    public void GetWorldController(WorldController worldController)
    {
        _worldController = worldController;
    }

    private void Update()
    {
        platfSpeed = _worldController.WorldSpeed;

        if (_isMoving)
        {
            transform.position -= Vector3.forward * platfSpeed * Time.deltaTime;
        }
        else if (gameObject != null)
        {
             transform.position -= Vector3.forward * 0;
        }

        if (transform.position.z < -5) Destroy(gameObject);
    }

    private void StopMoving()
    {
        _isMoving = false;
    }

    private void OnEnable()
    {
        PlayerController.IsDeadEvent += StopMoving;
    }

    private void OnDisable()
    {
        PlayerController.IsDeadEvent -= StopMoving;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Destroy(gameObject);
    }
}
