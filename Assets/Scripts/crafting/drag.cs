using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class drag : MonoBehaviour
{
    public Vector3 lastMousePosition;
    public bool IsDragging = true;
    public bool isOver;
    // Update is called once per frame
    void Update()
    {
        bool check = IsDragging;
        IsDragging = (Input.GetKey(KeyCode.Mouse0) == true && isOver == true) ? true : false;
        if(IsDragging == true)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 8);
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        }
        else {
            transform.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
    void OnMouseOver()
    {
        isOver = true;
    }
    private void OnMouseExit() {
        isOver = false;
    }
}
