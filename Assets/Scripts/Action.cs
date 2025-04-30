using System;
using UnityEngine;

public class Action : BaseAction
{
    public int damage = 0;
    public int heal = 0;
    public int quantity = 0;
    public NextTurn nextTurn;
    public GameObject topText, bottomText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextTurn = GameObject.FindWithTag("NextTurn").GetComponentInChildren<NextTurn>();

        if (name == "Potion")
        {
            topText = new("HealText");
            topText.transform.parent = gameObject.transform;
            topText.AddComponent<MeshRenderer>();
            topText.AddComponent<TextMesh>();
            topText.GetComponent<TextMesh>().text = "Heal: " + heal.ToString();
            topText.GetComponent<TextMesh>().color = Color.black;
            topText.GetComponent<TextMesh>().fontSize = 30;
            topText.transform.localScale *= 0.2f;
            topText.transform.localPosition = new Vector3(-0.45f, 0.45f, 0f);

            bottomText = new("QuantityText");
            bottomText.transform.parent = gameObject.transform;
            bottomText.AddComponent<MeshRenderer>();
            bottomText.AddComponent<TextMesh>();
            bottomText.GetComponent<TextMesh>().text = "Quantity: " + quantity.ToString();
            bottomText.GetComponent<TextMesh>().color = Color.black;
            bottomText.GetComponent<TextMesh>().fontSize = 30;
            bottomText.transform.localScale *= 0.2f;
            bottomText.transform.localPosition = new Vector3(-0.45f, -0.4f, 0f);
        }
        else
        {
            topText = new("DamageText");
            topText.transform.parent = gameObject.transform;
            topText.AddComponent<MeshRenderer>();
            topText.AddComponent<TextMesh>();
            topText.GetComponent<TextMesh>().text = "Damage: " + damage.ToString();
            topText.GetComponent<TextMesh>().color = Color.black;
            topText.GetComponent<TextMesh>().fontSize = 30;
            topText.transform.localScale *= 0.2f;
            topText.transform.localPosition = new Vector3(-0.45f, 0.45f, 0f);

            bottomText = new("StaminaText");
            bottomText.transform.parent = gameObject.transform;
            bottomText.AddComponent<MeshRenderer>();
            bottomText.AddComponent<TextMesh>();
            bottomText.GetComponent<TextMesh>().text = "Stamina: " + staminaCost.ToString();
            bottomText.GetComponent<TextMesh>().color = Color.black;
            bottomText.GetComponent<TextMesh>().fontSize = 30;
            bottomText.transform.localScale *= 0.2f;
            bottomText.transform.localPosition = new Vector3(-0.45f, -0.4f, 0f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if (nextTurn.player.playerStamina.curStamina >= staminaCost)
        {
            if (nextTurn.cardSelected || nextTurn.actionSelected) 
            {
                if (nextTurn.actionSelected && nextTurn.selectedAction != this)
                {
                    nextTurn.selectedAction.DeselectAction();
                    nextTurn.ActionSelected(this);
                    transform.position = new Vector3(transform.position.x, -15, transform.position.z);
                }
                else
                {
                    nextTurn.ActionSelected(null);
                    DeselectAction();
                }
                if(nextTurn.selectedCard != null) 
                {
                    nextTurn.CardSelected(null);
                    nextTurn.ActionSelected(this);
                    transform.position = new Vector3(transform.position.x, -15, transform.position.z);
                }
            }
            else
            {
                nextTurn.ActionSelected(this);
                transform.position = new Vector3(transform.position.x, -15, transform.position.z);
            }
        }
    }

    public void DeselectAction()
    {
        transform.position = new Vector3(transform.position.x, -17, transform.position.z);
        nextTurn.UnsetAction();
    }

    public void UpdateQuantity()
    {
        bottomText.GetComponent<TextMesh>().text = "Quantity: " + quantity.ToString();
    }
}
