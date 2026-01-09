using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 5f;
    public float jumpPower = 7f;

    [Header("リスポーン")]
    public Transform respawnPoint;

    [Header("ゴール設定")]
    public GameObject goalObject;        // 実際のゴール
    public Image goalPreviewImage;        // コイン不足時のゴールUI
    public int requiredCoins = 3;

    [Header("UI")]
    public Text coinText;

    [Header("マリオ風重力")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody2D rb;
    bool isGrounded;

    int coinCount = 0;

    // コイン管理
    List<GameObject> coins = new List<GameObject>();
    List<Vector3> coinPositions = new List<Vector3>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // コイン取得＆初期位置保存
        GameObject[] coinObjects = GameObject.FindGameObjectsWithTag("coin");
        foreach (GameObject coin in coinObjects)
        {
            coins.Add(coin);
            coinPositions.Add(coin.transform.position);
        }

        // 初期状態
        if (goalObject != null)
            goalObject.SetActive(false);

        if (goalPreviewImage != null)
            goalPreviewImage.gameObject.SetActive(true);

        UpdateCoinUI();
    }

    void Update()
    {
        // ジャンプ
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }

        // マリオ風ジャンプ制御
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y
                * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y
                * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.A))
            moveInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            moveInput = 1f;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    // 地面判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Dead"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Dead"))
            isGrounded = false;
    }

    // トリガー判定
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 落下死
        if (collision.CompareTag("Dead"))
        {
            Restart();
        }
        // コイン取得
        else if (collision.CompareTag("coin"))
        {
            coinCount++;
            collision.gameObject.SetActive(false);
            UpdateCoinUI();

            // 規定数集めたらゴール解放
            if (coinCount >= requiredCoins)
            {
                if (goalObject != null)
                    goalObject.SetActive(true);

                if (goalPreviewImage != null)
                    goalPreviewImage.gameObject.SetActive(false);
            }
        }
        // ゴール
        else if (collision.CompareTag("goal"))
        {
            SceneManager.LoadScene("goal");
        }
    }

    void Restart()
    {
        // プレイヤーリセット
        rb.linearVelocity = Vector2.zero;
        transform.position = respawnPoint.position;

        // コインリセット
        coinCount = 0;
        UpdateCoinUI();

        // ゴール状態リセット
        if (goalObject != null)
            goalObject.SetActive(false);

        if (goalPreviewImage != null)
            goalPreviewImage.gameObject.SetActive(true);

        // コイン再配置
        for (int i = 0; i < coins.Count; i++)
        {
            coins[i].SetActive(true);
            coins[i].transform.position = coinPositions[i];
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins : " + coinCount + " / " + requiredCoins;
        }
    }
}
