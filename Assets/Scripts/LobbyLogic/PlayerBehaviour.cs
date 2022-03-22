using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UIFrame;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    private Player _player;

    #region UI Field

    private Image color; 
    private InputField playerName;
    private Button readyButton;
    private Button notReadyButton;

    #endregion

    private void Awake()
    {
        color = transform.Find("Color").GetComponent<Image>();
        playerName = transform.Find("PlayerName").GetComponent<InputField>();
        readyButton = transform.Find("ReadyButton").GetComponent<Button>();
        notReadyButton = transform.Find("NotReadyButton").GetComponent<Button>();
    }

    /// <summary>
    /// 玩家初始化
    /// </summary>
    /// <param name="player"></param>
    public void PlayerInit(Player player)
    {
        _player = player;
        playerName.text = player.NickName;
        color.color = SystemDefine.PLAYER_COLORS[player.ActorNumber % SystemDefine.PLAYER_COLORS.Length];
        if (player.IsLocal)
        {
            readyButton.gameObject.SetActive(true);
            notReadyButton.gameObject.SetActive(false);
        }
        else
        {
            readyButton.gameObject.SetActive(false);
            notReadyButton.gameObject.SetActive(true);
        }
    }
}

