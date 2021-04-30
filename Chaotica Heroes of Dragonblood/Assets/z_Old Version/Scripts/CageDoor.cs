using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageDoor : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private bool upDoor;
    // Start is called before the first frame update
    private void Awake() {
        spriteRenderer = transform.Find("Cage Door").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag("Player"))
        {
            if(upDoor)
                spriteRenderer.sortingOrder = 3;
            else
                spriteRenderer.sortingOrder = 1;
            Debug.Log("sa");
        }   
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(upDoor)
            spriteRenderer.sortingOrder = 1;
            else
                spriteRenderer.sortingOrder = 3;
        }   
    }
}
