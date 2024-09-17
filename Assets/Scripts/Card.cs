using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string description;
    public int damage;

    void Start()
    {
        transform.GetChild(1).GetComponent<TextMesh>().text = damage.ToString();
        transform.GetChild(2).GetComponent<TextMesh>().text = gameObject.name;
    }   
    
}
