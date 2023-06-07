using System;
using System.Collections;
using System.Collections.Generic;
using DialogSystem;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    private ProgressManager progressManager;
    private GridLayoutGroup inventoryLayout;

    [SerializeField] private GameObject inSceneObject;
    [SerializeField] private GameObject inInventoryObject;

    public int objectID;
    
    public bool isInteractable;
    private ProgressManager.Progress requiredProgress;
    
    public bool isPickup;
    [HideInInspector] public bool inInventory;

    public int mergeableObjectID;
    [SerializeField] private GameObject mergedObject;

    public int solvingObjectID;
    private ProgressManager.Progress addedProgress;
    public bool dontDestroyOnSolve;
    
    public int dialogueID;

    public AnimationClip interactAnimation;

    private void Awake()
    {
        progressManager = FindObjectOfType<ProgressManager>();
        inventoryLayout = FindObjectOfType<GridLayoutGroup>();

        progressManager.OnProgressChanged += ChangeInteractability;
    }

    private void OnDestroy()
    {
        progressManager.OnProgressChanged -= ChangeInteractability;
    }

    private void Start()
    {
        if (requiredProgress != ProgressManager.Progress.None)
        {
            isInteractable = false;
        }
    }

    private void ChangeInteractability(ProgressManager.Progress progress)
    {
        if (progress == requiredProgress)
        {
            isInteractable = true;
        }
    }
    
    public void Interact()
    {
        if (isPickup)
        {
            inInventory = true;
            
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
        if (isInteractable)
        {
            if (inInventory && otherObject.inInventory)
            {
                Destroy(otherObject.gameObject);
                Instantiate(mergedObject, transform.position, quaternion.identity, inventoryLayout.transform);
                Destroy(gameObject);
            }
        }
    }

    public void Solve(InteractableObject otherObject)
    {
        if (isInteractable)
        {
            if (otherObject.solvingObjectID == objectID)
            {
                if (otherObject.dontDestroyOnSolve)
                {
                    return;
                }
                
                Destroy(otherObject.gameObject);
                
                //OnAddProgress?.Invoke(addedProgress);
                print("solved");
            }
        }
    }
}

