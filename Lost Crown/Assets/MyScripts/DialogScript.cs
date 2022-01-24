using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour
{
    public GameObject keyE;
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    private bool canStart;
    private bool PressE;
    private bool canStartDialogue;
    public GameObject dialogVisibility;
    private void Start()
    {
        canStart = true;
        dialogVisibility.SetActive(true);
    }

    private void Update()
    {
        if (canStartDialogue == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PressE = true;


                if (PressE == true && canStart == true)
                {

                    canStart = false;
                    index = 0;
                    StartCoroutine(Type());
                }
            }
        }


        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true); 
        }
    }
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }

    public void ContinueSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            canStart = true;
            continueButton.SetActive(false);
            index = 0;
        }
    }


     void OnTriggerStay2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player"))
        {
            keyE.SetActive(true);
            canStartDialogue = true;
           
        }
    }
 
    private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                keyE.SetActive(false);
            canStartDialogue = false;
            }
        }

    }

