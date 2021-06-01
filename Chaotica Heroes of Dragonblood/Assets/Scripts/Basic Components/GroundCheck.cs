using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{     
    #region Variables
    private bool canJump;
    private bool isGrounded;
    private bool groundOff  =false;
    private float delayGroundenTimer = 0f;

    #endregion

    #region Update
    private void Update() {

        //Simple timer for delaying the grounded effect so that the players can jump briefly when they ran off the ground
        if(groundOff)
        {
            delayGroundenTimer += Time.deltaTime;
            if(delayGroundenTimer >= 0.2f)
            {
                isGrounded = false;
                delayGroundenTimer = 0f;
                groundOff = false;
            }
        }
    }
    #endregion

    #region Trigger Stay - Exit
    private void OnTriggerStay2D(Collider2D other) {
        //If the characters special trigger collider is on collision with the Ground tagged tilemap the player is grounded
        if(other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        
        //Start the ground of timer so that the players can still jump for a little time while in the air
        if(other.gameObject.CompareTag("Ground"))
        {
            groundOff = true;
        }
    }
    #endregion

    #region Getter Functions
    public bool GetGroundState()
    {
        return isGrounded;
    }   
    #endregion
}
