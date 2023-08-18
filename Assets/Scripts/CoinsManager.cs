using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CoinsManager : MonoBehaviour
{
    public static CoinsManager Instance;

    public int TotalCoins { get; private set; }
    public int CollectedCoins { get; private set; }

    [SerializeField] private TMP_Text coinCountText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Find total coins at the start (assuming you spawn all coins at the start)
        TotalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        UpdateCoinUI();
    }

    public void CoinCollected()
    {
        CollectedCoins++;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        coinCountText.text = $"{CollectedCoins}";
    }
}
