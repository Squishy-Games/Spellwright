using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryLoadUI : MonoBehaviour
{
    public float distance;
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
            int quantity = Inventory.RequestAmount(names[counter]);
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
        Ingrediente.transform.position = new Vector3(Ingrediente.transform.position.x,Ingrediente.transform.position.y - distance * counter + 1,Ingrediente.transform.position.z);
        Ingrediente.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = names[counter];
        Ingrediente.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = ""+Amount;
    }
}
