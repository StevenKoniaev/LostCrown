using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StrongWizardTeleport : MonoBehaviour
{
    public Transform wizardTeleport;
    public Transform wizard;
    private bool teleport = true;
    // Update is called once per frame
    void Update()
    {
        if (wizard.GetComponent<Enem>().currentHealth <= 300 && teleport == true)
        {
            teleport = false;
            wizard.position = wizardTeleport.position;
        }

        if (wizard.GetComponent<Enem>().currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1f;
        }
    }
}
