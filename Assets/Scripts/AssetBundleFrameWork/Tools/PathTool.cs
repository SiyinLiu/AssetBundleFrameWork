/*************************************************************************
 *  File                        :  PathTool.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-20
 *  Description        :  路径工具类 
 *                                  包含本框架中所有的路径常量，路径方法
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class PathTool
    {
        //路径常量
        public const string AB_RESOURCES = "AB_Res";
        //路径方法
        public static string GetABResourcePath() {
            return Application.dataPath +"/"+ AB_RESOURCES;
        }
        /// <summary>
        /// 获取AB输出路径
        ///     算法：
        ///         1：平台(PC/移动端)路径
        ///         2：平台的名称
        /// </summary>
        /// <returns></returns>
        public static string GetABOutPath()
        {

            return GetPlatfromPath() + "/" + GetPlatformName();        
        }

        private static string GetPlatfromPath()
        {
            string strReturnPlatformPath = string.Empty;
            switch (Application.platform)
            {
              
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor://利用穿透现象
                    strReturnPlatformPath = Application.streamingAssetsPath;
                    break;
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    strReturnPlatformPath = Application.persistentDataPath;//读写目录
                    break;
                default:
                    break;
            }
            return strReturnPlatformPath;
        }
        private static string GetPlatformName()
        {
            string strReturnPlatformName = string.Empty;
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturnPlatformName = "Windows";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    strReturnPlatformName = "Iphone";
                    break;
                case RuntimePlatform.Android:
                    strReturnPlatformName = "Android";
                    break;
                default:
                    break;
            }
            return strReturnPlatformName;
        }

        /// <summary>
        /// 获取WWW协议下载(AB包路径)
        /// </summary>
        /// <returns></returns>
        public static string GetWWWPath()
        {
            string strReturnWWWPath = string.Empty;
            switch (Application.platform)
            {
               
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturnWWWPath = "file://" + GetABOutPath();
                    break;
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    strReturnWWWPath = "jar:file://" + GetABOutPath();
                    break;
                default:
                    break;
            }
            return strReturnWWWPath;
        }
    }
}

