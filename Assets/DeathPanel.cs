using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : MonoBehaviour
{
    public GameObject deathPanel;
    public void activatePanel()
    {
        deathPanel.SetActive(true);
    }

    public void deactivatePanel()
    {
        deathPanel.SetActive(false);
    }
}
