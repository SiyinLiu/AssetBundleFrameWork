/*************************************************************************
 *  File                        :  AssetLoader.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-20
 *  Description        :  框架主流程：第一层：AB资源加载类
 *      功能：
 *      1：管理与加载指定AB的资源
 *      2：加载具有"缓存功能"的资源，带选用参数
 *      3：卸载、释放AB资源
 *      4：查看当前AB资源
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABFW
{
    public class AssetLoader : System.IDisposable
    {
        //当前AssetBundle
        private AssetBundle _CurrentAssetBundle;
        //缓存容器集合
        private Hashtable _Ht;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abObj">给WWW加载的AssetBundle实例</param>
        public AssetLoader(AssetBundle abObj)
        {
            if(abObj != null)
            {
                _CurrentAssetBundle = abObj;
                _Ht = new Hashtable();
            }
            else
            {
                Debug.LogError(GetType() + "/ 构造函数 AssetBundle()/ 参数abObj == null !请检查");
            }
        }
        /// <summary>
        /// 加载当前包中指定的资源
        /// </summary>
        /// <param name="assetName">资源的名称</param>
        /// <param name="isCache">是否开启缓存</param>
        /// <returns></returns>
        public Object LoadAsset(string assetName, bool isCache = false)
        {
            return LoadResource<Object>(assetName, isCache);
        }

        /// <summary>
        /// 加载当前AB包的资源，带缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName">资源的名称</param>
        /// <param name="isCache">是否开启缓存</param>
        /// <returns></returns>
        private T LoadResource<T>(string assetName, bool isCache) where T : UnityEngine.Object
        {
            if (_Ht.Contains(assetName))
            {
                return _Ht[assetName] as T;
            }
            //正式加载
            T tmpTResource =  _CurrentAssetBundle.LoadAsset<T>(assetName);
            if(tmpTResource != null && isCache)
            {
                _Ht.Add(assetName, tmpTResource);
            }
            else if(tmpTResource == null)
            {
                Debug.LogError(GetType() + "/LoadResource<T>()/参数 tmpTResource==null, 请检查！");
            }
            return tmpTResource;
        }
        /// <summary>
        /// 卸载指定的资源
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public bool UnLoadAsset(UnityEngine.Object asset)
        {
            if(asset != null)
            {
                Resources.UnloadAsset(asset);
            }
            Debug.LogError(GetType() + "/UnLoadAsset()/参数 asset == null, 请检查");
            return false;
        }
        /// <summary>
        /// 释放当前AssetBundle内存镜像资源
        /// </summary>
        public void Dispose()
        {
            //仅仅释放AssetBundle内存镜像资源
            _CurrentAssetBundle.Unload(false);
        }
        /// <summary>
        /// 释放当前AssetBundle内存资源，且释放内存资源
        /// </summary>
        public void DisposeAll()
        {
            _CurrentAssetBundle.Unload(true);
        }

        /// <summary>
        /// 查询当前AssetBundle包含的所有资源名称
        /// </summary>
        /// <returns></returns>
        public string[] RetriveAllAssetName()
        {
            return _CurrentAssetBundle.GetAllAssetNames();
        }
    }
}

