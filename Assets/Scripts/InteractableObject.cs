using System;
using System.Collections;
using System.Collections.Generic;
using DialogSystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    private GridLayoutGroup inventoryLayout;

    [SerializeField] private GameObject inSceneObject;
    [SerializeField] private GameObject inInventoryObject;

    public int objectID;
    
    public bool isInteractable;
    
    public bool ispickup;

    public int mergeableObjectID;
    [SerializeField] private GameObject mergedObject;
    
    public int dialogueID;

    public bool interactAnimation;

    private void Awake()
    {
        inventoryLayout = FindObjectOfType<GridLayoutGroup>();
    }

    public void ShowInteractability()
    {
        if (ispickup)
        {
            print("Pickup");
        }
        if (isInteractable)
        {
            print("Interact");
        }
        if (dialogueID != 0)
        {
            print("Start Dialouge");
        }
    }
    
    public void Interact()
    {
        if (ispickup)
        {
            inSceneObject.gameObject.SetActive(false);
            inInventoryObject.gameObject.SetActive(true);
            gameObject.transform.SetParent(inventoryLayout.transform);
        }

        if (dialogueID != 0)
        {
            DialogManager.Instance.StartDialog(dialogueID);
        }
        
        if (interactAnimation)
        {
            /*play anim*/
        }
        
    }

    public void Merge(InteractableObject otherObject)
    {
        Destroy(otherObject);
        Instantiate(mergedObject, transform.position, quaternion.identity, inInventoryObject.transform);
        Destroy(this);
    }
}
