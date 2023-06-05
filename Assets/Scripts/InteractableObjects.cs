using System.Collections;
using System.Collections.Generic;
using DialogSystem;
using UnityEngine;

public class InteractableObjects : MonoBehaviour, IInteractable
{
    public bool isInteractable;
    public bool ispickupable;
    public int pid;
    public bool hasDialoug;
    public bool isMergable;
    public bool interactAnimation;
    
    

    public void ShowInteractability()
    {
        if (ispickupable)
        {
            print("Aufheben");
        }
        if (isInteractable)
        {
            print("Interagieren");
        }
        if (hasDialoug)
        {
            print("Start Dialoug");
        }
    }




    public void Interact()
    {
        if (ispickupable)
        {
            /* ins Inventar aufheben*/
        }


        if (isInteractable)
        {
            /*interact*/
        }

        if (hasDialoug)
        {
            DialogManager.Instance.StartDialog(pid);
        }

        if (isMergable)
        {
            /*merge*/
        }

        if (interactAnimation)
        {
            /*play anim*/
        }


    }

}
