using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _diedPanel;
    [SerializeField] private GameObject _soulsObj;
    [SerializeField] private GameObject _distanceObj;
    [SerializeField] private Text _bestSoulCount;
    [SerializeField] private Text _bestDistanceCount;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _distanceScoreText;
    private ResultCounter _resultCounter;
    private DataSaver _dataSaver;
    private bool _isPlayerAlive;
    private bool _isPaused;
    private int _counter;

    private void Awake()
    {
        _resultCounter = GetComponent<ResultCounter>();
        _dataSaver = GetComponent<DataSaver>();
    }

    private void Start()
    {
        _soulsObj.SetActive(true);
        _distanceObj.SetActive(true);
        _isPaused = false;
        _isPlayerAlive = true;
        _counter = 0;
    }

    private void OnEnable()
    {
        PlayerController.IsDeadEvent += PlayerDied;
    }

    private void OnDisable()
    {
        PlayerController.IsDeadEvent -= PlayerDied;
    }

    private void PlayerDied()
    {
        _isPlayerAlive = false;
    }

    void Update()
    {
        _scoreText.text = "" + _resultCounter.Score.ToString();
        _distanceScoreText.text = "" + _resultCounter.Distance.ToString();

        if (Input.GetKeyDown(KeyCode.Escape) & _isPlayerAlive)
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (!_isPlayerAlive & _counter == 0)
        {
            _soulsObj.SetActive(false);
            _distanceObj.SetActive(false);
            _dataSaver.NewGameData();
            _dataSaver.LoadDistanceData();
            _dataSaver.LoadScoreData();
            SavingGameData();
            Invoke("Died", 1f);
            _counter++;
        }
    }

    private void SavingGameData()
    {
        if (_dataSaver.NewScore > _dataSaver.SavedScore)
        {
            _dataSaver.SaveScore();
        }

        if (_dataSaver.NewDistance > _dataSaver.SavedDistance)
        {
            _dataSaver.SaveDistance();
        }
    }

    private void Died()
    {
        _bestSoulCount.text =_dataSaver.SavedScore.ToString();
        _bestDistanceCount.text = _dataSaver.SavedDistance.ToString();
        _diedPanel.SetActive(true);
    }

    private void Pause()
    {
        if (_pauseMenu != null) _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
        if (_settingsPanel != null) _settingsPanel.SetActive(false);
        if (_pauseMenu != null) _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenSettings()
    {
        if (_pauseMenu != null) _pauseMenu.SetActive(false);
        if (_settingsPanel != null) _settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (_pauseMenu != null) _pauseMenu.SetActive(true);
        if (_settingsPanel != null) _settingsPanel.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
