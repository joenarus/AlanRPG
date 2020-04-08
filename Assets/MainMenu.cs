using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject LoadMenu;
    
    public void activateMain(){
        mainMenu.SetActive(true);
        LoadMenu.SetActive(false);
    }

    public void activateLoad(){
        LoadMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
}
