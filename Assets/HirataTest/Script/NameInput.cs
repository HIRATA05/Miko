using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{

    //タイトル画面で使用する変数---
    [Header("プレイヤー名入力のフィールド")]
    [SerializeField] private InputField nameInputField;

    [Header("名前入力時に表示するTEXT")]
    [SerializeField] private Text playerName;

    //別のシーンでもスコア管理ができるようにスクリプタブルを用意
    [Header("スコアのスクリプタブル")]
    [SerializeField] public ScoreData scoreData;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //入力した名前を画面に表示してスクリプタブルに保存 タイトル画面の名前入力時に名前入力フィールドに使う OnEndEdit
    public void NameDisplay()
    {
        playerName.text = nameInputField.text;

        //scoreInfo.name = playerName.text;
        scoreData.Name = nameInputField.text;
    }
}
