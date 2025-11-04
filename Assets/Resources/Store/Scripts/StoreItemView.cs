using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemView : MonoBehaviour
{
    [Header("Refs do Prefab")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Button buyBtn;

    private StoreItemDto _item;
    private StoreManager _manager;

    public void Bind(StoreItemDto item, StoreManager manager)
    {
        _item = item;
        _manager = manager;

        nameText.text = item.name;
        descText.text = item.description;
        costText.text = $"Cost: {item.cost}";

        // Carrega sprite do Resources
        var sprite = Resources.Load<Sprite>(item.imagepatch);
        if (sprite != null) iconImage.sprite = sprite;

        buyBtn.onClick.RemoveAllListeners();
        buyBtn.onClick.AddListener(OnBuyClicked);

        // estado visual se já comprado (opcional)
        buyBtn.interactable = !item.purchased;
    }

    private void OnBuyClicked()
    {
        _manager.TryPurchase(_item);
    }
}
