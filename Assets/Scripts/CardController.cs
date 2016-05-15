using UnityEngine;
using System.Collections;

public class CardController : MonoBehaviour {

    public Camera cam;
    public bool selected = false;
    public Vector3 defaultPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public CardType cardType;
    public GameObject minion;

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

    void Update()
    {

    }

    void OnMouseEnter()
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(0.75f, 0.75f, 0.75f, 1);
    }

    void OnMouseExit()
    {
        if (!selected)
        {
            var sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(1.0f, 1.0f, 1.0f, 1);
        }
    }

    void OnMouseOver()
    {
        if (!selected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var sr = GetComponent<SpriteRenderer>();
                sr.color = new Color(0.5f, 0.5f, 0.5f, 1);
                selected = true;
                GetComponent<SpriteRenderer>().sortingOrder = 5;
            }
        }
        else
        {
            var border = GameObject.Find("Border");
            var mouseY = cam.ScreenToWorldPoint(Input.mousePosition).y;
            var borderY = border.transform.position.y;

            if (Input.GetMouseButtonDown(0))
            {
                if (mouseY < borderY)
                {
                    selected = false;
                    var sr = GetComponent<SpriteRenderer>();
                    sr.color = new Color(0.75f, 0.75f, 0.75f, 1);
                    GetComponent<Rigidbody2D>().MovePosition(defaultPosition);
                    GetComponent<SpriteRenderer>().sortingOrder = 0;
                }
                else
                {
                    // Card is played
                    if (this.cardType == CardType.Minion)
                    {
                        Instantiate(minion, transform.position, transform.rotation);

                        Destroy(gameObject);
                    }
                }
            }
        }
    }
            
}
