using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletLifeTime;
    float bulletLifeCounter;
    [SerializeField]ParticleSystem particleConffeti;

    private void Update()
    {
        if(bulletLifeCounter < bulletLifeTime)
        {
            bulletLifeCounter += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().dead = true;
            collision.GetComponent<PlayerController>().anim.SetTrigger("Dead");
            GameManager.Instance.PlayerDie();   
        }
        else if(collision.CompareTag("Ground"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            rb.freezeRotation = true;
            GetComponent<BoxCollider2D>().enabled = false;
            particleConffeti.Play();
        }
    }
}
