namespace UIFrame
{
    [System.Serializable]
    public class JsonWidgetsModel
    {
        public SceneWidgetDataModel[] AllData;
    }

    [System.Serializable]
    public class SceneWidgetDataModel
    {
        public string SceneName;
        public WidgetDataModel[] Data;
    }

    [System.Serializable]
    public class WidgetDataModel
    {
        public string WidgetName;
        public string WidgetPath;
    }
}