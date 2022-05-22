using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> menus;
    [SerializeField] private List<GameObject> mainMenuButtons;
    [SerializeField] private TextMeshProUGUI karmaScore;

    private PlayerInput _uiInput;
    private PlayerController _player;
    private int _activeMenu;

    private void Start()
    {
        menus[0].SetActive(true);
    }

    private void OnEnable()
    {
        _uiInput = new PlayerInput();
        _uiInput.UI.Enable();
        _uiInput.UI.Pause.performed += OnPause;
        _player = FindObjectOfType<PlayerController>();
    }

    public void StartGame()
    {
        mainMenuButtons[0].SetActive(false);
        mainMenuButtons[1].SetActive(true);
        menus[0].SetActive(false);
        _player.isActive = true;
    }

    public void Continue()
    {
        menus[0].SetActive(false);
        _player.isActive = true;
    }

    public void ChangeMenu(int targetMenu)
    {
        menus[targetMenu].SetActive(true);
        menus[_activeMenu].SetActive(false);
        _activeMenu = targetMenu;
    }

    public void ExitButton()
    {
        Application.Quit(0);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        _player.isActive = false;
        menus[0].SetActive(true);
    }

    public void UpdateKarma(int karmaAmount)
    {
        karmaScore.SetText($"Karma: {karmaAmount}");
    }
}