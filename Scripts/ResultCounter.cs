using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCounter : MonoBehaviour
{
    [SerializeField] private float _distanceScoreDevision = 10f;
    [SerializeField] private WorldController _worldController;
    private int _score;
    private float _distanceCount;

    public int Score { get { return _score; } }
    public float Distance { get { return _distanceCount; } }

    private void OnEnable()
    {
        PlayerController.IsDeadEvent += StopCounting;
        PlayerController.AddScoreEvent += AddScore;
    }

    private void OnDisable()
    {
        PlayerController.IsDeadEvent -= StopCounting;
        PlayerController.AddScoreEvent -= AddScore;
    }

    private void Start()
    {
            StartCoroutine(DistanceCounterCorutine());
    }

    private void AddScore()
    {
        _score++;
    }

    private void StopCounting()
    {
        StopCoroutine(DistanceCounterCorutine());
    }

    private IEnumerator DistanceCounterCorutine()
    {
        while (true)
        {
        yield return new WaitForSeconds(0.5f);
        _distanceCount = Mathf.Round((Time.timeSinceLevelLoad) / _distanceScoreDevision * _worldController.WorldSpeed);
        }
    }
}
