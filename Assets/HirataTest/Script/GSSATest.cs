using System.Collections;
using System.Linq;
using UnityEngine;

using GSSA;
public class GSSATest : MonoBehaviour
{
    string playerName = "";

    int score = 100;

    void Start()
    {
        
    }

    //データの保存関数
    public void ChatLogSave()
    {
        var so = new SpreadSheetObject("Chat");
        so["name"] = "playerName";
        so["message"] = "たべないでください！";
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
        query.Where("name", "=", "shouta");
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