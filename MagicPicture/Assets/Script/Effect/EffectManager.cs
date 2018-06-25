using System.Collections.Generic;
using UnityEngine;

//注意!
//EffectにはSuicide.csが取り付けられているものとする
//Effect生成後の管理は行っておらず、出現させる機能のみ実装
class EffectManager
{
    static EffectManager instance = null;

    public static EffectManager GetInstance()
    {
        return instance ?? (instance = new EffectManager());
    }

    Dictionary<string, GameObject> effectTable = new Dictionary<string, GameObject>();

    public void Load(string fileName)
    {
        string filePath = "Effects/" + fileName;
        this.effectTable[fileName] = Resources.Load(filePath) as GameObject;
    }

    public void PopUp(string fileName, Vector3 pos)
    {
        if (!this.effectTable.ContainsKey(fileName))
        {
            Load(fileName);
        }

        GameObject.Instantiate(this.effectTable[fileName], pos, Quaternion.identity);
    }
}