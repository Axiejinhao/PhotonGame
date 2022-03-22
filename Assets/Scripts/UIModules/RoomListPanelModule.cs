using UIFrame;

public class RoomListPanelModule : UIModuleBase
{
    public override void Awake()
    {
        base.Awake();
        var controller = new RoomListPanelController();
        BindController(controller);
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

    }

    public override void OnExit()
    {
        base.OnExit();
        _canvasGroup.alpha = 0;

    }
}
