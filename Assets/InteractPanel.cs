using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPanel : MonoBehaviour
{
    public GameObject interactPrefab;
    public List<GameObject> interactions;
    // Start is called before the first frame update
    public void setInteractions(List<Interactions> targetInteractions)
    {
        for (int i = 0; i < interactions.Count; i++)
        {
            if (i < targetInteractions.Count)
            {
                string param = targetInteractions[i].ToString();
                interactions[i].GetComponent<Button>().onClick.AddListener(() => ActivateInteraction(param));
                interactions[i].GetComponentInChildren<Text>().text = param;
                interactions[i].SetActive(true);
            }
            else
            {
                interactions[i].SetActive(false);
            }
        }
    }

    public void ActivateInteraction(string interaction)
    {
        switch (interaction)
        {
            case "ATTACK":
                Debug.Log("ATTACK");
                break;
            case "TALK":
                Debug.Log("TALK");
                break;
            case "CANCEL":
                Debug.Log("CANCEL");
                break;
            default:
                Debug.Log("DEFAULT");
                break;
        }
    }
}
