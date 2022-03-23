using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UIFrame;

public class RoomListPanelModule : UIModuleBase
{
    private RoomListPanelController controller;
    public override void Awake()
    {
        base.Awake();
        controller = new RoomListPanelController();
        BindController(controller);
    }

    /// <summary>
    /// 设置房间信息列表
    /// </summary>
    /// <param name="roomInfos"></param>
    public void SetRoomInfos(List<RoomInfo> roomInfos)
    {
        controller.UpdateRoomList(roomInfos);
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        _canvasGroup.alpha = 1;
    }

    public override void OnPause()
    {
        base.OnPause();
        _canvasGroup.alpha = 0;

    }

    public override void OnResume()
    {
        base.OnResume();
        _canvasGroup.alpha = 1;
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        _canvasGroup.alpha = 0;

    }
}
