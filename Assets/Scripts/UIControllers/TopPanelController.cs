using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UIFrame;

public class TopPanelController : UIControllerBase {
    protected override void ControllerStart()
    {
        base.ControllerStart();
        Debug.Log("TopPanel Start");
        BindEvent();
    }

    private void BindEvent()
    {
        crtModule.FindCurrentModuleWidget("BackButton_F").AddOnClickListener(() =>
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
            }

            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
            }
            
            UIManager.Instance.PopUI();
            UIManager.Instance.PopUI();
        });
        
        MonoHelper.Instance.InvokeRepeat(() =>
        {
            crtModule.FindCurrentModuleWidget("StatusInfo_F").SetTextText(
                "STATUS:"+PhotonNetwork.NetworkClientState);
        },0, () => { return false;});
    }
}
