using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private Animator _startButton;
    [SerializeField] private Animator _settingsButton;
    [SerializeField] private Animator _exitButton;
    [SerializeField] private Animator _settingsPanel;

    private void Start()
    {
        Time.timeScale = 1f;
        _startButton.SetBool("_isSlided", false);    
        _settingsButton.SetBool("_isSlided", false);
        _exitButton.SetBool("_isSlided", false);
        _settingsPanel.SetBool("_isSlided", false);
    }


    public void OnExitSettingsClick()
    {
        _startButton.SetBool("_isSlided", false);
        _settingsButton.SetBool("_isSlided", false);
        _exitButton.SetBool("_isSlided", false);
        _settingsPanel.SetBool("_isSlided", false);
    }

    public void OnSettingsClick()
    {
        _startButton.SetBool("_isSlided", true);
        _settingsButton.SetBool("_isSlided", true);
        _exitButton.SetBool("_isSlided", true);
        _settingsPanel.SetBool("_isSlided", true);
    }
}
