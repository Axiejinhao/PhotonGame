using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UIFrame;
using UnityEngine;

public class RoomPanelController : UIControllerBase
{
    //当前房间所有玩家
    private Dictionary<int, GameObject> roomPlayers;
    protected override void ControllerStart()
    {
        base.ControllerStart();
        roomPlayers = new Dictionary<int, GameObject>();
        crtModule.FindCurrentModuleWidget("StartGameButton_F").AddOnClickListener(() =>
        {
            PhotonNetwork.LoadLevel("CompleteMainScene");
        });
    }

    /// <summary>
    /// 清空玩家列表
    /// </summary>
    public void ClearPlayerUIList()
    {
        foreach (var item in roomPlayers)
        {
            //销毁游戏对象
            GameObject.Destroy(item.Value);
        }
        roomPlayers.Clear();
    }

    private void GeneratePlayerUIList()
    {
        Transform parent = crtModule.FindCurrentModuleWidget("PlayerList_F").transform;
        
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            //生成一个游戏玩家
            GameObject tempPlayer = UIManager.Instance.CreateDynamicWidget("PlayerInfo",parent,false);
            tempPlayer.GetComponent<PlayerBehaviour>().PlayerInit(PhotonNetwork.PlayerList[i]);
            //添加到字典
            roomPlayers.Add(PhotonNetwork.PlayerList[i].ActorNumber,tempPlayer);
        }
    }
    
    /// <summary>
    /// 更新玩家信息
    /// </summary>
    public void UpdatePlayerMsg()
    {
        ClearPlayerUIList();
        GeneratePlayerUIList();
    }

    /// <summary>
    /// 设置玩家准备状态
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="isReady"></param>
    public void SetPlayerReadyState(int playerID, bool isReady)
    {
        if (!roomPlayers.ContainsKey(playerID))
        {
            return;
        }
        roomPlayers[playerID].GetComponent<PlayerBehaviour>().SetReadyStateUI(isReady);
    }

    public void SetPlayerReadyStateHash(int playerID, bool isReady)
    {
        if (!roomPlayers.ContainsKey(playerID))
        {
            return;
        }
        roomPlayers[playerID].GetComponent<PlayerBehaviour>().SetPlayerReadyState(isReady);
    }
    
    /// <summary>
    /// 显示游戏开始按钮
    /// </summary>
    public void ShowStartGameBtn()
    {
        //设置按钮是否显示
        crtModule.FindCurrentModuleWidget("StartGameButton_F").SetGameObjectActive(
            PhotonNetwork.IsMasterClient && AllPlayerHasReady());

    }

    private bool AllPlayerHasReady()
    {
        foreach (var item in roomPlayers)
        {
            if (!item.Value.GetComponent<PlayerBehaviour>().isReady)
            {
                return false;
            }
        }

        return true;
    }
}