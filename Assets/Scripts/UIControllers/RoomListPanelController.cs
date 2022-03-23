using System.Collections.Generic;
using System.Threading;
using Photon.Realtime;
using UIFrame;
using UnityEngine;

public class RoomListPanelController : UIControllerBase
{
    //房间信息
    public List<RoomInfo> roomInfos;
    //所有的房间
    private Dictionary<string, RoomBehaviour> rooms;
    protected override void ControllerStart()
    {
        base.ControllerStart();
        rooms = new Dictionary<string, RoomBehaviour>();
        roomInfos = new List<RoomInfo>();
    }

    public void UpdateRoomList(List<RoomInfo> roomInfos)
    {
        //this.roomInfos = roomInfos;
        
        //动态管理
        foreach (var item in roomInfos)
        {
            //如果变化的原因是，有一个房间没人了
            if (item.RemovedFromList || item.PlayerCount == 0)
            {
                this.roomInfos.Remove(item);
                Debug.Log("-");
            }
            //如果变化的原因是，有新房间创建
            else
            {
                this.roomInfos.Add(item);
                Debug.Log("+");
            }
        }
        Debug.Log(this.roomInfos.Count);
        UpdateRoomListUI();
    }

    private void ClearRoomListUI()
    {
        foreach (var item in rooms)
        {
            GameObject.Destroy(item.Value.gameObject);
        }

        rooms.Clear();
    }

    private void ShowRoomListUI()
    {
        Transform parent = crtModule.FindCurrentModuleWidget("RoomList_F").transform;

        foreach (var item in roomInfos)
        {
            if (!item.IsOpen)
            {
                continue;
            }
            if (!item.IsVisible)
            {
                continue;
            }

            if (item.MaxPlayers == 0)
            {
                continue;
            }
            
            //生成一个房间
            GameObject tempRoom = UIManager.Instance.CreateDynamicWidget("RoomInfo", parent, false);
            tempRoom.GetComponent<RoomBehaviour>().RoomInit(item);
            //添加到字典
            rooms.Add(item.Name,tempRoom.GetComponent<RoomBehaviour>());
        }
    }

    public void UpdateRoomListUI()
    {
        ClearRoomListUI();
        ShowRoomListUI();
    }
}
