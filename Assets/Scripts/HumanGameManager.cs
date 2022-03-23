using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HumanGameManager : MonoBehaviour {
    public static HumanGameManager Instance;

    private void Awake()
    {
        Instance = this;
        //当前对象在过渡场景时不用销毁
        DontDestroyOnLoad(gameObject);
    }
}
