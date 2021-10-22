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
        /// <summary>
        /// 释放本类资源
        /// </summary>
        public void Dispose()
        {

        }
    }
}

