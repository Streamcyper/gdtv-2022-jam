using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [SerializeField] private UiManager uiManager;

    public int KarmaAmount { get; private set; } = 0;

    public void PickUpKarma()
    {
        KarmaAmount++;
        uiManager.UpdateKarma(KarmaAmount);
    }

    public void UseKarma(int amount)
    {
        KarmaAmount -= amount;
        uiManager.UpdateKarma(KarmaAmount);
    }
}