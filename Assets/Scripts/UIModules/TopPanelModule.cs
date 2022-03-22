using System.Collections;
using System.Collections.Generic;
using UIFrame;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TopPanelModule : UIModuleBase {
    public override void Awake()
    {
        base.Awake();
        //创建控制器
        var controller = new TopPanelController();
        //绑定控制器
        BindController(controller);
    }
}
