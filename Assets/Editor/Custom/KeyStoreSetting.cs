/*************************************************************************
 *  File                        :  KeyStoreSetting.cs
 *  Author                 :  DavidLiu
 *  Date                     :  2020-10-21
 *  Description        :  
 *************************************************************************/
using UnityEditor;
using UnityEngine;
[InitializeOnLoad]
public class KeyStoreSetting
{
    static KeyStoreSetting(){
        PlayerSettings.Android.keystorePass = "123456";
        PlayerSettings.Android.keyaliasPass = "123456";
    }
}
