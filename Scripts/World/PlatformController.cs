using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Transform _endPlatform;
    private WorldController _worldController;
    private WorldBuilder _worldBuilder;

    public Transform EndPlatform { get { return _endPlatform; } }

    public void GetWorldController(WorldController worldController)
    {
        _worldController = worldController;
    }

    public void GetWorldBuilder(WorldBuilder worldBuilder)
    {
        _worldBuilder = worldBuilder;
    }

    private void Awake()
    {
        GetComponent<Spawner>().GetWorldController(_worldController);
    }

    void Start()
    {
        _worldController.OnPlatformMovement += TryDelAndAddPlatform;
    }

    private void TryDelAndAddPlatform()
    {
        if (transform.position.z < _worldController.MinZ)
        {
            _worldBuilder.CreatePlatform();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (_worldController != null)
        _worldController.OnPlatformMovement -= TryDelAndAddPlatform;
    }
}
