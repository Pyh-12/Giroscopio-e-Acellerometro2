using System;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static event Action<StoreItemDto> OnPurchasedSucess;
    public static event Action<StoreItemDto, string> OnPurchasedFail;

    [SerializeField] private WalletService wallet;
    [SerializeField] private StoreDatabase db;


    public void TryPurchase(StoreItemDto item)
    {
        if (wallet == null)
        {
            OnPurchasedFail?.Invoke(item, "Item inválido.");
            return;
        }

        if (item.purchased)
        {
            OnPurchasedFail?.Invoke(item, "Item já comprado.");
            return;
        }

        if (wallet.TryDebit(item.cost))
        {
            OnPurchasedFail?.Invoke(item, "Falha ao .");
            return;
        }

        if(wallet.CanAfford(item.cost))
        {
            OnPurchasedFail?.Invoke(item, "Moedas insuficiente.");
            return;
        }

        db.SavePurchased(item.id);

        item.purchased = true;

        //Aqui iria o script de instaciar o 

        OnPurchasedSucess
    }
}
