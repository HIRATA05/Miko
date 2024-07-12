using System;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    public Text total;
    public Text coin;

    private static PointController m_instance;

    //別のシーンでもスコア管理ができるようにスクリプタブルを用意
    [Header("スコアのスクリプタブル")]
    [SerializeField] public ScoreData scoreData;

    public static PointController instance
    {
        get
        {
            if (m_instance == false)
            {
                m_instance = FindObjectOfType<PointController>();
            }
            return m_instance;
        }
    }

    public void AddCoin()
    {
        coin.text = (Convert.ToInt32(coin.text) + 1).ToString("00");
        total.text = (Convert.ToInt32(total.text) + 100).ToString("0000000");
        scoreData.Score += 100;
    }
}
