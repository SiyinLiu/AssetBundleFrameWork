/*************************************************************************
 *  File                        :  ABManifestLoader.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-21
 *  Description        :  �����ࣺ��ȡAssetBundle�����ļ�
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{

    public class ABManifestLoader : System.IDisposable
    {
        //����ʵ��
        private static ABManifestLoader _Instance;
        //AssetBundle(�嵥�ļ���ϵͳ��
        private AssetBundleManifest _ManifestObj;
        //AssetBundle �嵥�ļ���·��
        private string _StrManifestPath;
        /// <summary>
        /// �ͷű�����Դ
        /// </summary>
        public void Dispose()
        {

        }
    }
}

