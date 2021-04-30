using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    #region Unity Editor Seen Variables
    [SerializeField]
    private WeaponObject weaponObject;
    [SerializeField]
    private SpriteRenderer weaponSprite;
    [SerializeField]
    private CapsuleCollider2D capsuleCollider;
    [SerializeField]
    private Rigidbody2D weaponRigidBody;
    [SerializeField]
    private BoxCollider2D boxCollider;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float playerTransformDistanceToPickUp;
    #endregion

    #region Variables
    private bool isPickable = false;
    #endregion

    #region Awake - Update
    private void Awake() {
        weaponSprite = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        weaponRigidBody = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("Player").transform;
    }
    private void Update() 
    {
        if(transform.parent != GameObject.Find("Weapons").transform)
        {
            PlayerDistance();
            PlayerPickUpObject();
        }
    }
    #endregion

    #region Getter Functions
    public WeaponObject GetWeaponObjectDetails()
    {
        return weaponObject;
    }
    #endregion

    #region Player - Distance - PickUpObject

    //Calculate the distance between player and object and decide if the player is close enough to pick up that object
    private void PlayerDistance()
    {
        //Debug.Log((playerTransform.position - transform.position).sqrMagnitude);
        if((playerTransform.position - transform.position).sqrMagnitude < playerTransformDistanceToPickUp)
        {
            Debug.Log("Press E to pick up");
            isPickable = true;
        }
    }

    //Deactivate components that can create issues when picking up an object, setting new parent and resettin position just for being safe
    private void PlayerPickUpObject()
    {
            if(Input.GetKeyDown(KeyCode.E) && isPickable)
            {
                Debug.Log("Pressed");
                playerTransform.Find("Inventory").gameObject.GetComponent<InventoryComponent>().AddWeaponToInventory(this);
                transform.SetParent(playerTransform.transform.Find("Inventory").transform.Find("Weapons").transform);
                weaponSprite.enabled = false;
                capsuleCollider.enabled = false;
                boxCollider.enabled = false;
                Destroy(weaponRigidBody);
                transform.localPosition = new Vector2(0f,0f);
                isPickable = false;
            }
    }
    #endregion

}
