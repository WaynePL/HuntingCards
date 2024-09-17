using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Palmmedia.ReportGenerator.Core.Common;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;

public class CardSlot : MonoBehaviour
{

    TextMesh text;
    public MeshRenderer descriptionPane, highlightBorder;
    public CardSlots cardSlots;
    public Card currentCard;
    public bool isSelected = false;
    public int fontSize;
    public GameObject player;
    public NextTurn nextTurn;
    // Start is called before the first frame update
    void Start()
    {
        descriptionPane.enabled = false;
        highlightBorder.enabled = false;
        player = GameObject.Find("Player");
        cardSlots = GameObject.Find("Card Slots").GetComponent<CardSlots>();
        nextTurn = GameObject.Find("Next Turn Button").GetComponentInChildren<NextTurn>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        text = descriptionPane.transform.GetChild(0).AddComponent<TextMesh>();
        text.fontSize = fontSize;
        text.color = Color.black;
        text.text = getDescription();
        descriptionPane.enabled = true;
        highlightBorder.enabled = true;
        highlightBorder.material.color = Color.gray;
    }

    void OnMouseExit()
    {
        descriptionPane.enabled = false;
        Destroy(text);
        if (!isSelected)
        {
            highlightBorder.enabled = false;
        }
    }

    void OnMouseDown()
    {
        cardSlots.DeselectCards(); 
        player.transform.position = gameObject.transform.position + new Vector3(0, -13, 0);
        highlightBorder.enabled = true;
        highlightBorder.material.color = Color.black;
        isSelected = true;
        nextTurn.currentMonsterDamage = currentCard.damage;
        nextTurn.cardSelected = true;
    }

    public void DeselectCard()
    {
        isSelected = false;
        highlightBorder.enabled = false;
    }

    public void SetCard(Card card)
    {
        currentCard = card;
        currentCard = GameObject.Instantiate(currentCard);
        currentCard.transform.parent = transform.GetChild(2);
        currentCard.transform.localPosition = new Vector3(0, 0, -10);
        currentCard.transform.localScale = new Vector3(0.9f, 0.9f, 1);
    }

    string getDescription()
    {
        return currentCard.description;
    }

}


