using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossDialogScript : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] darray;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    private bool canStart;
    public bool canStartDialogue;
    public GameObject dialogVisibility;
    public BossKingScript inRangeScript;
    public GameObject teleporter;
    public GameObject bosshealthbar;
    public Rigidbody2D playerrb;
    private void Start()
    {
        canStart = true;
        dialogVisibility.SetActive(true);
    }

    private void Update()
    {
        if (canStartDialogue == true)
        {
                if ( canStart == true)
                {
                playerrb.constraints = RigidbodyConstraints2D.FreezeAll;
                    canStart = false;
                    index = 0;
                    StartCoroutine(Type());
                }
            }
        


        if (textDisplay.text == darray[index])
        {
            continueButton.SetActive(true);
        }
    }
    IEnumerator Type()
    {
        foreach (char letter in darray[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public void ContinueSentence()
    {
        continueButton.SetActive(false);

        if (index < darray.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            playerrb.constraints = RigidbodyConstraints2D.None;
            playerrb.constraints = RigidbodyConstraints2D.FreezeRotation;
            canStart = true;
            continueButton.SetActive(false);
            index = 0;
            inRangeScript.GetComponent<BossKingScript>().inRange = true;
            bosshealthbar.SetActive(true);
            teleporter.SetActive(true);
            Time.timeScale = 1f;
            this.enabled = false;
            
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canStartDialogue = false;
            this.enabled = false;
        }
    }

}
