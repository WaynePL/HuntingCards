using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Palmmedia.ReportGenerator.Core.Common;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEditor.UI;

public class CardSlot : MonoBehaviour
{

    TextMesh text;
    public MeshRenderer descriptionPane, hoverBorder, selectedBorder, damageBorder;
    public CardSlots cardSlots;
    public Card currentCard, cardPrefab;
    public bool isSelected = false;
    public int fontSize;
    public GameObject player;
    public NextTurn nextTurn;
    public int cardPosition;

    void Awake()
    {
        cardSlots = GameObject.Find("Card Slots").GetComponent<CardSlots>();
        player = GameObject.Find("Player");
        nextTurn = GameObject.Find("Next Turn Button").GetComponentInChildren<NextTurn>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cardPosition = (int) (transform.position.x / -6.5f);
        descriptionPane.enabled = false;
        hoverBorder.enabled = false;
        selectedBorder.enabled = false;
        damageBorder.enabled = false;
        
        
        
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
        hoverBorder.enabled = true;
        hoverBorder.material.color = Color.gray;
    }

    void OnMouseExit()
    {
        descriptionPane.enabled = false;
        Destroy(text);
        hoverBorder.enabled = false;
    }

    void OnMouseDown()
    {
        player.transform.position = gameObject.transform.position + new Vector3(0, -13, 0);
        player.GetComponent<Player>().currentPosition =  (int) (gameObject.transform.position.x / -6.5f);
        selectedBorder.enabled = true;
        selectedBorder.material.color = Color.black;
        isSelected = true;
        nextTurn.currentMonsterDamage = currentCard.damage;
        nextTurn.cardSelected = true;
        nextTurn.selectedCardSlot = this;
    }

    public void DeselectCard()
    {
        isSelected = false;
        selectedBorder.enabled = false;
    }

    public void SetCard(Card card)
    {
        cardPrefab = card;
        currentCard = GameObject.Instantiate(cardPrefab);
        currentCard.gameObject.SetActive(true);
        currentCard.transform.parent = transform.GetChild(4).GetChild(0);
        currentCard.transform.localPosition = new Vector3(0, 0, -10);
        currentCard.transform.localScale = new Vector3(0.9f, 0.9f, 1);
    }

    

    string getDescription()
    {
        return currentCard.description;
    }

}


