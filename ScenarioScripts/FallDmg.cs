using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDmg : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.PlayerDie();
        }
    }
}
