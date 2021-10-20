/*************************************************************************
 *  File                        :  DeleteAssetBundle.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-20
 *  Description        :  
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
namespace ABFW {
    public class DeleteAssetBundle
    {
        [MenuItem("AssetBundleTools/DeleteAllAssetBundles")]
        public static void DelAssetBundle()
        {
            string strNeedDeleteDIR = string.Empty;
            strNeedDeleteDIR = PathTool.GetABOutPath();
            if (!string.IsNullOrEmpty(strNeedDeleteDIR))
            {
                //ע�⣺�������"true"��ʾ����ɾ���ǿ�Ŀ¼
                Directory.Delete(strNeedDeleteDIR,true);
                File.Delete(strNeedDeleteDIR + ".meta");
                AssetDatabase.Refresh();
            } 
        }
    }
}

