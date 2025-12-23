using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpPower = 7f;
    public Transform respawnPoint;

    public GameObject goalObject;      // ゴールオブジェクト
    public int requiredCoins = 3;      // 必要コイン数
    public Text coinText;              // コインUI

    Rigidbody2D rb;
    bool isGrounded;
    int coinCount = 0;

    // コイン管理用
    List<GameObject> coins = new List<GameObject>();
    List<Vector3> coinPositions = new List<Vector3>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // コインを取得して初期位置を保存
        GameObject[] coinObjects = GameObject.FindGameObjectsWithTag("coin");
        foreach (GameObject coin in coinObjects)
        {
            coins.Add(coin);
            coinPositions.Add(coin.transform.position);
        }

        // ゴールを非表示
        if (goalObject != null)
        {
            goalObject.SetActive(false);
        }

        UpdateCoinUI();
    }

    void Update()
    {
        // ジャンプ
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }
    }

    void FixedUpdate()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    // 地面判定
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Dead"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Dead"))
        {
            isGrounded = false;
        }
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
            collision.gameObject.SetActive(false); // Destroyしない
            UpdateCoinUI();

            // ゴール出現
            if (coinCount >= requiredCoins && goalObject != null)
            {
                goalObject.SetActive(true);
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
        // プレイヤー位置リセット
        rb.linearVelocity = Vector2.zero;
        transform.position = respawnPoint.position;

        // コインリセット
        coinCount = 0;
        UpdateCoinUI();

        // ゴール非表示
        if (goalObject != null)
        {
            goalObject.SetActive(false);
        }

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
