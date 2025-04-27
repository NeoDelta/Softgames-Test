using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    private void Start()
    {
        if (!sprite) sprite = GetComponent<SpriteRenderer>();
    }

    public void Move(Vector3 targetPosition)
    {
        transform.DOMove(targetPosition, 0.75f);
    }

    public void SetOrderInLayer(int order)
    {
        sprite.sortingOrder = order;
    }
}
