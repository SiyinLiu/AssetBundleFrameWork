/*************************************************************************
 *  File                        :  AutoSetLabels.cs
 *  Author                 :  SiyinLiu
 *  Date                     :  2021-10-20
 *  Description        :    ����˼·��
 *                                  1��������Ҫ�����Դ���ļ��и�Ŀ¼
 *                                  2������ÿ�������ļ���
 *                                          2.1:  ����������Ŀ¼�����е�Ŀ¼�����ļ�,�����Ŀ¼�������"�ݹ�"����������ļ���ֱ����λ���ļ�
 *                                          2.2:  �ҵ��ļ�����ʹ��AssetInporter�࣬���"����"��"��׺��"
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
        /// ����AB������
        /// </summary>
        [MenuItem("AssetBundleTools/Set AB Label")]
        public static void SetABLable()
        {
            //��Ҫ��AB����ǵĸ�Ŀ¼
            string strNeedSetLabelRoot = string.Empty;
            //Ŀ¼��Ϣ(����Ŀ¼��Ϣ���飬��ʾ���еĸ�Ŀ¼�³���Ŀ¼)
            DirectoryInfo[] dirScenesDIRArray = null;
            //�������AB���
            AssetDatabase.RemoveUnusedAssetBundleNames();
            //��Ҫ�����Դ���ļ��и�Ŀ¼
            strNeedSetLabelRoot = PathTool.GetABResourcePath();
            //����ÿ��"����"�ļ��У�Ŀ¼��
            DirectoryInfo dirTempInfo = new DirectoryInfo(strNeedSetLabelRoot);
            dirScenesDIRArray = dirTempInfo.GetDirectories();
            //����ÿ��"����"�ļ���(Ŀ¼)
            foreach(DirectoryInfo currentDIR in dirScenesDIRArray)
            {
                string tmpScenesDIR = strNeedSetLabelRoot + "/" + currentDIR.Name;
                //DirectoryInfo tmpScenesDIRInfo = new DirectoryInfo(tmpScenesDIR);
                int tmpIndex = tmpScenesDIR.LastIndexOf("/");
                string tmpScenesName = tmpScenesDIR.Substring(tmpIndex + 1);        //��������
                //�ݹ���÷����ҵ��ļ�����ʹ��AssetImporter�࣬���"����"��"��׺��"
                JudgeDIRorFileByRecursive(currentDIR, tmpScenesName);
            }
            AssetDatabase.Refresh();
            Debug.Log("AssetBundle ���β������ñ����ɣ�");
        }

        /// <summary>
        ///�ж��Ƿ�ΪĿ¼���ļ����޸�AssetBundle�ı��(lable) 
        /// </summary>
        /// <param name="currentDIR">��ǰ�ļ���Ϣ���ļ���Ϣ��Ŀ¼��Ϣ�����໥ת����</param>
        /// <param name="scenesName">��ǰ����������</param>
        private static void JudgeDIRorFileByRecursive(FileSystemInfo fileSystemInfo,string scenesName)
        {
            //�������
            if (!fileSystemInfo.Exists)
            {
                Debug.LogError("�ļ�����Ŀ¼���ƣ�" + fileSystemInfo + " �����ڣ�����");
                return;
            }
            //�õ���ǰĿ¼��һ�����ļ���Ϣ����
            DirectoryInfo dirInfoObj = fileSystemInfo as DirectoryInfo;
            FileSystemInfo[] fileSysArray = dirInfoObj.GetFileSystemInfos();
            foreach(var fileInfo in fileSysArray)
            {
                FileInfo fileinfoObj = fileInfo as FileInfo;
                if(fileinfoObj != null)
                {
                    //�޸Ĵ��ļ���AssetBundle��ǩ
                    SetFileABLabel(fileinfoObj, scenesName);
                }
                //Ŀ¼����
                else
                {
                    //�����Ŀ¼�͵ݹ������һ��Ŀ¼��
                    JudgeDIRorFileByRecursive(fileInfo, scenesName);
                }
            }
        }
        /// <summary>
        /// ��ָ�����ļ�����"AB����"
        /// </summary>
        /// <param name="fileinfoObj">�ļ���Ϣ(����·��)</param>
        /// <param name="scenesName">��������</param>
        private static void SetFileABLabel(FileInfo fileinfoObj,string scenesName)
        {
            //AssetBundle��������
            string strABName = string.Empty;
            string strAssetFilePath = string.Empty;
            //�������(*.meta�ļ���������)
            if(fileinfoObj.Extension == ".meta")
            {
                return;
            }
            //�õ�AB������
            strABName = GetABName(fileinfoObj, scenesName);
            //��ȡ��Դ�ļ������·��
            int tmpIndex = fileinfoObj.FullName.IndexOf("Assets");
            strAssetFilePath = fileinfoObj.FullName.Substring(tmpIndex);    //�õ��ļ����·��
            //����Դ�ļ�����AB�����Լ���׺
            AssetImporter tmpImporterObj = AssetImporter.GetAtPath(strAssetFilePath);
            tmpImporterObj.assetBundleName = strABName;       //������ַ�����Ҫ�滻
            if (fileinfoObj.Extension == ".unity")
            {
                //����AB������չ��
                tmpImporterObj.assetBundleVariant = "u3d";

            }
            else
            {
                tmpImporterObj.assetBundleVariant = "ab";
            }
        }
        /// <summary>
        /// ��ȡAB��������
        /// </summary>
        /// <param name="fileInfoObj">�ļ���Ϣ</param>
        /// <param name="scenesName">��������</param>
        /// AB ���γɹ���
        ///     �ļ�AB������=���ڶ���Ŀ¼���ƣ��������ƣ� + ����Ŀ¼���ƣ���һ���� �������ƣ�
        /// <returns>����������AB������</returns>
        private static string GetABName(FileInfo fileInfoObj,string scenesName)
        {
            string strABName = string.Empty;
            //Windows ·��
            string tmpWinPath = fileInfoObj.FullName;
            //Unity ·��
            string tmpUnityPath = tmpWinPath.Replace("\\", "/");//�滻ΪUnity�ַ����ָ��
            int tmpSceneNamePosition = tmpUnityPath.IndexOf(scenesName) + scenesName.Length;
            //AB����"��������"��������
            string strABFileNameArea = tmpUnityPath.Substring(tmpSceneNamePosition + 1);
            //����
            if (strABFileNameArea.Contains("/"))
            {
                string[] tempStrArray = strABFileNameArea.Split('/');
                //AB��������ʽ�γ�
                strABName = scenesName + "/" + tempStrArray[0];
            }
            else
            {
                //����*.Unity�ļ��γɵ������ļ�����
                strABName = scenesName + "/" + scenesName;
            }
            return strABName;
        }
    }
}
