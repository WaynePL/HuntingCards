using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSlots : MonoBehaviour
{

    public List<CardSlot> cardSlots = new List<CardSlot>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeselectCards()
    {
        cardSlots = GetComponentsInChildren<CardSlot>().ToList();
        foreach ( CardSlot cardSlot in cardSlots )
        {
            cardSlot.DeselectCard();
        }
    }
}
