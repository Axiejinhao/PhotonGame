using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RoomBehaviour : MonoBehaviour
{
    private RoomInfo _roomInfo;

    #region UIField

    private Text roomNameText;
    private Text slotsText;
    private Button joinButton;

    #endregion

    private void Awake()
    {
        roomNameText = transform.GetChild(0).GetComponent<Text>();
        slotsText = transform.GetChild(1).GetComponent<Text>();
        joinButton = transform.GetChild(2).GetComponent<Button>();
    }

    public void RoomInit(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;
        roomNameText.text = roomInfo.Name;
        joinButton.onClick.AddListener(() =>
        {
            //离开大厅
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.JoinRoom(roomInfo.Name);
        });
    }

    private void Update()
    {
        slotsText.text = _roomInfo.PlayerCount + "/" + _roomInfo.MaxPlayers;
    }
}
