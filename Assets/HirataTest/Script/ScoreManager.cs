using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //タイトル　インゲーム　ランキングの３シーンでアタッチ 関数を呼び出してスコアを管理する


    /*
    //テスト時に使用したステート 実際に組み込む時はコメントアウトで動作しないようにすること
    enum GameState
    {
        Title,
        InGame,
        ScoreRanking,
    }
    GameState gameState = GameState.Title;
    //ゲームステート画面
    [SerializeField] private GameObject TitlePanel;
    [SerializeField] private GameObject InGamePanel;
    [SerializeField] private GameObject RankingPanel;
    */
    
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



    //タイトル画面で使用する変数---
    [Header("プレイヤー名入力のフィールド")]
    [SerializeField] private InputField nameInputField;

    [Header("名前入力時に表示するTEXT")]
    [SerializeField] private Text playerName;


    /*
    //ランキング画面は個別のシーンを作成したため 実際に組み込む時はコメントアウトで動作しないようにすること
    //ランキング画面で使用する変数---
    //ランキングで表示するテキストの配列
    [SerializeField] private Text[] rankingNames = new Text[10];
    [SerializeField] private Text[] rankingScores = new Text[10];

    //ランキング表示の待機画面
    [SerializeField] private GameObject WaitPanal;

    //ランキング表示のフラグ
    bool RankingDisplay = false;



    //ボタンで加算したスコアを表示 テスト時に使用した
    [SerializeField] private Text ViewScore;
    */


    //スコアを管理するために使う色々

    [Header("スプレッドシートのスコア管理スクリプト")]
    [SerializeField] private GSSA_ScoreManager gssa_Score;

    //別のシーンでもスコア管理ができるようにスクリプタブルを用意
    [Header("スコアのスクリプタブル")]
    [SerializeField] public ScoreData scoreData;


    void Start()
    {
        //実際に組み込む時はコメントアウトで動作しないようにすること
        /*
        ScoreInit(scoreData);
        */
    }

    
    void Update()
    {
        //実際に組み込む時はコメントアウトで動作しないようにすること
        /*
        switch (gameState)
        {
            case GameState.Title://名前入力
                if (!TitlePanel.activeSelf) TitlePanel.SetActive(true);
                if (InGamePanel.activeSelf) InGamePanel.SetActive(false);
                if (RankingPanel.activeSelf) RankingPanel.SetActive(false);
                //名前が入力されている時キーで切り替え
                if (Input.GetKeyDown(KeyCode.Q) && nameInputField.text != "") { gameState = GameState.InGame; }
                break;
            case GameState.InGame://ランキングに移動時scoreInfoのデータをシートに出力
                if (TitlePanel.activeSelf) TitlePanel.SetActive(false);
                if (!InGamePanel.activeSelf) InGamePanel.SetActive(true);
                if (RankingPanel.activeSelf) RankingPanel.SetActive(false);
                //キーで切り替え
                if (Input.GetKeyDown(KeyCode.P))
                {
                    //スコアをスプレッドシートに保存 
                    gssa_Score.ChatLogSave(scoreData.Name, scoreData.Score);
                    //gssa_Score.ChatLogSave(scoreInfo.name, scoreInfo.score);

                    gameState = GameState.ScoreRanking;
                }
                break;
            case GameState.ScoreRanking://ランキングに反映　
                if (TitlePanel.activeSelf) TitlePanel.SetActive(false);
                if (InGamePanel.activeSelf) InGamePanel.SetActive(false);
                if (!RankingPanel.activeSelf) RankingPanel.SetActive(true);

                //スコアを取得していない時シートから取得する
                if (!RankingDisplay)
                {
                    RankingDisplay = true;

                    //コルーチンを使ってランキングを表示 ランキング画面でこれを使う
                    StartCoroutine(DisplayRanking());
                }

                break;
        }
        */
    }
    /*
    private IEnumerator DisplayRanking()
    {
        //スコアの取得が終了するまで待機する
        yield return gssa_Score.ChatLogGetIterator();
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
    */

    //入力した名前を画面に表示してスクリプタブルに保存 タイトル画面の名前入力時に名前入力フィールドに使う OnEndEdit
    public void NameDisplay()
    {
        playerName.text = nameInputField.text;

        //scoreInfo.name = playerName.text;
        scoreData.Name = nameInputField.text;
    }

    //引数をスクリプタブルのスコアに保存する ゲーム終了時の最終的なスコアを入れること
    public void ScoreSave(int score)
    {
        scoreData.Score = score;
    }

    //スクリプタブルのスコアを初期化する タイトル画面のスタート関数で使うこと
    public void ScoreInit(ScoreData scoreData)
    {
        scoreData.Name = "";
        scoreData.Score = 0;
    }

    /*
    //スコア加算ボタン テスト用に作成したもの
    public void ScoreAddButton()
    {
        //SCORE += 1;
        scoreData.Score += 1;
        //ViewScore.text = SCORE.ToString();
        ViewScore.text = scoreData.Score.ToString();
        //scoreInfo.score = SCORE;
        //scoreInfo.score = scoreData.Score;
    }
    */
}
