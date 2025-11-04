using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreModalController : MonoBehaviour
{
    [Header("Domínio")]
    [SerializeField] private StoreManager storeManager;
    [SerializeField] private StoreDatabase db;

    [Header("UI - Botões principais")]
    [SerializeField] private Button openStoreBtn;

    [Header("UI - Modal da Store")]
    [SerializeField] private GameObject storeModalPanel;
    [SerializeField] private Button closeModalBtn;
    [SerializeField] private TMP_Text modalTitleText;

    [Header("UI - Lista")]
    [SerializeField] private ScrollRect itemsScroll;
    [SerializeField] private Transform itemsContent;         // Content do ScrollRect
    [SerializeField] private GameObject itemEntryPrefab;     // Prefab com StoreItemViewUGUI

    [Header("UI - Feedback")]
    [SerializeField] private GameObject feedbackModalPanel;
    [SerializeField] private TMP_Text feedbackTitleText;
    [SerializeField] private TMP_Text feedbackMessageText;
    [SerializeField] private Button closeFeedbackBtn;

    private void OnEnable()
    {
        openStoreBtn.onClick.AddListener(ShowStore);
        closeModalBtn.onClick.AddListener(HideStore);
        closeFeedbackBtn.onClick.AddListener(HideFeedback);

        StoreManager.OnPurchaseSucceeded += OnPurchaseSucceeded;
        StoreManager.OnPurchaseFailed += OnPurchaseFailed;

        HideStore();
        HideFeedback();
    }

    private void OnDisable()
    {
        openStoreBtn.onClick.RemoveListener(ShowStore);
        closeModalBtn.onClick.RemoveListener(HideStore);
        closeFeedbackBtn.onClick.RemoveListener(HideFeedback);

        StoreManager.OnPurchaseSucceeded -= OnPurchaseSucceeded;
        StoreManager.OnPurchaseFailed -= OnPurchaseFailed;
    }

    private void ShowStore()
    {
        modalTitleText.text = "Store";
        RefreshList();
        storeModalPanel.SetActive(true);
    }

    private void HideStore()
    {
        storeModalPanel.SetActive(false);
    }

    private void RefreshList()
    {
        // limpa filhos
        for (int i = itemsContent.childCount - 1; i >= 0; i--)
            Destroy(itemsContent.GetChild(i).gameObject);

        var all = db.LoadAll();
        var available = all.Where(i => !i.purchased).ToList();

        if (available.Count == 0)
        {
            var go = new GameObject("EmptyLabel", typeof(RectTransform));
            go.transform.SetParent(itemsContent, false);
            var text = go.AddComponent<TextMeshProUGUI>();
            text.text = "Nenhum item disponível.";
            text.alignment = TextAlignmentOptions.Center;
            return;
        }

        foreach (var item in available)
        {
            var entryGO = Instantiate(itemEntryPrefab, itemsContent);
            var view = entryGO.GetComponent<StoreItemView>();
            view.Bind(item, storeManager);
        }

        // opcional: força reposicionar layout
        LayoutRebuilder.ForceRebuildLayoutImmediate(itemsContent as RectTransform);
    }

    private void OnPurchaseSucceeded(StoreItemDto item)
    {
        ShowFeedback("Sucesso", $"Compra de \"{item.name}\" realizada.");
        RefreshList();
    }

    private void OnPurchaseFailed(StoreItemDto item, string reason)
    {
        ShowFeedback("Falha", reason);
    }

    private void ShowFeedback(string title, string msg)
    {
        feedbackTitleText.text = title;
        feedbackMessageText.text = msg;
        feedbackModalPanel.SetActive(true);
    }

    private void HideFeedback()
    {
        feedbackModalPanel.SetActive(false);
    }
}
