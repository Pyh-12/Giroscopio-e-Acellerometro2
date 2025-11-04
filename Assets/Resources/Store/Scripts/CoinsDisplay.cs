using TMPro;
using UnityEngine;

public class CoinsText : MonoBehaviour
{
    [SerializeField] private WalletService wallet;
    private TMP_Text textCoins;

    private void Awake()
    {
        textCoins = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        UpdateCoins();
        StoreManager.OnPurchaseSucceeded += _ => UpdateCoins();
    }

    private void OnDisable()
    {
        StoreManager.OnPurchaseSucceeded -= _ => UpdateCoins();
    }

    private void UpdateCoins()
    {
        textCoins.text = wallet.Coins.ToString();
    }
}
