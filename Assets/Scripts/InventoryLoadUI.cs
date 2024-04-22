using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLoadUI : MonoBehaviour
{
    public inventoryHolder Inventory;
    public int counter;
    public List<string> names = new List<string>();
    // Start is called before the first frame update
    void Start()
    {


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
        counter = 0;
    }
    public void SpawnIn()
    {

    }
}
