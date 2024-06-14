using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSSA;
using System.Linq;

public class GSSA_ScoreManager : MonoBehaviour
{
    string playerName = "";

    int score = 100;

    void Start()
    {

    }

    void Update()
    {

    }

    //データの保存関数
    public void ChatLogSave()
    {
        var so = new SpreadSheetObject("Chat");
        so["name"] = playerName;
        so["message"] = score;
        so.SaveAsync();
    }

    public void DataUpdate()
    {
        //データの更新コルーチンを呼ぶ
        StartCoroutine(ChatLogGetIterator());
    }
    //データの更新コルーチン
    private IEnumerator ChatLogGetIterator()
    {
        var query = new SpreadSheetQuery("Chat");
        query.Where("name", "=", playerName);
        yield return query.FindAsync();

        var so = query.Result.FirstOrDefault();
        if (so != null)
        {
            Debug.Log(so["name"] + ">" + so["message"]);
            so["message"] = score;
            yield return so.SaveAsync();
        }
    }

    
}
