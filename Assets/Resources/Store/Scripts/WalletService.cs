using UnityEngine;

public class WalletService : MonoBehaviour
{
    [SerializeField] private int initialCoins = 200;
    const string CoinsKey = "WALLET_COINS";

    public int Coins
    {
        get => PlayerPrefs.GetInt(CoinsKey, initialCoins);
        private set { PlayerPrefs.SetInt(CoinsKey, value); PlayerPrefs.Save(); }
    }

    public bool CanAfford(int cost) => Coins >= cost;

    public bool TryDebit(int cost)
    {
        if (!CanAfford(cost)) return false;
        Coins -= cost;
        return true;
    }

    // opcional: método para adicionar moedas (recompensas do jogo)
    public void Add(int amount) => Coins = Mathf.Max(0, Coins + amount);
}
