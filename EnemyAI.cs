using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyType enemyType;
    public float patrolSpeed;
    public List<Transform> patrolPoints = new List<Transform>();
    public int currentPoint = 0;
    Rigidbody2D rb;
    Animator anim;
    float ySpeed = 2;
    [SerializeField] float yTime;
    public GameObject tearPrefab;
    public Transform tearPlace;
    float yCounter;
    [SerializeField] float yFrequency;
    [SerializeField] float yAmplitude;
    public GameObject enemyDmgBox;
    public GameObject player;
    float rayDir;
    [SerializeField] Transform rayPoint;
    RaycastHit2D hit;
    public bool fireCD = false;
    public Transform gunPoint;
    public GameObject gunBullet;
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void FixedUpdate()
    {
        if(!dead)
        {
            if (enemyType == EnemyType.fly)
            {
                ySpeed = Mathf.Sin(Time.time * yFrequency) * yAmplitude;
            }
            if (player.transform.position.x < transform.position.x)
            {
                rayDir = -1;
            }
            else
            {
                rayDir = 1;
            }
            if (enemyType == EnemyType.station)
            {
                hit = Physics2D.Raycast(rayPoint.position, new Vector2(rayDir, 0), 40);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyType)
        {
            case EnemyType.walk:
                if (!dead)
                {
                    if (patrolPoints[currentPoint].position.x > transform.position.x)
                    {
                        transform.localScale = new Vector2(0.5f, 0.5f);
                        if (Vector2.Distance(transform.position, patrolPoints[currentPoint].position) >= 0.5f)
                        {
                            rb.velocity = new Vector2(1 * patrolSpeed, 0);
                        }
                        else
                        {
                            currentPoint++;
                        }

                    }
                    else
                    {
                        transform.localScale = new Vector2(-0.5f, 0.5f);
                        if (Vector2.Distance(transform.position, patrolPoints[currentPoint].position) >= 0.5f)
                        {
                            rb.velocity = new Vector2(-1 * patrolSpeed, 0);
                        }
                        else
                        {
                            currentPoint = 0;
                        }

                    }
                }
                
                break;
            case EnemyType.fly:
                if(!dead)
                {
                    if (yCounter < yTime)
                    {
                        yCounter += Time.deltaTime;
                    }
                    else
                    {
                        ySpeed *= -1;
                        yCounter = 0;
                        DropTear();
                    }
                    if (patrolPoints[currentPoint].position.x > transform.position.x)
                    {
                        transform.localScale = new Vector2(0.5f, 0.5f);
                        if (Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(patrolPoints[currentPoint].position.x, 0)) >= 0.5f)
                        {
                            rb.velocity = new Vector2(1 * patrolSpeed, ySpeed);
                        }
                        else
                        {
                            currentPoint++;
                        }

                    }
                    else
                    {
                        transform.localScale = new Vector2(-0.5f, 0.5f);
                        if (Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(patrolPoints[currentPoint].position.x, 0)) >= 0.5f)
                        {
                            rb.velocity = new Vector2(-1 * patrolSpeed, ySpeed);
                        }
                        else
                        {
                            currentPoint = 0;
                        }

                    }
                }
                break;
            case EnemyType.station:
                if (!dead)
                {
                    RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, new Vector2(rayDir, 0), Mathf.Infinity);
                    float playerDistance = Vector2.Distance(player.transform.position, transform.position);
                    if (hit.collider.CompareTag("Player"))
                    {
                        if (playerDistance > 2 && playerDistance < 10 && fireCD == false)
                        {
                            FireFrog();
                        }
                    }
                    else
                    {

                    }
                }
                break;
        }
    }


    public void DropTear()
    {
        Instantiate(tearPrefab, tearPlace.position, Quaternion.identity);
    }

    public void FireFrog()
    {
        fireCD = true;
        anim.SetTrigger("Shoot");
        GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(FireHide());
    }

    
    IEnumerator FireHide()
    {
        yield return new WaitForSeconds(3);
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetTrigger("Hide");
        yield return new WaitForSeconds(4);
        fireCD = false;
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(gunBullet, gunPoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        Vector2 forceDirection = new Vector2(-5f,0);
        bulletRb.AddForce(forceDirection * 10 , ForceMode2D.Impulse);
    }

    public void TakeDamage()
    {
        dead = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        patrolSpeed = 0;
        anim.SetTrigger("Die");
        enemyDmgBox.SetActive(false);
        if(enemyType == EnemyType.fly)
        {
            rb.gravityScale = 3; 
        }
    }
}

