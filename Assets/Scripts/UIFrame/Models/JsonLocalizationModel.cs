using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFrame
{
    [System.Serializable]
    public class JsonLocalizationModel
    {
        public LocalizationSceneDataModel[] AllData;
    }

    [System.Serializable]
    public class LocalizationSceneDataModel
    {
        public string SceneName;
        public LocalizationDataModel[] Data;
    }

    [System.Serializable]
    public class LocalizationDataModel
    {
        public string TextObjName;
        public string[] TextLanguageText;
    }
}
