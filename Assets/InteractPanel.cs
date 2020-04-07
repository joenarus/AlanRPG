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
        removeAllButtons();
        for (int i = 0; i < targetInteractions.Count; i++)
        {
            string param = targetInteractions[i].ToString();
            var interactButton = Instantiate(interactPrefab, transform);
            interactButton.GetComponent<Button>().onClick.AddListener(() => ActivateInteraction(param));
            interactButton.GetComponentInChildren<Text>().text = param;
            interactions.Add(interactButton);
        }
    }

    void removeAllButtons()
    {
        for (int i = interactions.Count - 1; i >= 0; i--)
        {
            Destroy(interactions[i]);
            interactions.RemoveAt(i);
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
                gameObject.SetActive(false);
                break;
            default:
                Debug.Log("DEFAULT");
                break;
        }
    }
}
