using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class drag : MonoBehaviour
{
    public Vector3 lastMousePosition;
    public bool IsDragging = true;
    public bool isOver;
    public float launchspeed = 2;
    public int framesInBetween;
    public Dictionary<int,Vector3> LastPositions;
    public int PositionIndex; 
    // Update is called once per frame
     void FixedUpdate() 
    {
        StartCoroutine(wait());

        bool check = IsDragging;

        if(Input.GetKeyDown(KeyCode.Mouse0) && isOver == true)
        {
            IsDragging = true;
        }

        if(Input.GetKey(KeyCode.Mouse0) == true && IsDragging == true)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 8);
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        }
        else if (IsDragging == true){
            IsDragging = false;
            Debug.Log(IsDragging);
            transform.GetComponent<Rigidbody2D>().gravityScale = 1;
            float frame = 0.01666666666f;
            transform.GetComponent<Rigidbody2D>().velocity = new Vector3((Input.mousePosition.x - lastMousePosition.x) * frame * launchspeed,(Input.mousePosition.y - lastMousePosition.y) * frame * launchspeed, (Input.mousePosition.z + lastMousePosition.z) * frame * launchspeed);
            Debug.Log("speed" + new Vector3((Input.mousePosition.x + lastMousePosition.x) * frame,(Input.mousePosition.y + lastMousePosition.y) * frame, (Input.mousePosition.z + lastMousePosition.z) * frame));
        }
    }
    public IEnumerator<WaitForEndOfFrame> wait(){
        yield return new WaitForEndOfFrame();
        lastMousePosition = Input.mousePosition;
    }
    
    void OnMouseOver()
    {
        isOver = true;
    }
    private void OnMouseExit() {
        isOver = false;
    }
}
