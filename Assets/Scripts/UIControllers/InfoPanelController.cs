using UIFrame;
using UnityEngine;

public class InfoPanelControllerL : UIControllerBase
{
    protected override void ControllerStart()
    {
        base.ControllerStart();
        crtModule.FindCurrentModuleWidget("CancelButton_F").AddOnClickListener(() =>
        {
            UIManager.Instance.PopUI();
        });
    }

    private void BindEvent()
    {
        
    }

    public void SetInfoText(string text)
    {
        crtModule.FindCurrentModuleWidget("Text_F").SetTextText(text);
    }
}
