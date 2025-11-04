using System.Collections.Generic;
using UnityEngine;

public class StoreDatabase : MonoBehaviour
{
    [SerializeField] private string jsonResourcePatch = "Store/store_item";

    private Dictionary<string, StoreItemDto> map;

    //Limpa os registros para fins de test
    [SerializeField] private bool clearOnStart = false;

    const string PurchasedKey = "STORE_PURCHASED_IDS";

    private void Awake()
    {
        if (clearOnStart)
        {
            PlayerPrefs.DeleteKey(PurchasedKey);
            PlayerPrefs.Save();
        }
    }

    public IReadOnlyList<StoreItemDto> LoadAll()
    {
        TextAsset ta = Resources.Load<TextAsset>(jsonResourcePatch);
        if (ta == null)
        {
            Debug.LogError("Store Json não encontyrada");
            return new List<StoreItemDto>();
        }

        var wrapper = JsonUtility.FromJson<StoreItemsWrapper>(ta.text);

        if (wrapper?.items == null)
        {
            wrapper = new StoreItemsWrapper { items = new List<StoreItemDto>() };
        }

        //marca os itens comprados com base no PlayerPrefabs

        var purchasedCsv = PlayerPrefs.GetString(PurchasedKey, "");
        var purchasedSet = new HashSet<string>(purchasedCsv.Split(',', System.StringSplitOptions.RemoveEmptyEntries));

        foreach (var item in wrapper.items)
        {
            item.purchased = purchasedSet.Contains(item.id);
        }
        return wrapper.items;
    }

    public void SavePurchased(string id)
    {
        var purchasedCsv = PlayerPrefs.GetString(PurchasedKey, "");
        var set = new HashSet<string>(purchasedCsv.Split(',', System.StringSplitOptions.RemoveEmptyEntries));

        if (set.Add(id))
        {
            PlayerPrefs.SetString(PurchasedKey, string.Join(',', set));
            PlayerPrefs.Save();
        }
    }
}
