using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Master : MonoBehaviour
{
    public Generator       generator;
    public Canvas          menu;
    public Canvas          hud;
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI bestScore;
    public Player          player;

    private const string BestScoreKey = "best-score";
    private const string Subject = "Round Flow";
    private const string Text = "I have collected {0} items in Round Flow!!!\n";
    
    private void Start()
    {
        Input.gyro.enabled = true;
        var bestScoreValue = PlayerPrefs.GetInt(BestScoreKey);
        if (bestScoreValue > 0)
        {
            bestScore.text = bestScoreValue.ToString("00");
        }
        else
        {
            bestScore.transform.parent.gameObject.SetActive(false);
        }
    }

    public void OnEnable()
    {
        player.Dead += OnDead;
    }
    public void OnDisable()
    {
        player.Dead -= OnDead;
    }

    public void StartRun()
    {
        menu.gameObject.SetActive(false);
        generator.gameObject.SetActive(true);
    }

    private void OnDead()
    {
        hud.gameObject.SetActive(true);
        scoreDisplay.text = $"Your score: {player.Score}";
    }

    public void Restart()
    {
        var lastBestScore = PlayerPrefs.GetInt(BestScoreKey);

        if (player.Score > lastBestScore)
        {
            PlayerPrefs.SetInt(BestScoreKey, player.Score);
            PlayerPrefs.Save();
        }

        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void Share()
    {
        IEnumerator Routine()
        {
            yield return new WaitForEndOfFrame();
            var intentClass = new AndroidJavaClass("android.content.Intent");
            var intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), Subject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "TITLE");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), Subject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
                string.Format(Text, player.Score));
            var unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            var jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
            currentActivity.Call("startActivity", jChooser);
        }

        StartCoroutine(Routine());
    }
}
