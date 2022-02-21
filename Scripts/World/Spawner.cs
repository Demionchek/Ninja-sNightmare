using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _positions;
    [SerializeField] private GameObject _shuriken;
    [SerializeField] private GameObject _blade;
    [SerializeField] private GameObject _soul;
    [SerializeField] private SpawnControllerScriptableObject _spwnControll;

    private WorldController _worldController;
    private float _worldSpeed;

    public void GetWorldController(WorldController worldController)
    {
        _worldController = worldController;
    }

    private void Start()
    {
        if (_worldController != null)
        {
            _worldSpeed = _worldController.WorldSpeed;

            if (_worldSpeed < _spwnControll.SpeedToIncreaseObstacleFreq &
                                            Random.value <= _spwnControll.StartObstacleFrequency)
            {
                ObstaclesGeneration();
            }
            if (_worldSpeed > _spwnControll.SpeedToIncreaseObstacleFreq & 
                            _worldSpeed < _spwnControll.SpeedToInstantObstacleFreq & 
                                                Random.value <= _spwnControll.SecondObstacleFrequency)
            {
                ObstaclesGeneration();
            }
            if (_worldSpeed > _spwnControll.SpeedToInstantObstacleFreq)
            {
                ObstaclesGeneration();
            }
            if (Random.value >= 0.5)
            {
                SoulGeneration();
            }
        }
    }

    void SoulGeneration()
    {
        int i = Random.Range(0, _positions.Length);
        GameObject thisSoul = Instantiate(_soul, _positions[i].transform.position, Quaternion.identity);
        thisSoul.GetComponent<SoulMovement>().GetWorldController(_worldController);
    }

    void ObstaclesGeneration()
    {
        int i = Random.Range(0, _positions.Length);
        if (Random.value >= 0.5)
        {
            GameObject thisShuriken = Instantiate(_shuriken, _positions[i].transform.position, Quaternion.Euler(-30, 0, 0));
            thisShuriken.GetComponent<ShurikenMovement>().GetWorldController(_worldController);
        }
        else
        {
            GameObject thisBlade = Instantiate(_blade, _positions[i].transform.position, Quaternion.Euler(0, 0, 0));
            thisBlade.GetComponent<BladeMovement>().GetWorldController(_worldController);
        }
    }
}
