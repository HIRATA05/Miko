using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSSA;
using System.Linq;
using static RankingManager;
using System;

public class SSManager : MonoBehaviour
{
    [SerializeField] private RankingManager rankingManager;


    void Start()
    {

    }

    void Update()
    {

    }

    //データの保存関数
    public void ChatLogSave(string name, int score)
    {
        var so = new SpreadSheetObject("Chat");
        so["name"] = name;
        so["message"] = score;
        so.SaveAsync();
    }

    public void DataUpdate(string name, int score)
    {
        //データの更新コルーチンを呼ぶ
        StartCoroutine(ChatLogUpdateIterator(name, score));
    }
    //データの更新コルーチン
    private IEnumerator ChatLogUpdateIterator(string name, int score)
    {
        var query = new SpreadSheetQuery("Chat");
        query.Where("name", "=", name);
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
    public IEnumerator ChatLogGetIterator()
    {
        var query = new SpreadSheetQuery("Chat");
        //query.Where("name", "=", "かつーき");
        yield return query.FindAsync();

        foreach (var so in query.Result)
        {
            Debug.Log(so["name"] + ">" + so["message"]);

            string s = so["message"].ToString();

            //string,int変換してリストに加える
            rankingManager.Scorelist.Add(new ScoreInfo { name = so["name"].ToString(), score = int.Parse(s) });
        }
        //スコアを入れ替える
        rankingManager.Scorelist.Sort((a, b) => b.score - a.score);

        //Debug.Log("要素数：" + testGameManager.Scorelist.Count);

        //10位分getScoreInfoに入れる
        for (int i = 0; i < 10; i++)
        {
            //リストの範囲内か確認
            if ((i >= 0) && (i < rankingManager.Scorelist.Count))
            {
                rankingManager.getScoreInfo[i].score = rankingManager.Scorelist[i].score;
                rankingManager.getScoreInfo[i].name = rankingManager.Scorelist[i].name;
            }
            else
            {
                rankingManager.getScoreInfo[i] = new ScoreInfo("", 0);
            }
            Debug.Log("取得したリスト：" + rankingManager.getScoreInfo[i].name);
        }
    }

    public void DataDelete(string name)
    {
        //データの消去コルーチンを呼ぶ
        StartCoroutine(ChatLogDelete(name));
    }
    //データ消去関数
    private IEnumerator ChatLogDelete(string name)
    {
        var query = new SpreadSheetQuery("Chat");
        query.Where("name", "=", name);
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
