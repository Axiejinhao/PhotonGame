using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MonoHelper : MonoBehaviour
{
    public static MonoHelper Instance;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 不间断执行函数
    /// </summary>
    /// <param name="method"></param>
    /// <param name="interval"></param>
    /// <param name="endCondition"></param>
    public void InvokeRepeat(Action method, float interval, Func<bool> endCondition)
    {
        StartCoroutine(ReaptCRT(method, interval, endCondition));
    }

    IEnumerator ReaptCRT(Action method, float interval, Func<bool> endCondition)
    {
        while (true)
        {
            if (interval <= 0)
            {
                yield return 0;
            }
            else
            {
                yield return new WaitForSeconds(interval);
            }

            method();

            if (endCondition())
            {
                yield break;
            }
        }
    }
    
}