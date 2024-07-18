using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Image clearImage;

    public Transform target;
    public Transform stopPosition;

    [SceneName]
    public string nextLevel;

    private Camera m_camera;

    [Header("スプレッドシートのスコア管理スクリプト")]
    [SerializeField] private GSSA_ScoreManager gssa_Score;

    //別のシーンでもスコア管理ができるようにスクリプタブルを用意
    [Header("スコアのスクリプタブル")]
    [SerializeField] public ScoreData scoreData;

    void Awake()
    {
        m_camera = GetComponent<Camera>();
        
        this.clearImage.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        var right = m_camera.ViewportToWorldPoint(Vector2.right);
        var center = m_camera.ViewportToWorldPoint(Vector2.one * 0.5f);

        if (center.x < target.position.x)
        {
            var pos = m_camera.transform.position;

            if (Math.Abs(pos.x - target.position.x) >= 0.0000001f)
            {
                m_camera.transform.position = new Vector3(target.position.x, pos.y, pos.z);
            }
        }

        if (stopPosition.position.x - right.x < 0)
        {
            //スコアをスプレッドシートに保存 
            gssa_Score.ChatLogSave(scoreData.Name, scoreData.Score);
            StartCoroutine(INTERNAL_Clear());
            enabled = false;
        }
    }

    private IEnumerator INTERNAL_Clear()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            player.SendMessage("Clear", SendMessageOptions.DontRequireReceiver);
        }

        this.clearImage.gameObject.SetActive(true);

        

        yield return new WaitForSeconds(3);
        //SceneManager.LoadScene(nextLevel);
        SceneManager.LoadScene("RankingScene");
    }
}
