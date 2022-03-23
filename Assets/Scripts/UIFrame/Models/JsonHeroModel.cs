namespace UIFrame
{
    [System.Serializable]
    public class JsonHeroModel
    {
        public SceneHeroDataModel[] AllData;
    }

    [System.Serializable]
    public class SceneHeroDataModel
    {
        public string SceneName;
        public HeroDataModel[] Data;
    }

    [System.Serializable]
    public class HeroDataModel
    {
        public int HeroIndex;
        public string HeroPath;
    }
}