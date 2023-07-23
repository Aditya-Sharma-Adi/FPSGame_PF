using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuCard", menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    public int health;
    public int cost;

    public void DisplayData()
    {
        Debug.Log("Name : " + name);
    }

}
