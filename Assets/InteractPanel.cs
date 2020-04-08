using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPanel : MonoBehaviour
{
    public GameObject interactPrefab;
    private Interactable target;
    public List<GameObject> interactions;
    // Start is called before the first frame update
    public void setInteractions(List<Interactions> targetInteractions, Interactable interactionTarget)
    {
        target = interactionTarget;
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
                PlayerManager.instance.player.GetComponent<PlayerController>()
                .SetFocus(target);
                break;
            case "PICKUP":
                Debug.Log("PICK UP");
                PlayerManager.instance.player.GetComponent<PlayerController>()
                .SetFocus(target.GetComponent<Interactable>());
                break;
            case "TALK":
                Debug.Log("TALK");
                PlayerManager.instance.player.GetComponent<PlayerController>()
                .SetFocus(target.GetComponent<Interactable>());
                break;
            case "CANCEL":
                Debug.Log("CANCEL");
                gameObject.SetActive(false);
                break;
            default:
                Debug.Log("DEFAULT");
                break;
        }

        gameObject.SetActive(false);
        target = null;
    }
}
