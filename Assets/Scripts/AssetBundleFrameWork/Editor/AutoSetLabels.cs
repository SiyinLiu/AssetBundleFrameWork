/*************************************************************************
 *  File                        :  AutoSetLabels.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-20
 *  Description        :    开发思路：
 *                                  1、定义需要打包资源的文件夹根目录
 *                                  2、遍历每个场景文件夹
 *                                          2.1:  遍历本场景目录下所有的目录或者文件,如果是目录，则继续"递归"访问里面的文件，直到定位到文件
 *                                          2.2:  找到文件，则使用AssetInporter类，标记"包名"与"后缀名"
 *  
 *************************************************************************/
using UnityEditor;
using System.IO;
using UnityEngine;

namespace ABFW
{
    public class AutoSetLabels : Editor
    {
        /// <summary>
        /// 设置AB包名称
        /// </summary>
        [MenuItem("AssetBundleTools/Set AB Label")]
        public static void SetABLable()
        {
            //需要给AB做标记的根目录
            string strNeedSetLabelRoot = string.Empty;
            //目录信息(场景目录信息数组，表示所有的根目录下场景目录)
            DirectoryInfo[] dirScenesDIRArray = null;
            //清空无用AB标记
            AssetDatabase.RemoveUnusedAssetBundleNames();
            //需要打包资源的文件夹根目录
            strNeedSetLabelRoot = PathTool.GetABResourcePath();
            //遍历每个"场景"文件夹（目录）
            DirectoryInfo dirTempInfo = new DirectoryInfo(strNeedSetLabelRoot);
            dirScenesDIRArray = dirTempInfo.GetDirectories();
            //遍历每个"场景"文件夹(目录)
            foreach(DirectoryInfo currentDIR in dirScenesDIRArray)
            {
                string tmpScenesDIR = strNeedSetLabelRoot + "/" + currentDIR.Name;
                //DirectoryInfo tmpScenesDIRInfo = new DirectoryInfo(tmpScenesDIR);
                int tmpIndex = tmpScenesDIR.LastIndexOf("/");
                string tmpScenesName = tmpScenesDIR.Substring(tmpIndex + 1);        //场景名称
                //递归调用方法找到文件，则使用AssetImporter类，标记"包名"与"后缀名"
                JudgeDIRorFileByRecursive(currentDIR, tmpScenesName);
            }
            AssetDatabase.Refresh();
            Debug.Log("AssetBundle 本次操作设置标记完成！");
        }

        /// <summary>
        ///判断是否为目录与文件，修改AssetBundle的标记(lable) 
        /// </summary>
        /// <param name="currentDIR">当前文件信息（文件信息和目录信息可以相互转换）</param>
        /// <param name="scenesName">当前场景的名称</param>
        private static void JudgeDIRorFileByRecursive(FileSystemInfo fileSystemInfo,string scenesName)
        {
            //参数检查
            if (!fileSystemInfo.Exists)
            {
                Debug.LogError("文件或者目录名称：" + fileSystemInfo + " 不存在，请检查");
                return;
            }
            //得到当前目录下一级的文件信息集合
            DirectoryInfo dirInfoObj = fileSystemInfo as DirectoryInfo;
            FileSystemInfo[] fileSysArray = dirInfoObj.GetFileSystemInfos();
            foreach(var fileInfo in fileSysArray)
            {
                FileInfo fileinfoObj = fileInfo as FileInfo;
                if(fileinfoObj != null)
                {
                    //修改此文件的AssetBundle标签
                    SetFileABLabel(fileinfoObj, scenesName);
                }
                //目录类型
                else
                {
                    //如果是目录就递归调用下一层目录。
                    JudgeDIRorFileByRecursive(fileInfo, scenesName);
                }
            }
        }
        /// <summary>
        /// 对指定地文件设置"AB包名"
        /// </summary>
        /// <param name="fileinfoObj">文件信息(绝对路径)</param>
        /// <param name="scenesName">场景名称</param>
        private static void SetFileABLabel(FileInfo fileinfoObj,string scenesName)
        {
            //AssetBundle包地名称
            string strABName = string.Empty;
            string strAssetFilePath = string.Empty;
            //参数检查(*.meta文件不做处理)
            if(fileinfoObj.Extension == ".meta")
            {
                return;
            }
            //得到AB包名称
            strABName = GetABName(fileinfoObj, scenesName);
            //获取资源文件地相对路径
            int tmpIndex = fileinfoObj.FullName.IndexOf("Assets");
            strAssetFilePath = fileinfoObj.FullName.Substring(tmpIndex);    //得到文件相对路径
            //给资源文件设置AB名称以及后缀
            AssetImporter tmpImporterObj = AssetImporter.GetAtPath(strAssetFilePath);
            tmpImporterObj.assetBundleName = strABName;       //这里地字符串需要替换
            if (fileinfoObj.Extension == ".unity")
            {
                //定义AB包地扩展名
                tmpImporterObj.assetBundleVariant = "u3d";

            }
            else
            {
                tmpImporterObj.assetBundleVariant = "ab";
            }
        }
        /// <summary>
        /// 获取AB包地名称
        /// </summary>
        /// <param name="fileInfoObj">文件信息</param>
        /// <param name="scenesName">场景名称</param>
        /// AB 包形成规则：
        ///     文件AB包名称=所在二级目录名称（场景名称） + 三级目录名称（下一级的 类型名称）
        /// <returns>返回完整的AB包名称</returns>
        private static string GetABName(FileInfo fileInfoObj,string scenesName)
        {
            string strABName = string.Empty;
            //Windows 路径
            string tmpWinPath = fileInfoObj.FullName;
            //Unity 路径
            string tmpUnityPath = tmpWinPath.Replace("\\", "/");//替换为Unity字符串分割符
            int tmpSceneNamePosition = tmpUnityPath.IndexOf(scenesName) + scenesName.Length;
            //AB包中"类型名称"所在区域
            string strABFileNameArea = tmpUnityPath.Substring(tmpSceneNamePosition + 1);
            //测试
            if (strABFileNameArea.Contains("/"))
            {
                string[] tempStrArray = strABFileNameArea.Split('/');
                //AB包名称正式形成
                strABName = scenesName + "/" + tempStrArray[0];
            }
            else
            {
                //定义*.Unity文件形成的特殊文件名称
                strABName = scenesName + "/" + scenesName;
            }
            return strABName;
        }
    }
}
