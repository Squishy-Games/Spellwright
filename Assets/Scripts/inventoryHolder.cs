using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
[CreateAssetMenu]
public class inventoryHolder : ScriptableObject
{
    public Dictionary<string, int> Inventory = new Dictionary<string, int>();
    public List<string> Obtainableitems;
    public void AddToInventory(string name, int Amount)
    {
        Inventory.Add(name, Amount);
    }
    public void EjectFromInventory(string name, int amountToEject)
    {
        Inventory[name] = Inventory[name] - amountToEject;
    }
    public void AddToIngredient(string name, int amountToAdd)
    {
        Inventory[name] = Inventory[name] + amountToAdd;
        Debug.Log(Inventory[name]);
    }
}
