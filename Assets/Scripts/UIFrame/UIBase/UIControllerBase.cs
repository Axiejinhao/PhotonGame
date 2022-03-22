using System;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;

namespace UIFrame
{
    /// <summary>
    /// UI中的业务逻辑 
    /// </summary>
    public class UIControllerBase
    {
        //当前所处的模块
        protected UIModuleBase crtModule;

        public void ControllerInit(UIModuleBase moduleBase)
        {
            crtModule = moduleBase;
            //启动
            ControllerStart();
        }

        protected virtual void ControllerStart()
        {

        }
    }
}
