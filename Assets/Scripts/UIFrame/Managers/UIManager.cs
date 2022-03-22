using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;

namespace UIFrame
{
    public class UIManager : Singleton<UIManager>
    {
        //管理当前场景的所有UI模块
        private Dictionary<UIType, UIModuleBase> uiModules;
        //UI模块的栈存储
        private Stack<UIModuleBase> uiModuleStack;
        //UI模块的列表存储
        private List<UIModuleBase> uiModuleList;
        //管理当前场景的所有UI元件
        private Dictionary<string, Dictionary<string, UIWidgetBase>> uiWidgets;
        private Transform _canvas;

        private UIManager()
        {
            uiModules = new Dictionary<UIType, UIModuleBase>();
            uiModuleStack = new Stack<UIModuleBase>();
            uiModuleList = new List<UIModuleBase>();
            uiWidgets = new Dictionary<string, Dictionary<string, UIWidgetBase>>();
            _canvas = GameObject.FindWithTag(SystemDefine.CANVAS).transform;
        }

        #region UI Module GameObject
        
        /// <summary>
        /// 通过名字获取模块
        /// </summary>
        /// <param name="uiPanelName"></param>
        /// <returns></returns>
        public UIModuleBase GetUIModuleByName(string uiPanelName)
        {
            //获取UIType
            UIType _uiType = UITypeManager.Instance.GetUIType(uiPanelName);
            //获取UIModuleBase
            return GetUIModule(_uiType);
        }
        
        /// <summary>
        /// 通过UIType获取Type对应的游戏模块对象身上的UIModuleBase
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        private UIModuleBase GetUIModule(UIType uiType)
        {
            UIModuleBase crtModule = null;
            //字典中没有该模块
            if (!uiModules.TryGetValue(uiType, out crtModule))
            {
                //生成该模块
                crtModule = InstantiateUIModule(AssetsManager.Instance.GetAsset(uiType.Path) as GameObject);
                uiModules.Add(uiType, crtModule);
            }
            else if (crtModule == null)
            {
                //生成该模块
                crtModule = InstantiateUIModule(AssetsManager.Instance.GetAsset(uiType.Path) as GameObject);
                uiModules[uiType] = crtModule;
            }
            return crtModule;
        }

        private UIModuleBase InstantiateUIModule(GameObject prefab)
        {
            //生成模块对象
            GameObject crtModuleObj = GameObject.Instantiate(prefab);
            //设置父对象为画布
            crtModuleObj.transform.SetParent(_canvas, false);
            //返回组件
            return crtModuleObj.GetComponent<UIModuleBase>();
        }

        #endregion

        #region UI Module Stack

        /// <summary>
        /// 通过PanelName获取模块对象,并压栈
        /// </summary>
        /// <param name="uiPanelName"></param>
        public void PushUI(string uiPanelName)
        {
            //获取UIType
            UIType _uiType = UITypeManager.Instance.GetUIType(uiPanelName);
            //获取UIModuleBase
            UIModuleBase crtModuleBase = GetUIModule(_uiType);
            
            if (uiModuleStack.Count != 0)
            {
                //栈顶窗口进入暂停状态
                uiModuleStack.Peek().OnPause();
            }
            //新窗口压栈
            uiModuleStack.Push(crtModuleBase);
            //新窗口执行Enter
            crtModuleBase.OnEnter();
        }

        /// <summary>
        /// 栈顶元素出栈
        /// </summary>
        public void PopUI()
        {
            if (uiModuleStack.Count > 0)
            {
                //栈顶元素出栈
                uiModuleStack.Pop().OnExit();
            }
            if (uiModuleStack.Count != 0)
            {
                //新栈顶元素恢复
                uiModuleStack.Peek().OnResume();
            }
        }

        #endregion

        #region UI Module List

        public void ShowUI(string uiPanelName)
        {
            //获取UIType
            UIType _uiType = UITypeManager.Instance.GetUIType(uiPanelName);
            //获取UIModuleBase
            UIModuleBase crtModuleBase = GetUIModule(_uiType);
            if (!uiModuleList.Contains(crtModuleBase))
            {
                uiModuleList.Add(crtModuleBase);
            }
            //新窗口执行Enter
            crtModuleBase.OnEnter();
        }

        #endregion

        #region UI uiWidgets Widgets->Module (Un)Register

        /// <summary>
        /// 注册UI模块
        /// </summary>
        /// <param name="moduleName"></param>
        private void RegisterUIModuleToUIWidgets(string moduleName)
        {
            if (!uiWidgets.ContainsKey(moduleName))
            {
                //向字典添加元素
                uiWidgets.Add(moduleName, new Dictionary<string, UIWidgetBase>());
            }
        }

        /// <summary>
        /// 取消注册UI模块
        /// </summary>
        /// <param name="moduleName"></param>
        private void UnRegisterUIModuleFromUIWidgets(string moduleName)
        {
            if (uiWidgets.ContainsKey(moduleName))
            {
                //向字典添加元素
                uiWidgets.Remove(moduleName);
            }
        }


        #endregion

        #region UI Widgets Add/Remove

        /// <summary>
        /// 添加元件
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="widgetName">元件名称</param>
        /// <param name="uiWidgetBase">元件对象</param>
        public void AddUIWidget(string moduleName, string widgetName, UIWidgetBase uiWidget)
        {
            //如果模块不存在,添加模块
            RegisterUIModuleToUIWidgets(moduleName);
            if (!uiWidgets[moduleName].ContainsKey(widgetName))
            {
                uiWidgets[moduleName].Add(widgetName, uiWidget);
            }
        }

        /// <summary>
        /// 删除元件
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="widgetName">元件名称</param>
        public void RemoveUIWidget(string moduleName, string widgetName)
        {
            if (uiWidgets[moduleName].ContainsKey(widgetName))
            {
                uiWidgets[moduleName].Remove(widgetName);
            }
        }

        #endregion

        #region Find Widget

        /// <summary>
        /// 获取某个模块的某个元件
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="widgetName"></param>
        public UIWidgetBase FindWidget(string moduleName, string widgetName)
        {
            RegisterUIModuleToUIWidgets(moduleName);

            UIWidgetBase uiWidget = null;
            //尝试获取元件
            uiWidgets[moduleName].TryGetValue(widgetName, out uiWidget);
            return uiWidget;
        }

        #endregion

        #region Dynamic Widget Instantiate
        
        /// <summary>
        /// 根据动态元件名称创建元件
        /// </summary>
        /// <param name="widgetName"></param>
        /// <returns></returns>
        public GameObject CreateDynamicWidget(string widgetName)
        {
            //获取元件的资源路径
            string widgetPath = JsonDataManager.Instance.FindWidgetPath(widgetName);
            //获取预设体
            GameObject prefab = AssetsManager.Instance.GetAsset(widgetPath) as GameObject;
            return GameObject.Instantiate(prefab);
        }
        
        /// <summary>
        /// 根据动态元件名称创建元件,并设置父物体
        /// </summary>
        /// <param name="widgetName"></param>
        /// <returns></returns>
        public GameObject CreateDynamicWidget(string widgetName, Transform parent, bool worldPosStays)
        {
            GameObject gameObject = CreateDynamicWidget(widgetName);
            gameObject.transform.SetParent(parent,worldPosStays);
            return gameObject;
        }

        #endregion
    }
}
