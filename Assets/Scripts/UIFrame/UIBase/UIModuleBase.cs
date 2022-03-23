using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;

namespace UIFrame
{
    /// <summary>
    /// 挂载在每一个Panel模块上
    /// </summary>
    /// 
    [RequireComponent(typeof(CanvasGroup))]
    public class UIModuleBase : MonoBehaviour
    {
        protected CanvasGroup _canvasGroup;
        //当前模块的所有子对象
        private Transform[] allChild;

        public virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            //修改模块的名称
            gameObject.name = gameObject.name.Remove(gameObject.name.Length - "(Clone)".Length);
            //获得所有子对象
            allChild = GetComponentsInChildren<Transform>();
            //给所有可用的UI元件添加行为
            AddWidgetBehaviour();
        }

        #region Controller Bind

        protected void BindController(UIControllerBase controllerBase)
        {
            controllerBase.ControllerInit(this);
        }

        #endregion

        #region Set Widgets

        /// <summary>
        /// 给所有满足条件的子对象添加UIWidgetBase组件
        /// </summary>
        private void AddWidgetBehaviour()
        {
            //遍历所有子对象
            for (int i = 0; i < allChild.Length; i++)
            {
                //遍历所有标记Token
                for (int j = 0; j < SystemDefine.WIDGET_TOKEN.Length; j++)
                {
                    //判断当前元件对象是否以该标记为名称结尾
                    if (allChild[i].name.EndsWith(SystemDefine.WIDGET_TOKEN[j]))
                    {
                        AddComponentForWidget(i);
                    }
                }
            }
        }

        /// <summary>
        /// 给元件添加UIWidgetBase组件
        /// </summary>
        /// <param name="index"></param>
        protected virtual void AddComponentForWidget(int index)
        {
            UIWidgetBase uiWidgetBase =
                allChild[index].gameObject.AddComponent<UIWidgetBase>();
            //设置当前元件的模块是this
            uiWidgetBase.UIWidgetInit(this);
        }

        #endregion

        #region OnState

        /// <summary>
        /// 进入该模块是执行函数
        /// </summary>
        public virtual void OnEnter()
        {
            _canvasGroup.blocksRaycasts = true;
            transform.SetSiblingIndex(transform.parent.childCount-1);
        }

        /// <summary>
        /// 离开该模块是执行函数
        /// </summary>
        public virtual void OnExit()
        {
            _canvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        ///  暂离该模块是执行函数
        /// </summary>
        public virtual void OnPause()
        {
            _canvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// 恢复该模块是执行函数
        /// </summary>
        public virtual void OnResume()
        {
            _canvasGroup.blocksRaycasts = true;
        }

        #endregion

        #region Find Widget

        public UIWidgetBase FindCurrentModuleWidget(string widgetName)
        {
            return UIManager.Instance.FindWidget(this.name, widgetName);
        }

        #endregion
    }
}
