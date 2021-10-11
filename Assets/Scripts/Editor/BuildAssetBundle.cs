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
        //打包AB输出的路径
        string strABOutPathDIR = string.Empty;
        //获取"StreamAssets"数值
        strABOutPathDIR = Application.streamingAssetsPath;
        //判断生成输出目录文件夹
        if (!Directory.Exists(strABOutPathDIR))
        {
            Directory.CreateDirectory(strABOutPathDIR);
        }
        BuildPipeline.BuildAssetBundles(strABOutPathDIR, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
