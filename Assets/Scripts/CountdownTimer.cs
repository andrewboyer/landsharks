using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

    private float minutes = 0;
    private float seconds = 0;

    public Text timerlabel1;
    public Text timerlabel2;
    public float time_initial;
    public float time_left;

    public GameObject gameOverPanel;
    public Text gameOverText;

    void Start()
    {
        Time.timeScale = 1;
        time_left = time_initial;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        time_left -= Time.deltaTime;

        minutes = Mathf.Floor(time_left / 60.0f);
        seconds = Mathf.RoundToInt(time_left % 60.0f);
        if (time_left <= 0.0f)
        {
            gameOverText.text = "Shadow Wins!";
            time_left = 0.0f;
            timerEnded();
        }
        if (seconds < 10)
        {
            timerlabel1.text = "Time Left  " + minutes + ":" + "0" + seconds;
            timerlabel2.text = timerlabel1.text;
        }
        else
        {
            timerlabel1.text = "Time Left  " + minutes + ":" + seconds;
            timerlabel2.text = timerlabel1.text;
        }
    }

    public void timerEnded()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
}
