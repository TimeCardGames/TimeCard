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

    void OnMouseOver()
    {
        if (!selected)
        {
            if (Input.GetMouseButtonDown(0))
            {
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
                    GetComponent<Rigidbody2D>().MovePosition(defaultPosition);
                    GetComponent<SpriteRenderer>().sortingOrder = 0;
                }
                else
                {
                    // Card is played
                    if (this.cardType == CardType.Minion)
                    { 
                        minion = new GameObject();
                        minion.name = "Minion";
                        minion.AddComponent<SpriteRenderer>();
                        minion.AddComponent<Rigidbody2D>();
                        minion.AddComponent<BoxCollider2D>();
                        minion.AddComponent<MinionController>();
                        Sprite sprite = Resources.Load<Sprite>("Sprites/Minion");

                        var sr = minion.GetComponent<SpriteRenderer>();
                        sr.sprite = sprite;
                        sr.sortingLayerName = "Foreground";
                        sr.sortingOrder = 1;
                        minion.GetComponent<Rigidbody2D>().isKinematic = true;
                        Vector3 spawnPosition = new Vector3(0, 3, 0);
                        minion.transform.position = spawnPosition;
                        selected = false;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
            
}
