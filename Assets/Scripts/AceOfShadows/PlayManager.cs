using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class PlayManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Stack stackA;
    [SerializeField] private Stack stackB;

    [SerializeField] private GameObject card;
    [SerializeField] private int numCards;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI countA;
    [SerializeField] private TextMeshProUGUI countB;
    private void Start()
    {
        InitializeStack();

        InvokeRepeating("MoveCard", 2f, 1f);
    }

    private void InitializeStack()
    {
        for (int i = 0; i < numCards; i++)
        {
            GameObject newCard = Instantiate(card, stackA.transform.position + new Vector3(1, 0, 0) * i * 0.5f, Quaternion.identity);
            stackA.AddCard(newCard.GetComponent<Card>());
        }

        stackA.ResetPivot();
    }

    private void MoveCard()
    {
        MoveCardToStack(stackA, stackB);
    }

    public void MoveCardToStack(Stack originStack, Stack targetStack)
    {
        if (!targetStack || !originStack) return;

        Card card = originStack.GetTopCard();

        if (card == null) return;

        card.Move(targetStack.pivot);
        card.SetOrderInLayer(targetStack.GetTopOrder());

        countB.text = targetStack.AddCard(card).ToString();
        countA.text = originStack.RemoveCard(card).ToString();
    }
}
