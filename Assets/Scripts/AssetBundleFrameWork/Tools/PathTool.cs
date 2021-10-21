/*************************************************************************
 *  File                        :  PathTool.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-20
 *  Description        :  ·�������� 
 *                                  ��������������е�·��������·������
 *************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABFW
{
    public class PathTool
    {
        //·������
        public const string AB_RESOURCES = "AB_Res";
        //·������
        public static string GetABResourcePath() {
            return Application.dataPath +"/"+ AB_RESOURCES;
        }
        /// <summary>
        /// ��ȡAB���·��
        ///     �㷨��
        ///         1��ƽ̨(PC/�ƶ���)·��
        ///         2��ƽ̨������
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
                case RuntimePlatform.WindowsEditor://���ô�͸����
                    strReturnPlatformPath = Application.streamingAssetsPath;
                    break;
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    strReturnPlatformPath = Application.persistentDataPath;//��дĿ¼
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
        /// ��ȡWWWЭ������(AB��·��)
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

