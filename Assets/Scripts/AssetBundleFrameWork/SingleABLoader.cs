/*************************************************************************
 *  File                        :  SingleABLoader.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-21
 *  Description        :  
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class SingleABLoader : System.IDisposable
    {
        //引用类：资源加载类
        private AssetLoader _AssetLoader;
        //委托
        private DeLoadComplete _LoadCompleteHandler;
        //AssetBundle 名称
        private string _ABName;
        //AssetBundle 下载路径
        private string _ABDownLoadPath;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abName"></param>
        public SingleABLoader(string abName,DeLoadComplete loadComplete)
        {
            _ABName = abName;
            _ABDownLoadPath = PathTool.GetWWWPath() + "/" + abName;
            _LoadCompleteHandler = loadComplete;
        }
        /// <summary>
        /// 使用WWW加载AssetBundle资源包
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadAssetBundle()
        {
            using(WWW www = new WWW(_ABDownLoadPath))
            {
                //下载
                yield return www;
                //WWW下载AB包完成
                if(www.progress >= 1)
                {
                    //获取AssetBundle的实例
                    AssetBundle abObj = www.assetBundle;
                    if(abObj != null)
                    {
                        //实例化引用类
                        _AssetLoader = new AssetLoader(abObj);
                        //AssetBundle 下载完毕，调用委托
                       if (_LoadCompleteHandler != null)
                        {
                            _LoadCompleteHandler(_ABName);
                        }
                    }
                    else
                    {
                        Debug.LogError(GetType() + "/LoadAssetBundle()/WWW 下载出错请检查！ AssetBundle URL:" + _ABDownLoadPath + " 错误信息：" + www.error);
                    }
                }
            }//using end
        }
        /// <summary>
        /// 加载AB包内的资源
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="isCache"></param>
        /// <returns></returns>
        public UnityEngine.Object LoadAsset(string assetName,bool isCache)
        {
            if(_AssetLoader != null)
            {
                return _AssetLoader.LoadAsset(assetName, isCache);
            }
            Debug.LogError(GetType() + "/LoadAsset()/参数 _AssetLoader==null请检查! ");
            return null;
        }

        /// <summary>
        /// 卸载（AB包中资源）
        /// </summary>
        public void UnLoadAsset(UnityEngine.Object asset)
        {
            if(_AssetLoader != null)
            {
                _AssetLoader.UnLoadAsset(asset);
            }else
            {
                Debug.LogError(GetType() + "/UnLoadAsset()/参数_AssetLoader == null, 请检查！");
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (_AssetLoader != null)
            {
                _AssetLoader.Dispose();
                _AssetLoader = null;
            }
            else
            {
                Debug.LogError(GetType() + "/UnLoadAsset()/参数_AssetLoader == null, 请检查！");
            }
        }
        /// <summary>
        /// 释放当前AssetBundle资源包，且卸载所有资源
        /// </summary>
        public void DisposeAll()
        {
            if (_AssetLoader != null)
            {
                _AssetLoader.DisposeAll();
                _AssetLoader = null;
            }
            else
            {
                Debug.LogError(GetType() + "/UnLoadAsset()/参数_AssetLoader == null, 请检查！");
            }
        }

        //查询当前AssetBundle包中所有的资源
        public string[] RetrivalAllAssetName()
        {
            if(_AssetLoader != null)
            {
                return _AssetLoader.RetriveAllAssetName();
            }
            Debug.LogError(GetType() + "/RetrivalAllAssetName()/参数_AssetLoader == null, 请检查！");
            return null;
        }
    }
}

