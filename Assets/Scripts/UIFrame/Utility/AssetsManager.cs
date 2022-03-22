using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

namespace UIFrame
{
    public class AssetsManager : Singleton<AssetsManager>
    {
        //私有构造
        private AssetsManager()
        {

        }

        private Dictionary<string, Object> assetsCache = new Dictionary<string, Object>();

        [Obsolete("过时方法,存在新方法GetAsset")]
        public Object OldGetAsset(string path)
        {
            Object assetObj = null;
            if (!assetsCache.ContainsKey(path))
            {
                //通过Resources加载资源
                assetObj = Resources.Load(path);
                //将资源存入缓存
                assetsCache.Add(path, assetObj);
            }
            else
            {
                //从缓存中获取
                assetObj = assetsCache[path];
            }
            return assetObj;
        }

        public Object GetAsset(string path)
        {
            Object assetObj = null;
            //尝试从字典中获取该路径所对应的资源
            if (!assetsCache.TryGetValue(path, out assetObj))
            {
                //通过Resources加载资源
                assetObj = Resources.Load(path);
                //将资源存入缓存
                assetsCache.Add(path, assetObj);
            }
            
            return assetObj;
        }
    }
}