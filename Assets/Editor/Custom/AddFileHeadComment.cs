using UnityEngine;
using System.IO;
public class AddFileHeadComment : UnityEditor.AssetModificationProcessor
{
    /// <summary>  
    /// 此函数在asset被创建完，文件已经生成到磁盘上，但是没有生成.meta文件和import之前被调用  
    /// </summary>  
    /// <param name="newFileMeta">newfilemeta 是由创建文件的path加上.meta组成的</param>  
    public static void OnWillCreateAsset(string newFileMeta)
    {
        string newFilePath = newFileMeta.Replace(".meta", "");
        string fileExt = Path.GetExtension(newFilePath);
        if (!(fileExt == ".cs" || fileExt == ".shader" || fileExt == ".compute" || fileExt == ".json" || fileExt == ".txt")){ return; } 
        //注意，Application.datapath会根据使用平台不同而不同  
        string realPath = Application.dataPath.Replace("Assets", "") + newFilePath;
        string scriptContent = File.ReadAllText(realPath);
        //这里实现自定义的一些规则  
        scriptContent = scriptContent.Replace("#SCRIPTNAME#", Path.GetFileName(newFilePath));
        scriptContent = scriptContent.Replace("#AUTHORNAME#", "SiyinLiu");
        scriptContent = scriptContent.Replace("#CREATETIME#", System.DateTime.Now.ToString("yyyy-MM-dd"));
        File.WriteAllText(realPath, scriptContent);
    }  
}
