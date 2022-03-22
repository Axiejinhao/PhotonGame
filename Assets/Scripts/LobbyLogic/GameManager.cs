using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using UIFrame;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    public string infoMessage;
    
    private void Awake()
    {
        Instance = this;
        //当前对象在过渡场景时不用销毁
        DontDestroyOnLoad(gameObject);
        //支持后台运行
        Application.runInBackground = true;
        //支持网络场景同步
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        //连接Photon云服务器
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        MainPanelModule mainPanelModule = UIManager.Instance.GetUIModuleByName("MainPanel") as MainPanelModule;
        mainPanelModule.ResumePanelInteractable();
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("创建房间成功");
        UIManager.Instance.PushUI("RoomPanel");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        infoMessage = message;
        //显示提示框
        UIManager.Instance.PushUI("InfoPanel");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        UIManager.Instance.PushUI("RoomPanel");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        
        infoMessage = message;
        //显示提示框
        UIManager.Instance.PushUI("InfoPanel");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        //进入大厅Panel
        UIManager.Instance.PushUI("RoomListPanel");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        //获取房间模块
        RoomPanelModule roomPanel = UIManager.Instance.GetUIModuleByName("RoomPanel") as RoomPanelModule;
        //更新玩家列表
        roomPanel.UpdatePlayerUIMsg();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        //获取房间模块
        RoomPanelModule roomPanel = UIManager.Instance.GetUIModuleByName("RoomPanel") as RoomPanelModule;
        //更新玩家列表
        roomPanel.UpdatePlayerUIMsg();
    }

    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(target, changedProps);
        object res = null;
        changedProps.TryGetValue(GameConst.READY_PROPERTY, out res);
        if (res == null)
        {
            res = false;
        }
        //获取房间模块
        RoomPanelModule roomPanel = UIManager.Instance.GetUIModuleByName("RoomPanel") as RoomPanelModule;
        //调用房间模块方法
        roomPanel.SetPlayerReadyState(target.ActorNumber, (bool)res );
    }
}
