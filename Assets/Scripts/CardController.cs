using UnityEngine;
using System.Collections;

public class CardController : MonoBehaviour {

    public Camera cam;
    public bool selected = false;
    public Vector3 defaultPosition = new Vector3(0.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start ()
    {
	    if (cam == null)
        {
            cam = Camera.main;
        }
	}
	
	// FixedUpdate is called once per physics Timestep
	void FixedUpdate ()
    {
        if (selected)
        {
            Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPosition = new Vector3(rawPosition.x, rawPosition.y, 0.0f);
            var rigidbody2d = GetComponent<Rigidbody2D>();
            rigidbody2d.MovePosition(targetPosition);
        }        
	}

    void OnMouseOver()
    {
        if (!selected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                selected = true;
            }
        }
        else
        {
            var border = GameObject.Find("Border");
            var mouseY = cam.ScreenToWorldPoint(Input.mousePosition).y;
            var borderY = border.transform.position.y;

            if (Input.GetMouseButtonDown(0) && mouseY < borderY)
            {
                selected = false;
                GetComponent<Rigidbody2D>().MovePosition(defaultPosition);
            }
        }            
    }
            
}
