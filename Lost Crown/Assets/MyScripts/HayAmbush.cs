using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayAmbush : MonoBehaviour
{
    public GameObject enemyOne;
    public GameObject enemyTwo;
    public GameObject Getem;
    public GameObject hayambush;
    private void OnTriggerEnter2D(Collider2D collision)
    {
   
            Getem.SetActive(true);
        
        enemyOne.SetActive(true);
        enemyTwo.SetActive(true);
       
        StartCoroutine(ResourceTickOver(.5f));
    }


    IEnumerator ResourceTickOver(float waitTime)
    {
       
            yield return new WaitForSeconds(waitTime);
        Getem.SetActive(false);
        hayambush.SetActive(false);
        this.enabled = false;
        

        

    }
}
