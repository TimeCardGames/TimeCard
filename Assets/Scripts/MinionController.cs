using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour {

    public Camera cam;
    public Vector3 targetPosition;
    public float speed = 2.0f;
    public bool selected;
    private bool buttonDown;

	// Use this for initialization
	void Start ()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        this.targetPosition = this.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            buttonDown = false;
        }

        if (!buttonDown)
        {
            if (selected && Input.GetMouseButtonDown(0))
            {
                Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                this.targetPosition = new Vector3(rawPosition.x, rawPosition.y, 0.0f);
                selected = false;
                var sr = GetComponent<SpriteRenderer>();
                sr.color = new Color(1, 1, 1, 1);
            }
        }        
    }
	
	// FixedUpdate is called once per physics timestep
	void FixedUpdate ()
    {
        
        var rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed));
    }

    void OnMouseOver()
    {
        if (!selected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                selected = true;
                buttonDown = true;
                var sr = GetComponent<SpriteRenderer>();
                sr.color = new Color(0.5f, 0.5f, 0.5f, 1);
                GetComponent<SpriteRenderer>().sortingOrder = 5;
            }
        }
    }
}
