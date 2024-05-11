using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryLoadUI : MonoBehaviour
{
    public Vector3 distance;
    public GameObject Ingredient;
    public GameObject content;
    public inventoryHolder Inventory;
    public int counter;
    public int plankCounter;
    public int itemCounter;

    public List<string> names = new List<string>();
    public List<Vector4> ItemPlacement;

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
            itemCounter ++;
            if (itemCounter == ItemPlacement[plankCounter].w){
                itemCounter ++;
                plankCounter ++;
            }
        }
        counter = 0;
        itemCounter = 0;
        plankCounter = 0;
    }
    public void SpawnIn(int Amount)
    {
        Debug.Log("came here");
        GameObject Ingrediente = Instantiate(Ingredient, content.transform.GetChild(plankCounter).transform);
        Ingrediente.transform.position = new Vector3(ItemPlacement[plankCounter].x + distance.x * counter + 1, ItemPlacement[plankCounter].y + distance.y * counter + 1,ItemPlacement[plankCounter].z + distance.z * counter + 1);
        //Ingrediente.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = names[counter];
        Ingrediente.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = ""+Amount;
    }
}
