using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearingPlatform : MonoBehaviour
{
    float timeToDisapear = 2;
    [SerializeField]float timeCounter;
    Animator anim;
    [SerializeField]bool disapear;
    bool disapearing;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(disapearing)
        {
            timeCounter += Time.deltaTime;
        }
        if (timeCounter >= timeToDisapear)
        {
            disapear = true;
            disapearing = false;
            anim.SetTrigger("Disapear");
            StartCoroutine(Recover());
            timeCounter = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && disapear == false)
        {
            anim.SetBool("DIsapearing", true);
            disapearing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            disapearing = false;
            timeCounter = 0;
            anim.SetBool("DIsapearing", false);
        }
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(5);
        disapear = false;
        anim.SetTrigger("Recover");
    }
}
