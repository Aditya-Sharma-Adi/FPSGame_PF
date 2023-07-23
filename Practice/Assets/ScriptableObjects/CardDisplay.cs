using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    public Card playerCard;

    // Start is called before the first frame update
    void Start()
    {
        playerCard.DisplayData();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
