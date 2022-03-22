using UIFrame;

public class RoomPanelModule : UIModuleBase
{
    private RoomPanelController controller;
    public override void Awake()
    {
        base.Awake();
        controller = new RoomPanelController();
        BindController(controller);
    }
    
    /// <summary>
    /// 更新玩家UI显示
    /// </summary>
    public void UpdatePlayerUIMsg()
    {
        controller.UpdatePlayerMsg();
    }
    
    /// <summary>
    /// 设置玩家准备状态
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="isReady"></param>
    public void SetPlayerReadyState(int playerID, bool isReady)
    {
        controller.SetPlayerReadyState(playerID,isReady);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _canvasGroup.alpha = 1;
        UpdatePlayerUIMsg();
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
        controller.ClearPlayerUIList();
    }
}