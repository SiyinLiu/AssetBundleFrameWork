/*************************************************************************
 *  File                        :  ABManifestLoader.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-21
 *  Description        :  辅助类：读取AssetBundle依赖文件
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class ABManifestLoader : System.IDisposable
    {
        //本类实例
        private static ABManifestLoader _Instance;
        //AssetBundle(清单文件）系统类
        private AssetBundleManifest _ManifestObj;
        //AssetBundle 清单文件的路径
        private string _StrManifestPath;
        //读取AB清单文件的AssetBundle类
        private AssetBundle _ABReadMainfest;
        //清单文件是否加载（Mainfest)完成。
        private bool _IsLoadFinish;

        public bool IsLoadFinish
        {
            get { return _IsLoadFinish; }
        }

        private ABManifestLoader()
        {
            //确定清单文件WWW下载路径的问题
            _StrManifestPath = PathTool.GetWWWPath() + "/" + PathTool.GetPlatfromPath();
            _ManifestObj = null;
            _ABReadMainfest = null;
            _IsLoadFinish = false;
        }

        /// <summary>
        /// 获取本类实例
        /// </summary>
        /// <returns></returns>
        public static ABManifestLoader GetInstance() { 
            if(_Instance == null)
            {
                _Instance = new ABManifestLoader();
            }
            return _Instance;
        }

        /// <summary>
        /// 加载Manifest清单文件
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadMainfest()
        {
            using(WWW www = new WWW(_StrManifestPath))
            {
                yield return www;
                if(www.progress >= 1)
                {
                    AssetBundle abObj = www.assetBundle;
                    if(abObj != null)
                    {
                        _ABReadMainfest = abObj;
                        //读取清单文件资源。（读取到系统类的实例中)
                        _ManifestObj = _ABReadMainfest.LoadAsset(ABDefine.ASSETBUNDLE_MANIFEST) as AssetBundleManifest;//AssetBundleManifest是固定常量
                        //本次加载与读取清单文件完毕
                        _IsLoadFinish = true;
                    }
                    else
                    {
                        Debug.LogError(GetType() + "/LoadManifestFile()/WWW下载出错，请检查!,_StrManifestPath="+_StrManifestPath 
                            + " 错误信息："+ www.error);
                    }
                }
            }
        }
        /// <summary>
        /// 获取AssetBundleManifest系统类
        /// </summary>
        /// <returns></returns>
        public AssetBundleManifest GetABManifest()
        {
            if (_IsLoadFinish)
            {
                if(_ManifestObj != null)
                {
                    return _ManifestObj;
                }
                else
                {
                    Debug.LogError(GetType() + "/GetABManifest()/_ManifestObj==null 请检查！");
                }
            }
            else
            {
                Debug.Log(GetType() + "/GetABManifest()/_IsLoadFinish== false,Manifest没有加载完毕 请检查！");
            }
            return null;
        }

        /// <summary>
        /// 获取AssetBundleManifest（系统类）中指定包名称所有依赖项
        /// </summary>
        /// <param name="abName">AB包名称</param>
        /// <returns></returns>
        public string[] RetrivalDependence(string abName) {
            if(_ManifestObj != null && !string.IsNullOrEmpty(abName))
            {
                return _ManifestObj.GetAllDependencies(abName);
            }
            return null;
        }

        /// <summary>
        /// 释放本类资源
        /// </summary>
        public void Dispose()
        {
            if(_ABReadMainfest != null)
            {
                _ABReadMainfest.Unload(true);
            }
        }
    }
}

