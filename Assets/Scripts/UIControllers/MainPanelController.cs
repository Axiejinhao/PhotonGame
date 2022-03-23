using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UIFrame;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MainPanelController : UIControllerBase
{
    protected override void ControllerStart()
    {
        base.ControllerStart();
        Debug.Log("MainPanel Start");
        BaseMsgInit();
        BindEvent();
    }

    private void BaseMsgInit()
    {
        //设置随机玩家昵称
        crtModule.FindCurrentModuleWidget("PlayerNameInputField_F")
            .SetInputFieldText("Player" + Random.Range(100, 1000));
        //设置随机房间名称
        crtModule.FindCurrentModuleWidget("RoomNameInputField_F").SetInputFieldText("Room" + Random.Range(10, 100));
        //设置默认的最大人数
        crtModule.FindCurrentModuleWidget("RoomMaxCountInputField_F").SetInputFieldText("5");
    }

    private void BindEvent()
    {
        //Hero按钮点击事件绑定
        //设置选择框到当前模块
        crtModule.FindCurrentModuleWidget("Hero01Button_F").AddOnClickListener(() =>
        {
            SelectClickEvent("Hero01Button_F");
            //标记当前英雄选择的编号
            HumanGameManager.Instance.selectedHeroIndex = 1;
        });

        crtModule.FindCurrentModuleWidget("Hero02Button_F").AddOnClickListener(() =>
        {
            SelectClickEvent("Hero02Button_F");
            //标记当前英雄选择的编号
            HumanGameManager.Instance.selectedHeroIndex = 2;
        });

        crtModule.FindCurrentModuleWidget("Hero03Button_F").AddOnClickListener(() =>
        {
            SelectClickEvent("Hero03Button_F");
            //标记当前英雄选择的编号
            HumanGameManager.Instance.selectedHeroIndex = 3;
        });

        //创建房间按钮事件
        crtModule.FindCurrentModuleWidget("CreateButton_F").AddOnClickListener(CreateRoomBtnClick);

        //创建随机加入房间按钮事件
        crtModule.FindCurrentModuleWidget("RandomRoomButton_F").AddOnClickListener(JoinRandomRoomBtnClick);

        //创建房间大厅按钮事件
        crtModule.FindCurrentModuleWidget("ListServerButton_F").AddOnClickListener(JoinLobbyBtnClick);
    }

    private void CreateRoomBtnClick()
    {
        //已经加入房间后,不能再创建房间了
        if (PhotonNetwork.NetworkClientState == ClientState.Joined)
        {
            return;
        }
        
        string playerName = crtModule.FindCurrentModuleWidget("PlayerNameInputField_F").GetInputFieldText();
        string roomPwd = crtModule.FindCurrentModuleWidget("RoomPasswordInputField_F").GetInputFieldText();
        string roomName = crtModule.FindCurrentModuleWidget("RoomNameInputField_F").GetInputFieldText();
        string roomMaxCount = crtModule.FindCurrentModuleWidget("RoomMaxCountInputField_F").GetInputFieldText();

        if (playerName == "")
        {
            playerName = "Player" + Random.Range(100, 1000);
        }
        if (roomName == "")
        {
            roomName = "Room" + Random.Range(10, 100);
        }
        if (roomMaxCount == "")
        {
            roomMaxCount = "5";
        }
        
        //设置玩家昵称
        PhotonNetwork.NickName = playerName;
        RoomOptions roomOptions = new RoomOptions();
        //设置最大人数
        roomOptions.MaxPlayers = byte.Parse(roomMaxCount);
        if (roomPwd != "")
        {
            if (roomOptions.CustomRoomProperties == null)
            {
                roomOptions.CustomRoomProperties = new Hashtable();
            }
            //添加房间密码
            roomOptions.CustomRoomProperties.Add("Password",roomPwd);
        }
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    private void JoinRandomRoomBtnClick()
    {
        //已经加入房间后,不能再创建房间了
        if (PhotonNetwork.NetworkClientState == ClientState.Joined)
        {
            return;
        }
        
        string playerName = crtModule.FindCurrentModuleWidget("PlayerNameInputField_F").GetInputFieldText();
        if (playerName == "")
        {
            playerName = "Player" + Random.Range(100, 1000);
        }

        PhotonNetwork.NickName = playerName;

        PhotonNetwork.JoinRandomRoom();
    }

    private void JoinLobbyBtnClick()
    {
        string playerName = crtModule.FindCurrentModuleWidget("PlayerNameInputField_F").GetInputFieldText();
        if (playerName == "")
        {
            playerName = "Player" + Random.Range(100, 1000);
        }
        PhotonNetwork.NickName = playerName;
        
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    private void SelectClickEvent(string name)
    {
        Transform parent = crtModule.FindCurrentModuleWidget(name).transform;
        crtModule.FindCurrentModuleWidget("Select_F").SetParent(parent, false);
    }
}