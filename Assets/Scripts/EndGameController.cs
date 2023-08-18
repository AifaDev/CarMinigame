using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel; // Drag the gameover_panel object here in the Inspector
    private string winningStageTagName = "WinningStage"; // Set the tag name for your winning stage object

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(winningStageTagName))
        {
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        float collected = CoinsManager.Instance.CollectedCoins;
        float TotalCoins = GameObject.FindGameObjectsWithTag("Coin").Length + collected;
        gameOverPanel.transform.Find("ScoreText").GetComponent<TMPro.TMP_Text>().text = $"Score: {collected}/{TotalCoins} coins collected";
        gameOverPanel.SetActive(true);

       

    }
}
