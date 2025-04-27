using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] private List<Card> cards = new List<Card>();

    public Vector3 pivot;

    private void Start()
    {
        ResetPivot();
    }

    public void ResetPivot()
    {
        pivot = transform.position + new Vector3(1, 0, 0) * (cards.Count + 0.5f);
    }

    public int AddCard(Card card)
    {
        cards.Add(card);
        pivot.x += 0.5f;

        return cards.Count;
    }
    public int RemoveCard(Card card)
    {
        cards.Remove(card);
        pivot.x -= 0.5f;

        return cards.Count;
    }

    public Card GetTopCard()
    {
        return cards.Count > 0 ? cards[cards.Count - 1] : null;
    }
    public int GetTopOrder()
    {
        return cards.Count > 0 ? cards.Count : 0;
    }
}
