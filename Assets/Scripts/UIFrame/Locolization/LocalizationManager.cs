using System;
using System.Collections.Generic;
using UIFrame;
using UnityEngine;

namespace Locolization
{
    public enum SupportLanguage
    {
        SimpleChinese,
        English,
        Janpanese
    }

    public class LocalizationManager : Singleton<LocalizationManager>
    {
        //存储所有委托对象
        private System.Action<int> localizationEventHandle;

        private LocalizationManager()
        {

        }

        public void LocalizationInit()
        {
            int id = PlayerPrefs.GetInt("LanguageID");
            ChangeLanguage((SupportLanguage)id);
        }

        /// <summary>
        /// 添加监听事件
        /// </summary>
        /// <param name="action"></param>
        public void AddLocalizationListener(System.Action<int> action)
        {
            localizationEventHandle += action;
        }

        /// <summary>
        /// 移除监听事件
        /// </summary>
        /// <param name="action"></param>
        public void RemoveLocalizationListener(System.Action<int> action)
        {
            localizationEventHandle -= action;
        }

        public void ChangeLanguage(SupportLanguage _supportLanguage)
        {
            localizationEventHandle((int)_supportLanguage);
            PlayerPrefs.SetInt("LanguageID", (int)_supportLanguage);

        }
    }
}
