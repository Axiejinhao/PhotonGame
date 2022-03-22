using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UIFrame;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerBehaviour : MonoBehaviour
{
    private Player _player;

    #region UI Field

    private Image color; 
    private InputField playerName;
    private Button readyButton;
    private Button notReadyButton;
    //标记玩家是否准备
    private GameObject hasReadySign;

    #endregion

    private void Awake()
    {
        color = transform.Find("Color").GetComponent<Image>();
        playerName = transform.Find("PlayerName").GetComponent<InputField>();
        readyButton = transform.Find("ReadyButton").GetComponent<Button>();
        notReadyButton = transform.Find("NotReadyButton").GetComponent<Button>();
        hasReadySign = readyButton.transform.Find("IsReadySign").gameObject;
        //绑定点击事件
        readyButton.onClick.AddListener(OnReadyButtonClick);
    }

    /// <summary>
    /// 玩家初始化
    /// </summary>
    /// <param name="player"></param>
    public void PlayerInit(Player player)
    {
        _player = player;
        playerName.text = player.NickName;
        color.color = GameConst.PLAYER_COLORS[(player.ActorNumber-1) % GameConst.PLAYER_COLORS.Length];
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

    /// <summary>
    /// Button点击事件
    /// </summary>
    private void OnReadyButtonClick()
    {
        if (!_player.IsLocal)
        {
            return;
        }
        bool crtReadyState = GetPlayerReadyState();
        //创建Hashtable
        Hashtable hashtable = new Hashtable();
        //添加属性
        hashtable.Add(GameConst.READY_PROPERTY,!crtReadyState);
        _player.SetCustomProperties(hashtable);
        //触发自己的UI状态
        SetReadyStateUI(!crtReadyState);
    }

    /// <summary>
    /// 获取玩家的准备状态
    /// </summary>
    /// <returns></returns>
    private bool GetPlayerReadyState()
    {
        object res = null;
        _player.CustomProperties.TryGetValue(GameConst.READY_PROPERTY, out res);
        if (res == null)
        {
            return false;
        }
        else
        {
            return (bool) res;
        }
    }

    /// <summary>
    /// 设置当前玩家的准备状态UI
    /// </summary>
    /// <param name="isReady"></param>
    public void SetReadyStateUI(bool isReady)
    {
        hasReadySign.SetActive(isReady);
        if (!_player.IsLocal)
        {
            readyButton.gameObject.SetActive(isReady);
            readyButton.interactable = false;
            notReadyButton.gameObject.SetActive(!isReady);
        }
    }
}

