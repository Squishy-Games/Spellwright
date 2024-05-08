using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLoadUI : MonoBehaviour
{
    public GameObject Ingredient;
    public GameObject content;
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
        while(counter < Inventory.Inventory.Count)
        {
            int quantity = Inventory.RequestAmount(names[counter - 1]);
            Debug.Log(quantity);
            SpawnIn(quantity);
            counter ++;
        }
        counter = 0;
    }
    public void SpawnIn(int Amount)
    {
        Debug.Log("came here");
        GameObject Ingrediente = Instantiate(Ingredient, content.transform);
    }
}
