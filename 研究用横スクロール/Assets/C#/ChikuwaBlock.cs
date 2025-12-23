using UnityEngine;

public class ChikuwaBlock : MonoBehaviour
{
    public float timeToFall = 2.0f; // 乗ってから落ちるまで
    public float fallGravity = 3f;

    Rigidbody2D rb;

    float standTimer = 0f;
    bool isStanding = false;
    bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        if (isFalling) return;

        if (isStanding)
        {
            standTimer += Time.deltaTime;
            Debug.Log("⏱ カウント中 : " + standTimer.ToString("F2"));

            if (standTimer >= timeToFall)
            {
                Debug.Log("⬇ 2秒経過、落下開始");
                Fall();
            }
        }
    }

    public void OnPlayerStand()
    {
        if (isFalling) return;

        if (!isStanding)
        {
            Debug.Log("👣 Player が乗った（カウント開始）");
            standTimer = 0f;
            isStanding = true;
        }
    }

    public void OnPlayerExit()
    {
        if (isFalling) return;

        Debug.Log("↩ Player が離れた（カウントリセット）");
        standTimer = 0f;
        isStanding = false;
    }

    void Fall()
    {
        isFalling = true;
        rb.gravityScale = fallGravity;
    }
}
