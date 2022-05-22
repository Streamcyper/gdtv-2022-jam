using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> icons;
    private States _states;

    private void Awake()
    {
        _states.MusicIsActive = true;
        _states.SfxIsActive = true;
    }

    public void MusicMute(Image image)
    {
        if (_states.MusicIsActive)
        {
            image.sprite = icons[0];
            _states.MusicIsActive = false;
        }
        else
        {
            image.sprite = icons[1];
            _states.MusicIsActive = true;
        }
    }

    public void SfxMute(Image image)
    {
        if (_states.SfxIsActive)
        {
            image.sprite = icons[0];
            _states.SfxIsActive = false;
        }
        else
        {
            image.sprite = icons[1];
            _states.SfxIsActive = true;
        }
    }
}