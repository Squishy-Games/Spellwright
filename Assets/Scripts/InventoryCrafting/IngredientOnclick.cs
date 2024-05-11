using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientOnclick : MonoBehaviour
{
    public string name;
    public inventoryHolder Inventory;
    public GameObject Ingredient;
    public bool IsOver = false;
    // Start is called before the first frame update
    void Start()
    {
        Inventory.AddToIngredient(name, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOver == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            instantiateIngredient();
        }
    }
    public void instantiateIngredient()
    {
        if(Inventory.Inventory[name] > 0){
            Instantiate(Ingredient);
            Inventory.EjectFromInventory(name, 1);
            Debug.Log("AmountstillThere" + Inventory.Inventory[name]);
        }

    }
    void OnMouseOver()
    {
        IsOver = true;
        Debug.Log("iingredienteOver " + IsOver);
    }
    private void OnMouseExit() {
        IsOver = false;
    }
}   
