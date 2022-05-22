using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiManager: MonoBehaviour
{
    [SerializeField] private List<GameObject> menus;
    [SerializeField] private List<GameObject> mainMenuButtons;

    private PlayerInput _uiInput;
    private PlayerController _player;
    private int _activeMenu;

    private void OnEnable()
    {
        _uiInput = new PlayerInput();
        _uiInput.UI.Enable();
        _uiInput.UI.Pause.performed += OnPause;
        _player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        menus[0].SetActive(true);
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
}
