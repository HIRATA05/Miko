using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class UnityChan2DController : MonoBehaviour
{
    public float maxSpeed = 12f; // 最大速度
    public float invincibleSpeedMultiplier = 1.5f; // 無敵時の速度倍率
    public float jumpPower = 600f; // ジャンプ力
    public Vector2 backwardForce = new Vector2(-4.5f, 5.4f);
    public LayerMask whatIsGround; // 地面の判定用レイヤー

    public int lives = 3; // 残機
    public List<Image> lifeIcons; // 残機のアイコン
    public int maxLives = 5; // 最大残機数

    private Animator m_animator;
    private BoxCollider2D m_boxcollider2D;
    private Rigidbody2D m_rigidbody2D;
    private bool m_isGround;
    private const float m_centerY = 1.5f;

    private State m_state = State.Normal;

    void Reset()
    {
        Awake();

        // UnityChan2DController
        maxSpeed = 12f;
        jumpPower = 600f; // 調整済み
        backwardForce = new Vector2(-4.5f, 5.4f);
        whatIsGround = 1 << LayerMask.NameToLayer("Ground");

        // Transform
        transform.localScale = new Vector3(1, 1, 1);

        // Rigidbody2D
        m_rigidbody2D.gravityScale = 7f; // 重力スケールを増加
        m_rigidbody2D.mass = 0.5f;

        // BoxCollider2D
        m_boxcollider2D.size = new Vector2(1, 2.5f);
        m_boxcollider2D.offset = new Vector2(0, -0.25f);

        // Animator
        m_animator.applyRootMotion = false;
    }

    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_boxcollider2D = GetComponent<BoxCollider2D>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (m_state != State.Damaged)
        {
            float x = Input.GetAxis("Horizontal");
            bool jump = Input.GetButtonDown("Jump");
            Move(x, jump);
        }
    }

    void Move(float move, bool jump)
    {
        if (Mathf.Abs(move) > 0)
        {
            Quaternion rot = transform.rotation;
            transform.rotation = Quaternion.Euler(rot.x, Mathf.Sign(move) == 1 ? 0 : 180, rot.z);
        }

        float currentSpeed = m_state == State.Invincible ? maxSpeed * invincibleSpeedMultiplier : maxSpeed;
        m_rigidbody2D.velocity = new Vector2(move * currentSpeed, m_rigidbody2D.velocity.y);

        m_animator.SetFloat("Horizontal", move);
        m_animator.SetFloat("Vertical", m_rigidbody2D.velocity.y);
        m_animator.SetBool("isGround", m_isGround);

        if (jump && m_isGround)
        {
            m_animator.SetTrigger("Jump");
            SendMessage("Jump", SendMessageOptions.DontRequireReceiver);
            m_rigidbody2D.AddForce(Vector2.up * jumpPower);
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        Vector2 groundCheck = new Vector2(pos.x, pos.y - (m_centerY * transform.localScale.y));
        Vector2 groundArea = new Vector2(m_boxcollider2D.size.x * 0.49f, 0.05f);

        m_isGround = Physics2D.OverlapArea(groundCheck + groundArea, groundCheck - groundArea, whatIsGround);
        m_animator.SetBool("isGround", m_isGround);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("DamageObject") && m_state == State.Normal)
        {
            m_state = State.Damaged;
            StartCoroutine(INTERNAL_OnDamage());
        }
        else if (other.CompareTag("LifeItem"))
        {
            CollectLifeItem(other.gameObject);
        }
    }

    IEnumerator INTERNAL_OnDamage()
    {
        m_animator.Play(m_isGround ? "Damage" : "AirDamage");
        m_animator.Play("Idle");

        SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);

        m_rigidbody2D.velocity = new Vector2(transform.right.x * backwardForce.x, transform.up.y * backwardForce.y);

        yield return new WaitForSeconds(.2f);

        while (!m_isGround)
        {
            yield return new WaitForFixedUpdate();
        }

        m_animator.SetTrigger("Invincible Mode");
        m_state = State.Invincible;

        // 残機を減らす
        lives--;
        UpdateLifeIcons();

        if (lives <= 0)
        {
            // ゲームオーバー処理
            GameOver();
        }
        else
        {
            // 無敵モード終了後、再び通常状態に戻る
            yield return new WaitForSeconds(2f); // 無敵時間の長さを調整
            OnFinishedInvincibleMode();
        }
    }

    void UpdateLifeIcons()
    {
        for (int i = 0; i < lifeIcons.Count; i++)
        {
            lifeIcons[i].enabled = i < lives;
        }
    }

    void CollectLifeItem(GameObject item)
    {
        if (lives < maxLives)
        {
            lives++;
            UpdateLifeIcons();
            Destroy(item); // アイテムを破壊
        }
    }

    void OnFinishedInvincibleMode()
    {
        m_state = State.Normal;
    }

    void GameOver()
    {
        // ゲームオーバーの処理
        Debug.Log("Game Over");
        // 必要に応じて、シーンのリロードやゲームオーバーメニューの表示などを行います。
    }

    enum State
    {
        Normal,
        Damaged,
        Invincible,
    }
}