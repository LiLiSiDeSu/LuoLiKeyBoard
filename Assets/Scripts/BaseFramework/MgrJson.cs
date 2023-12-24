using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class Config
{
    public bool IsRepeat = false;
}

public class MgrJson : InstanceBaseAuto_Mono<MgrJson>
{    
    public string filePath = "";

    private void Awake()
    {        
        filePath = "D:/LuoLiKeyBoardData";
    }

   /// <summary>
   /// �洢
   /// </summary>
   /// <param name="data">Ҫ�洢�Ķ���</param>
   /// <param name="folderPath">�ļ���·�� ֱ��filePath�Ǳ� ��ͷҪ��/</param>
   /// <param name="fileName">�ļ��� ��ͷҪ��/</param>
    public void Save(object data, string folderPath,string fileName)
    {
        if (!Directory.Exists(filePath + folderPath))
            Directory.CreateDirectory(filePath + folderPath);        
        
        File.WriteAllText(filePath + folderPath + fileName + ".Json", JsonConvert.SerializeObject(data, Formatting.Indented));
    }



    /// <summary>
    /// ��ȡ
    /// </summary>
    /// <typeparam name="T">��ȡ�����ݵ�����</typeparam>
    /// <param name="folderPath">�ļ�����·�� ֱ��filePath�Ǳ� ��ͷҪ��/</param>
    /// <param name="fileName">�ļ��� ��ͷҪ��/</param>
    /// <param name="callback">�ص�ί��</param>
    /// <returns></returns>
    public T Load<T>(string folderPath, string fileName, UnityAction<T> callback = null) where T : class, new()
    {
        if (!Directory.Exists(filePath + folderPath))
            Directory.CreateDirectory(filePath + folderPath);        
        
        if (!File.Exists(filePath + folderPath + fileName + ".Json"))
        {
            Debug.Log("--- MgrJson: " + filePath + folderPath + fileName + ".Json" + " is null ---");
            return new();
        }

        T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath + folderPath + fileName + ".Json"));

        callback?.Invoke(data);

        return data;
    }
}