using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] EnemyAI enemyAi;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !enemyAi.dead)
        {
            collision.GetComponent<PlayerController>().anim.SetTrigger("Dead");
            GameManager.Instance.PlayerDie();
        }
    }
}
