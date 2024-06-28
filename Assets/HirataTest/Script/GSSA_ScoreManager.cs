using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSSA;
using System.Linq;

public class GSSA_ScoreManager : MonoBehaviour
{
    string playerName = "";

    int score = 0;

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
        StartCoroutine(ChatLogUpdateIterator());
    }
    //データの更新コルーチン
    private IEnumerator ChatLogUpdateIterator()
    {
        var query = new SpreadSheetQuery("Chat");
        query.Where("name", "=", playerName);
        yield return query.FindAsync();

        var so = query.Result.FirstOrDefault();
        if (so != null)
        {
            Debug.Log("データ更新：" + so["name"] + ">" + so["message"]);
            so["message"] = score;
            yield return so.SaveAsync();
        }
    }

    public void DataGet()
    {
        //データの取得コルーチンを呼ぶ
        StartCoroutine(ChatLogGetIterator());
    }
    private IEnumerator ChatLogGetIterator()
    {
        var query = new SpreadSheetQuery("Chat");
        //query.Where("name", "=", "かつーき");
        yield return query.FindAsync();

        foreach (var so in query.Result)
        {
            Debug.Log(so["name"] + ">" + so["message"]);
        }
    }

    public void DataDelete()
    {
        //データの消去コルーチンを呼ぶ
        StartCoroutine(ChatLogDelete());
    }
    //データ消去関数
    private IEnumerator ChatLogDelete()
    {
        var query = new SpreadSheetQuery("Chat");
        query.Where("name", "=", playerName);
        yield return query.FindAsync();

        var so = query.Result.FirstOrDefault();
        if (so != null)
        {
            Debug.Log("データ消去：" + so["name"] + ">" + so["message"]);
            so["name"] = "";
            so["message"] = "";
            yield return so.SaveAsync();
        }
    }
}
