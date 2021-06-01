using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponentManager : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer itemSprite;
    [SerializeField]
    private Rigidbody2D itemRigidBody;
    [SerializeField]
    private BoxCollider2D boxCollider;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float playerTransformDistanceToPickUp = 1.25f;

    private bool isPickable = false;

    // Start is called before the first frame update
    void Start()
    {
        itemSprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        itemRigidBody = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        //Calculate the distance between player and object and decide if the player is close enough to pick up that object
    public void PlayerDistance()
    {
        //Debug.Log((playerTransform.position - transform.position).sqrMagnitude);
        if((playerTransform.position - transform.position).sqrMagnitude < playerTransformDistanceToPickUp * playerTransformDistanceToPickUp)
        {
            Debug.Log("Press E to pick up");
            isPickable = true;
        }
    }

    //Deactivate components that can create issues when picking up an object, setting new parent and resettin position just for being safe
    public void PlayerPickUpObject(string itemTypeParent)
    {
            if(Input.GetKeyDown(KeyCode.E) && isPickable)
            {
                Debug.Log("Pressed");
                if(itemTypeParent == "Consumables")
                    playerTransform.Find("Inventory").gameObject.GetComponent<InventoryComponent>().AddConsumableToInventory(GetComponent<ConsumableComponent>());
                else
                    playerTransform.Find("Inventory").gameObject.GetComponent<InventoryComponent>().AddWeaponToInventory(GetComponent<WeaponComponent>());
                transform.SetParent(playerTransform.transform.Find("Inventory").transform.Find(itemTypeParent).transform);
                itemSprite.enabled = false;
                boxCollider.enabled = false;
                Destroy(itemRigidBody);
                transform.localPosition = new Vector2(0f,0f);
                isPickable = false;
            }
    }
}
