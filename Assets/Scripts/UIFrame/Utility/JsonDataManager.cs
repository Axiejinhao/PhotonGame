using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrame;

namespace UIFrame
{
    public class JsonDataManager : Singleton<JsonDataManager>
    {
        private JsonDataManager()
        {
            panelData = new JsonPanelsModel();
            panelDataDic = new Dictionary<int, Dictionary<string, string>>();
            localizationDic = new Dictionary<int, Dictionary<string, string[]>>();
            widgetDataDic = new Dictionary<int, Dictionary<string, string>>();
            ParsePanelData();
            ParseLocalizationData();
            ParseWidgetData();
        }

        #region Saved Structure

        //Panel解析后的数据
        private JsonPanelsModel panelData;
        //Panel解析后的数据(字典版)
        private Dictionary<int, Dictionary<string, string>> panelDataDic;

        //本地化内容解析后的数据
        private JsonLocalizationModel localizationData;
        //本地化内容解析后的数据(字典版)
        private Dictionary<int, Dictionary<string, string[]>> localizationDic;
        
        //Widget解析后的数据
        private JsonWidgetsModel widgetDate;
        //Widget解析后的数据(字典版)
        private Dictionary<int, Dictionary<string, string>> widgetDataDic;
        
        #endregion

        #region Json Parse

        /// <summary>
        /// 解析动态元件数据
        /// </summary>
        private void ParseWidgetData()
        {
            //获取配置文本的资源
            TextAsset widgetConfig = AssetsManager.Instance.GetAsset(SystemDefine.WidgetConfigPath) as TextAsset;
            //将Panel的配置文件进行解析
            widgetDate = JsonUtility.FromJson<JsonWidgetsModel>(widgetConfig.text);

            //将widgetData转化为字典型
            for (int i = 0; i < widgetDate.AllData.Length; i++)
            {
                //创建一个字典
                Dictionary<string, string> crtDic = new Dictionary<string, string>();
                //给新建的字典赋值
                for (int j = 0; j < widgetDate.AllData[i].Data.Length; j++)
                {
                    crtDic.Add(widgetDate.AllData[i].Data[j].WidgetName,
                        widgetDate.AllData[i].Data[j].WidgetPath);
                }
                //添加一个场景ID和一个字典
                widgetDataDic.Add(i, crtDic);
            }
        }
        
        /// <summary>
        /// Json解析Panel
        /// </summary>
        private void ParsePanelData()
        {
            //获取配置文本的资源
            TextAsset panelConfig = AssetsManager.Instance.GetAsset(SystemDefine.PanelConfigPath) as TextAsset;
            //将Panel的配置文件进行解析
            panelData = JsonUtility.FromJson<JsonPanelsModel>(panelConfig.text);

            //将panelData转化为字典型
            for (int i = 0; i < panelData.AllData.Length; i++)
            {
                //创建一个字典
                Dictionary<string, string> crtDic = new Dictionary<string, string>();
                //给新建的字典赋值
                for (int j = 0; j < panelData.AllData[i].Data.Length; j++)
                {
                    crtDic.Add(panelData.AllData[i].Data[j].PanelName,
                        panelData.AllData[i].Data[j].PanelPath);
                }
                //添加一个场景ID和一个字典
                panelDataDic.Add(i, crtDic);
            }
        }

        /// <summary>
        /// Json解析Localizatione本地化配置文件
        /// </summary>
        private void ParseLocalizationData()
        {
            //获取配置文本的资源
            TextAsset localizationConfig = AssetsManager.Instance.GetAsset(SystemDefine.LocalizationConfigPath) as TextAsset;
            //将Localization的配置文件进行解析
            localizationData = JsonUtility.FromJson<JsonLocalizationModel>(localizationConfig.text);

            //将Localization转化为字典型
            for (int i = 0; i < localizationData.AllData.Length; i++)
            {
                //创建一个字典
                Dictionary<string, string[]> crtDic = new Dictionary<string, string[]>();
                //给新建的字典赋值
                for (int j = 0; j < localizationData.AllData[i].Data.Length; j++)
                {
                    crtDic.Add(localizationData.AllData[i].Data[j].TextObjName,
                        localizationData.AllData[i].Data[j].TextLanguageText);
                }
                //添加一个场景ID和一个字典
                localizationDic.Add(i, crtDic);
            }
        }

        #endregion

        #region Data Find

        /// <summary>
        /// 通过Panel资源名称返回Panel资源路径
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public string FindPanelPath(string panelName, int sceneID = (int)SystemDefine.SceneID.MainScene)
        {
            if (!panelDataDic.ContainsKey(sceneID))
            {
                return null;
            }
            if (!panelDataDic[sceneID].ContainsKey(panelName))
            {
                return null;
            }
            //如果ID和资源名称都存在
            return panelDataDic[sceneID][panelName];
        }

        /// <summary>
        /// 通过文本对象名称返回本地化数据
        /// </summary>
        /// <param name="textObjName"></param>
        /// <param name="sceneID"></param>
        /// <returns></returns>
        public string[] FindTextLocalization(string textObjName, int sceneID = (int)SystemDefine.SceneID.MainScene)
        {
            if (!localizationDic.ContainsKey(sceneID))
            {
                return null;
            }
            if (!localizationDic[sceneID].ContainsKey(textObjName))
            {
                return null;
            }
            //如果ID和资源名称都存在
            return localizationDic[sceneID][textObjName];
        }

        /// <summary>
        /// 通过Widget资源名称返回Widget资源路径
        /// </summary>
        /// <param name="widgetName"></param>
        /// <returns></returns>
        public string FindWidgetPath(string widgetName, int sceneID = (int)SystemDefine.SceneID.MainScene)
        {
            if (!widgetDataDic.ContainsKey(sceneID))
            {
                return null;
            }
            if (!widgetDataDic[sceneID].ContainsKey(widgetName))
            {
                return null;
            }
            //如果ID和资源名称都存在
            return widgetDataDic[sceneID][widgetName];
        }
        
        #endregion
    }
}
