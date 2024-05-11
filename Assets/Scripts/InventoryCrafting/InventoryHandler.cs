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
        load.Load();
        InventoryData.AddToIngredient("Hello", 1);
        InventoryData.AddToIngredient("Hello", 3);
        InventoryData.EjectFromInventory("Hello", 2);
        amount = InventoryData.RequestAmount("Hello");
        load.names = InventoryData.Obtainableitems;

        InventoryData.AddToIngredient("Mauro", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
