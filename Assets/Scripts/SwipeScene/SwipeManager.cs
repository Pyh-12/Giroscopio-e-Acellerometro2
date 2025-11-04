using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwipeManager : MonoBehaviour
{
    [Header("References")]
    int acceptCount, rejectCount;
    int spriteIndex = 0;
    public CardMover currentCard;

    [Header("UI")]
    public TextMeshProUGUI AcceptCountText;
    public TextMeshProUGUI RejectCountText;
    public Button AcceptButton;   // coração
    public Button RejectButton;   // X

    [Header("Card")]
    public GameObject cardPrefab; // prefab com SpriteRenderer + Collider(2D) + CardMover
    public Transform spawnPoint;  // onde a carta nasce
    public Sprite[] cardSprites;  // lista recebida (ordem será usada em sequência)

    void Start()
    {
        // botões fazem o mesmo efeito do swipe
        if (AcceptButton) AcceptButton.onClick.AddListener(() => HandleDecision(SwipeDecision.Accept));
        if (RejectButton) RejectButton.onClick.AddListener(() => HandleDecision(SwipeDecision.Reject));

        RefreshUI();
        SpawnNextCard();
    }

    void OnDestroy()
    {
        if (currentCard) currentCard.OnSwipeReleased -= OnCardReleased;
    }

    // --- ciclo da carta ---
    void SpawnNextCard()
    {
        var position = spawnPoint ? spawnPoint.position : Vector3.zero;
        var rotation = spawnPoint ? spawnPoint.rotation : Quaternion.identity;

        var go = Instantiate(cardPrefab, position, rotation);

        // aplica PRÓXIMO sprite da lista (sequencial, com wrap)
        if (cardSprites != null && cardSprites.Length > 0)
        {
            var spriteRender = go.GetComponent<SpriteRenderer>() ?? go.GetComponentInChildren<SpriteRenderer>();
            if (spriteRender)
            {
                spriteRender.sprite = cardSprites[spriteIndex];
                spriteIndex = (spriteIndex + 1) % cardSprites.Length;
            }
        }

        currentCard = go.GetComponent<CardMover>();
        if (currentCard) currentCard.OnSwipeReleased += OnCardReleased; // inscreve callback
    }

    void OnCardReleased(SwipeDecision decision)
    {
        if (decision == SwipeDecision.None) return; // solto em x==0 não conta
        HandleDecision(decision);
    }

    void HandleDecision(SwipeDecision decision)
    {
        if (decision == SwipeDecision.Accept) acceptCount++;
        else if (decision == SwipeDecision.Reject) rejectCount++;

        RefreshUI();

        // destrói a carta atual e gera outra
        if (currentCard)
        {
            currentCard.OnSwipeReleased -= OnCardReleased;// desinscreve callback
            Destroy(currentCard.gameObject);
            currentCard = null;
        }
        SpawnNextCard();
    }

    void RefreshUI()
    {
        if (AcceptCountText) AcceptCountText.text = acceptCount.ToString();
        if (RejectCountText) RejectCountText.text = rejectCount.ToString();
    }
}
