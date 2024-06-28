using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGameManager : MonoBehaviour
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
    ScoreInfo scoreInfo = new ScoreInfo("", 0);

    struct RankingText
    {
        public Text name;
        public Text score;
    }
    [SerializeField] private RankingText rankingText;

    [SerializeField] private Text rankingNames;
    [SerializeField] private Text rankingScores;
    //スコアランキングのリスト　10位まで入れて表示する
    public static List<ScoreInfo> ScoreRanking = new List<ScoreInfo>();

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
        gssa_Score.DataGet();
    }

    
    void Update()
    {
        switch (gameState)
        {
            case GameState.Title://名前入力
                if (!TitlePanel.activeSelf) TitlePanel.SetActive(true);
                if (InGamePanel.activeSelf) InGamePanel.SetActive(false);
                if (RankingPanel.activeSelf) RankingPanel.SetActive(false);
                //キーで切り替え
                if (Input.GetKeyDown(KeyCode.Q)) { gameState = GameState.InGame; }
                break;
            case GameState.InGame://ランキングに移動時scoreInfoのデータをシートに出力
                if (TitlePanel.activeSelf) TitlePanel.SetActive(false);
                if (!InGamePanel.activeSelf) InGamePanel.SetActive(true);
                if (RankingPanel.activeSelf) RankingPanel.SetActive(false);
                //キーで切り替え
                if (Input.GetKeyDown(KeyCode.P)) { gameState = GameState.ScoreRanking; }
                break;
            case GameState.ScoreRanking://ランキングに反映　
                if (TitlePanel.activeSelf) TitlePanel.SetActive(false);
                if (InGamePanel.activeSelf) InGamePanel.SetActive(false);
                if (!RankingPanel.activeSelf) RankingPanel.SetActive(true);

                //ランキングのリストを10位まで作成
                for (int i = 0; i < 10; i++)
                {

                }

                //スコアを表示
                rankingNames.text = scoreInfo.name;
                rankingScores.text = scoreInfo.score.ToString();
                break;
        }
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
