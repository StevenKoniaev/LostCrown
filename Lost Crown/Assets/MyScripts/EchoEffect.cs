using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EchoEffect : MonoBehaviour
{
    public float timeBetweenSpawns;
    public float startBetweenSpawns;
    public GameObject echo;
    public Rigidbody2D rb;
    bool isDashing;
    private void Update()
    {
        if (isDashing == true)
        {
            if (timeBetweenSpawns <= 0)
            {
                //spawn echhhooo
                GameObject Instance = (GameObject)Instantiate(echo, transform.position, Quaternion.identity);
                if (rb.velocity.x <= 0.01f)
                {
                    echo.transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else if (rb.velocity.x >= 0.01f)
                {
                    echo.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                Destroy(Instance, .45f);
                timeBetweenSpawns = startBetweenSpawns;
            }
            else
            {
                timeBetweenSpawns -= Time.deltaTime;
            }
        }
    }

   public void Dashing(bool isTrue)
    {
        isDashing = isTrue;
    }
}
