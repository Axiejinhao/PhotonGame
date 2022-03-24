using UnityEngine;

public class HumanGameManager : MonoBehaviour {
    public static HumanGameManager Instance;
    //英雄编号
    public int selectedHeroIndex = 1;
    //当前英雄
    public Transform currentHero;
    private void Awake()
    {
        Instance = this;
        //当前对象在过渡场景时不用销毁
        DontDestroyOnLoad(gameObject);
    }
}
