using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public PlayerController pMove;
    public bool isCoyote;
    public float coyoteTime;
    public float coyoteCounter;

    private void Update()
    {
        if(isCoyote)
        {
            coyoteCounter += Time.deltaTime;
        }
        else
        {
            coyoteCounter = 0;
        }

        if (coyoteCounter >= coyoteTime)
        {
            pMove.status = PlayerStatus.onAir;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isCoyote = false;
            pMove.status = PlayerStatus.grounded;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isCoyote = false;
            pMove.status = PlayerStatus.grounded;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isCoyote = true;
        }
    }
}
