using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearDrop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
        else if(collision.CompareTag("Player"))
        {
            GameManager.Instance.PlayerDie();
            collision.GetComponent<PlayerController>().anim.SetTrigger("Dead");
        }
    }
}
