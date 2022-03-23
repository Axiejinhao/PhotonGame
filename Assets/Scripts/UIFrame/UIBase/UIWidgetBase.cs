using System;
using System.Collections;
using System.Collections.Generic;
using UIFrame;
using UnityEngine;

namespace UIFrame
{
    public class UIWidgetBase : UIMono
    {
        //当前元件所处的模块
        private UIModuleBase currentModule;
        //临时参数
        private ArrayList tempParamters;
        
            
        public void UIWidgetInit(UIModuleBase uiModuleBase)
        {
            //设置当前元件所属的模块
            currentModule = uiModuleBase;
            //Debug.Log(currentModule.name + "|" + this.name);
            //将当前元件添加到UIManager的uiWidgets字典中
            UIManager.Instance.AddUIWidget(currentModule.name, this.name, this);
        }

        protected virtual void OnDestroy()
        {
            //将当前元件从UIManager的uiWidgets字典中删除
            UIManager.Instance.RemoveUIWidget(currentModule.name, this.name);
        }
    }
}
