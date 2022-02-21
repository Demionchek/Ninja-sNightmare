using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnSettingsScrObj", menuName = "ScriptableObjects/SpawnSettingsScrObj", order = 1)]
public class SpawnControllerScriptableObject : ScriptableObject
{
    [Header("Spawn Frequency Settings")]
    [Tooltip("set from 0 to 1")]
    [SerializeField] private float _startObstacleFrequency;
    [SerializeField] private float _speedToIncreaseObstacleFreq;
    [Tooltip("set from 0 to 1")]
    [SerializeField] private float _secondObstacleFrequency;
    [SerializeField] private float _speedToInstantObstacleFreq;

    public float StartObstacleFrequency { get { return _startObstacleFrequency; } }
    public float SecondObstacleFrequency { get { return _secondObstacleFrequency; } }
    public float SpeedToIncreaseObstacleFreq { get { return _speedToIncreaseObstacleFreq; } }
    public float SpeedToInstantObstacleFreq { get { return _speedToInstantObstacleFreq; } }
}
