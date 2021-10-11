/*************************************************************************
 *  File                        :  BuildAssetBundle.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-11
 *  Description        :  
 *************************************************************************/
using UnityEngine;
using UnityEditor;
using System.IO;
public class BuildAssetBundle
{
    [MenuItem("AssetBundleTools/BuildAllAssetBundles")]
    public static void BuildAllAB()
    {
        //���AB�����·��
        string strABOutPathDIR = string.Empty;
        //��ȡ"StreamAssets"��ֵ
        strABOutPathDIR = Application.streamingAssetsPath;
        //�ж��������Ŀ¼�ļ���
        if (!Directory.Exists(strABOutPathDIR))
        {
            Directory.CreateDirectory(strABOutPathDIR);
        }
        BuildPipeline.BuildAssetBundles(strABOutPathDIR, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
