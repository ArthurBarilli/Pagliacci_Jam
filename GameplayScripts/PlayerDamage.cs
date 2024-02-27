using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public ParticleSystem bonk;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyAI>().TakeDamage();
            bonk.Play();
            GameManager.Instance.Yay();
        }

    }
}
