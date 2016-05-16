using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour {

    public Camera cam;
    public Vector3 targetPosition;
    public int attack;
    public int health;
    public float speed = 2.0f;
    public bool selected;
    public GameObject attackText;
    public GameObject healthText;

    private bool buttonDown;

    // Use this for initialization
    void Start ()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        this.targetPosition = this.transform.position;

        var sr = this.GetComponent<SpriteRenderer>();
        sr.sortingLayerName = "Foreground";
        sr.sortingOrder = 1;
        Vector3 spawnPosition = new Vector3(0, 3, 0);
        transform.position = spawnPosition;
        selected = false;

        var currPos = gameObject.transform.position;

        // Initialize Text
        attackText.GetComponent<TextMesh>().text = attack.ToString();
        attackText.GetComponent<MeshRenderer>().sortingLayerName = "Foreground";
        attackText.GetComponent<MeshRenderer>().sortingOrder = 2;
        attackText.transform.position = new Vector3(currPos.x - 0.5f, currPos.y - 0.5f, currPos.z);
        attackText = (GameObject)Instantiate(attackText, attackText.transform.position, attackText.transform.rotation);

        healthText.GetComponent<TextMesh>().text = health.ToString();
        healthText.GetComponent<MeshRenderer>().sortingLayerName = "Foreground";
        healthText.GetComponent<MeshRenderer>().sortingOrder = 2;
        healthText.transform.position = new Vector3(currPos.x + 0.5f, currPos.y - 0.5f, currPos.z);
        healthText = (GameObject)Instantiate(healthText, healthText.transform.position, healthText.transform.rotation);
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
	
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Minion")
            this.targetPosition = this.transform.position;
    }

	// FixedUpdate is called once per physics timestep
	void FixedUpdate ()
    {
        var rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed));
        MoveText();
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

    void MoveText()
    {
        var currPos = GetComponent<Rigidbody2D>().transform.position;
        attackText.transform.position = new Vector3(currPos.x - 0.5f, currPos.y - 0.5f, currPos.z);
        healthText.transform.position = new Vector3(currPos.x + 0.5f, currPos.y - 0.5f, currPos.z);
    }
}
