using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLoadUI : MonoBehaviour
{
    inventoryHolder Inventory;
    public int counter;
    List<string> names = new List<string>();
    // Start is called before the first frame update
    void Start()
    {

 //       names = Inventory.Obtainableitems;
    }

    // Update is called once per frame
    public void Load()
    {
        while(counter < names.Count)
        {
            int quantity = Inventory.RequestAmount(names[counter]);
            Debug.Log(quantity);
            counter ++;
        }
    }
}
