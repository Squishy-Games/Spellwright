using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
[CreateAssetMenu]
public class inventoryHolder : ScriptableObject
{
    public int counter = 0;
    public Dictionary<string, int> Inventory = new Dictionary<string, int>();
    public List<string> Obtainableitems;
    public void AdministratingItems()
    {
        Debug.Log("Hello");
        while(counter < Obtainableitems.Count)
        {
            Debug.Log("HelloAgain");
            AddToInventory(Obtainableitems[counter], 0);
            Debug.Log(Obtainableitems[counter] + Inventory[Obtainableitems[counter]]);
            counter ++;

        }
        counter = 0;
    }
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
        if(Inventory.TryAdd(name, amountToAdd) == false)
        {
            AddToInventory(name, amountToAdd);
        }
        Inventory[name] = Inventory[name] + amountToAdd;
        Debug.Log(Inventory[name]);
    }
    public int RequestAmount(string name)
    {
        return Inventory[name];
    }
}
