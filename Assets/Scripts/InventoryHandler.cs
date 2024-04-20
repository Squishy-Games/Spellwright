using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{
    public inventoryHolder InventoryData;
    // Start is called before the first frame update
    void Start()
    {
        InventoryData.AddToInventory("Hello", 1);
        InventoryData.AddToIngredient("Hello", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
