using System;
using System.Collections.Generic;
using UIFrame;

namespace UIFrame
{
    public class UITypeManager : Singleton<UITypeManager>
    {
        //UIType的缓存池
        private Dictionary<string, UIType> _uiTypes;

        private UITypeManager()
        {
            _uiTypes = new Dictionary<string, UIType>();
        }

        /// <summary>
        /// 通过UIPanelName名称,获取其路径
        /// </summary>
        public UIType GetUIType(string uiPanelName)
        {
            //返回的UIType
            UIType uiType = null;
            if (!_uiTypes.TryGetValue(uiPanelName, out uiType))
            {
                //新建一个UIType
                uiType = new UIType(JsonDataManager.Instance.FindPanelPath(uiPanelName));
                _uiTypes.Add(uiPanelName, uiType);
            }
            return uiType;
        }
    }
}
