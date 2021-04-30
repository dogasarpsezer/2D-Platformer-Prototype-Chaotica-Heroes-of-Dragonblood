using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0f,10f)]
    private float playerSpeed = 10f;

    [SerializeField]
    private Animator playerAnim;

    private Vector2 horVerMove;
    private void Awake() 
    {
        playerAnim = GetComponent<Animator>();
    }
    void Start()
    {
    }

    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        horVerMove.x = Input.GetAxisRaw("Horizontal");
        horVerMove.y = Input.GetAxisRaw("Vertical");
        PlayerMoveAnim();

        transform.position = new Vector2(transform.position.x+horVerMove.x *playerSpeed*Time.deltaTime,transform.position.y + horVerMove.y*playerSpeed*Time.deltaTime);
    }
    void PlayerMoveAnim()
    {
        if(horVerMove.x == 1 || horVerMove.x == -1 || horVerMove.y == 1 || horVerMove.y == -1)
        {
            playerAnim.SetFloat("Horizontal_Idle",horVerMove.x);
            playerAnim.SetFloat("Vertical_Idle",horVerMove.y);
        }

        if(horVerMove.sqrMagnitude < 0.1f)
        {
            playerAnim.SetBool("Run",false);
        }
        else
        {
            playerAnim.SetBool("Run",true);
        }
        playerAnim.SetFloat("Horizontal",horVerMove.x);
        playerAnim.SetFloat("Vertical",horVerMove.y);
    }
}
