using UnityEngine;

public class StandDetector : MonoBehaviour
{
    ChikuwaBlock block;

    void Start()
    {
        block = GetComponentInParent<ChikuwaBlock>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            block.OnPlayerStand();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            block.OnPlayerExit();
        }
    }
}
