using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
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

    //構造体の定義
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
    //ゲーム中のスコアデータを入れる
    ScoreInfo scoreInfo = new ScoreInfo("", 0);
    //シートから取得する
    public ScoreInfo[] getScoreInfo = new ScoreInfo[10];

    //スコアソート用のリストを作成
    public List<ScoreInfo> Scorelist = new List<ScoreInfo>();

    //ランキング表示のフラグ
    bool RankingDisplay = false;

    [SerializeField] private Text[] rankingNames = new Text[10];
    [SerializeField] private Text[] rankingScores = new Text[10];
    //スコアランキングのリスト　10位まで入れて表示する
    public static List<ScoreInfo> ScoreRanking = new List<ScoreInfo>();

    //ランキング表示の待機画面
    [SerializeField] private GameObject WaitPanal;

    //仮のスコア
    [SerializeField] private Text ViewScore;
    private int SCORE = 0;

    [Header("スプレッドシートのスコア管理スクリプト")]
    [SerializeField] private GSSA_ScoreManager gssa_Score;

    [Header("プレイヤー名入力のフィールド")]
    [SerializeField] private InputField nameInputField;

    //表示するtext
    [SerializeField] private Text playerName;

    void Start()
    {
        RankingDisplay = false;
    }


    void Update()
    {
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
                    gssa_Score.ChatLogSave(scoreInfo.name, scoreInfo.score);

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

                    StartCoroutine(DisplayRanking());

                    //Invoke("DisplayRanking", 5.0f);
                }

                break;
        }
    }

    private IEnumerator DisplayRanking()
    {
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
            /*
            if(scoreInfo.name != "")
            {
                //スコアを表示
                rankingNames[i].text = scoreInfo.name;
                rankingScores[i].text = scoreInfo.score.ToString();
            }
            */
        }
        //リストの中身を解放する
        Scorelist.Clear();
    }

    //名前表示
    public void NameDisplay()
    {
        playerName.text = nameInputField.text;
        scoreInfo.name = playerName.text;
    }
    //スコア加算ボタン
    public void ScoreAddButton()
    {
        SCORE += 1;
        ViewScore.text = SCORE.ToString();
        scoreInfo.score = SCORE;
    }
}
