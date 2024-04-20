using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public int amount;
    public inventoryHolder InventoryData;
   public InventoryLoadUI load; 
    // Start is called before the first frame update
    void Start()
    {
        InventoryData.AdministratingItems();
        InventoryData.AddToInventory("Hello", 1);
        InventoryData.AddToIngredient("Hello", 3);
        InventoryData.EjectFromInventory("Hello", 2);
        amount = InventoryData.RequestAmount("Hello");
        load.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
