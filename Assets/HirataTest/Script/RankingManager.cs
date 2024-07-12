using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    //構造体の定義---
    public struct ScoreInfo
    {
        public string name;
        public int score;

        public ScoreInfo(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    //シートから取得する
    public ScoreInfo[] getScoreInfo = new ScoreInfo[10];

    //スコアソート用のリストを作成
    public List<ScoreInfo> Scorelist = new List<ScoreInfo>();



    //ランキング画面で使用する変数---
    //ランキングで表示するテキストの配列
    [SerializeField] private Text[] rankingNames = new Text[10];
    [SerializeField] private Text[] rankingScores = new Text[10];

    //ランキング表示の待機画面
    [SerializeField] private GameObject WaitPanal;

    //ランキング表示のフラグ
    bool RankingDisplay = false;


    [Header("スプレッドシートのスコア管理スクリプト")]
    [SerializeField] private SSManager ssManager;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //スコアを取得していない時シートから取得する
        if (!RankingDisplay)
        {
            RankingDisplay = true;

            //コルーチンを使ってランキングを表示 ランキング画面でこれを使う
            StartCoroutine(DisplayRanking());
        }
    }

    private IEnumerator DisplayRanking()
    {
        //スコアの取得が終了するまで待機する
        yield return ssManager.ChatLogGetIterator();
        //gssa_Score.DataGet();

        //待機画面非表示
        if (WaitPanal.activeSelf)
        {
            WaitPanal.SetActive(false);
        }

        //ランキングのリストを10位まで表示
        for (int i = 0; i < 10; i++)
        {
            //スコアを表示
            rankingNames[i].text = getScoreInfo[i].name;
            rankingScores[i].text = getScoreInfo[i].score.ToString();

        }
        //リストの中身を解放する
        Scorelist.Clear();
    }


}
