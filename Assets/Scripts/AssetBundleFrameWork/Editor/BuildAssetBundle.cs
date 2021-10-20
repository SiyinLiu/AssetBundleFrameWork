/*************************************************************************
 *  File                        :  BuildAssetBundle.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-11
 *  Description        :  对标记的资源进行打包输出
 *************************************************************************/
using UnityEngine;
using UnityEditor;
using System.IO;
namespace ABFW {
    public class BuildAssetBundle
    {
        [MenuItem("AssetBundleTools/BuildAllAssetBundles")]
        public static void BuildAllAB()
        {
            //打包AB输出的路径
            string strABOutPathDIR = string.Empty;
            //获取"StreamAssets"数值
            strABOutPathDIR = PathTool.GetABOutPath();
            //判断生成输出目录文件夹
            if (!Directory.Exists(strABOutPathDIR))
            {
                Directory.CreateDirectory(strABOutPathDIR);
            }
            BuildPipeline.BuildAssetBundles(strABOutPathDIR, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
            AssetDatabase.Refresh();
        }
    }

}

