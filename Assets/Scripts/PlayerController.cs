using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public List<GameObject> hand;

	// Use this for initialization
	void Start () {
        if (hand != null)
        {
            Debug.Log("Not null hand");
            var xVal = -1;
            foreach (var card in hand)
            {
                var cardController = card.GetComponent<CardController>();
                cardController.defaultPosition = new Vector3(xVal, 0.0f, 0.0f);
                xVal += 2;
                Instantiate(card, cardController.defaultPosition, card.transform.rotation);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
