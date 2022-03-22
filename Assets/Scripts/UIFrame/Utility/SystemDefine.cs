﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrame
{
    public static class SystemDefine
    {
        #region Configuration Path
        public const string PanelConfigPath = "Configuration/UIPanelConfig";
        public const string LocalizationConfigPath = "Configuration/UILanguageTextConfig";
        public const string WidgetConfigPath = "Configuration/UIWidgetConfig";
        #endregion

        #region Scene ID
        public enum SceneID
        {
            MainScene = 0,
            FightScene = 1
        }
        #endregion

        #region Game Tags
        public const string CANVAS = "Canvas";
        #endregion

        #region Widget Token
        public static string[] WIDGET_TOKEN = new string[] { "_F", "_S", "T" };
        #endregion

        #region PlayerColors

        public static Color[] PLAYER_COLORS;

        #endregion

        #region Static Constructor

        static SystemDefine()
        {
            PLAYER_COLORS = new Color[]
            {
                Color.red,
                Color.blue, 
                Color.gray,
                Color.black, 
                Color.green, 
                Color.magenta,
                Color.yellow,
                Color.cyan
            };
        }

        #endregion
    }
}
