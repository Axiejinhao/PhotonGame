using UIFrame;

public class InfoPanelModule : UIModuleBase
{
    private InfoPanelControllerL controller;
    public override void Awake()
    {
        base.Awake();
        controller = new InfoPanelControllerL();
        BindController(controller);
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        _canvasGroup.alpha = 1;
        controller.SetInfoText(LobbyManager.Instance.infoMessage);
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

    }

    public override void OnExit()
    {
        base.OnExit();
        _canvasGroup.alpha = 0;

    }
}