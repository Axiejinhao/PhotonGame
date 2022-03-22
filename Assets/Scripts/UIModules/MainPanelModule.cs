using System.Collections;
using System.Collections.Generic;
using UIFrame;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainPanelModule : UIModuleBase {
    public override void Awake()
    {
        base.Awake();
        var controller = new MainPanelController();
        BindController(controller);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _canvasGroup.interactable = false;
    }

    public void ResumePanelInteractable()
    {
        _canvasGroup.interactable = true;
    }
}
