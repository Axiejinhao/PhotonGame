using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using UIFrame;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class GameController : MonoBehaviourPunCallbacks
{
    //是否创建过英雄
    public bool hasInit = false;
    private void Start()
    {
        //设置每秒发送包数
        PhotonNetwork.SendRate = 30;
        //设置玩家加载属性成功
        SetPlayerLoaded();
        Test();
    }
    
    private void Test()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        PhotonNetwork.InstantiateSceneObject(JsonDataManager.Instance.FindHeroPath(1),
            Vector3.back*2, Quaternion.identity);
    }
    
    private void InitHero()
    {
        //已经创建过英雄
        hasInit = true;
        GameObject heroObj = PhotonNetwork.Instantiate(
            JsonDataManager.Instance.FindHeroPath(
                HumanGameManager.Instance.selectedHeroIndex),
            new Vector3(0,0,PhotonNetwork.LocalPlayer.ActorNumber*2), Quaternion.identity);
    }
    
    /// <summary>
    /// 设置当前玩家已经加载场景成功
    /// </summary>
    private void SetPlayerLoaded()
    {
        Hashtable hashtable = new Hashtable();
        //添加属性
        hashtable.Add(GameConst.LOADED_PROPERTY,true);
        //设置属性
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
    }

    /// <summary>
    /// 检测所有玩家是否准备
    /// </summary>
    /// <returns></returns>
    private bool CheckAllPlayerLoaded()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return false;
        }

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            object isLoaded = false;
            PhotonNetwork.PlayerList[i].CustomProperties.TryGetValue(GameConst.LOADED_PROPERTY, out isLoaded);
            if (isLoaded == null || !(bool) isLoaded)
            {
                return false;
            }
        }

        Hashtable hashtable = new Hashtable();
        //添加房间属性
        hashtable.Add(GameConst.INITHERO_PROPERTY,true);
        //设置房间属性
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
        
        //所有玩家加载完毕
        return true;
    }

    #region Photon Callbacks

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
        object canInit = false;
        //尝试获取属性
        propertiesThatChanged.TryGetValue(GameConst.INITHERO_PROPERTY, out canInit);
        if (canInit == null)
        {
            canInit = false;
        }

        if ((bool) canInit && !hasInit)
        {
            //每个玩家生成各自的英雄
            InitHero();   
        }
    }

    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(target, changedProps);
        //房主检测是否所有玩家都加载成功
        CheckAllPlayerLoaded();
    }

    #endregion
}
