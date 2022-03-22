using UnityEngine;
using System.Reflection;
using System;

namespace UIFrame
{
    public class Singleton<T> where T : class
    {
        //单例对象
        private static T _singleton;

        public static T Instance
        {
            get
            {
                if (_singleton == null)
                {
                    //通过反射实例化对象
                    //派生单例类必须要有一个私有的无参的构造
                    _singleton = (T)Activator.CreateInstance(typeof(T), true);
                }
                return _singleton;
            }
        }
    }
}
