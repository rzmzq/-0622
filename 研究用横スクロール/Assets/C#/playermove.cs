using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
            Destroy(collision.gameObject);
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
        // 移動リセット
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

        // ※注意：コインの再配置は別途必要（下に説明あり）
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins : " + coinCount + " / " + requiredCoins;
        }
    }
}
