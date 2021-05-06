using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SaveDialog
{
    
    
    

    /// <summary>
    /// 打开系统路径选择界面，选好路径
    /// </summary>
    /// <returns>选择的全局路径</returns>
    public static string SelectPath()
    {
        
        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "CSV文件|*.csv";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = Application.streamingAssetsPath.Replace('/', '\\');//默认路径
        openFileName.title = "选择文件";
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
        
        if (LocalDialog.GetSaveFileName(openFileName))
        {
            return openFileName.file;
        }
        return null;
        


    }

    
}

