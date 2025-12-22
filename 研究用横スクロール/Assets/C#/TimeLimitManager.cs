using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeLimitManager : MonoBehaviour
{
    public float timeLimit = 60f;
    private float currentTime;

    public Text timeText; // UI Text‚ð“o˜^

    void Start()
    {
        currentTime = timeLimit;
        UpdateTimeText();
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            UpdateTimeText();
            GameOver();
        }
        else
        {
            UpdateTimeText();
        }
    }

    void UpdateTimeText()
    {
        timeText.text = "Žc‚èŽžŠÔ : " + Mathf.CeilToInt(currentTime) + " •b";
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
