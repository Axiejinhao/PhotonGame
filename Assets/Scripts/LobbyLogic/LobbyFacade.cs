using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;

public class LobbyFacade : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.ShowUI("TopPanel");
        UIManager.Instance.ShowUI("MainPanel");
    }
}
