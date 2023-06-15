using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogSystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    private static ProgressManager progressManager;
    private static GridLayoutGroup inventoryLayout;
    //private static TextBox textBox;
    private MiniGameTrigger miniGameTrigger;

    [Header("References")]
    [SerializeField] private GameObject inSceneObject;
    [SerializeField] private GameObject inInventoryObject;

    [SerializeField] private int objectID;
    
    [Header("Interaction")]
    [SerializeField] private bool isInteractable;
    [SerializeField] private ProgressManager.Progress requiredProgress;
    
    [Header("Pickup")]
    [SerializeField]
    public bool isPickup;

    public bool inInventory;

    [Header("Door")]
    [SerializeField] private bool isDoor;
    [SerializeField] private Room targetroom;

    [Header("Merge")]
    [SerializeField]
    public int mergeableObjectID;
    [SerializeField] private GameObject mergedObject;

    [Header("Riddle")]
    [SerializeField] private int solvingObjectID;
    [SerializeField] private ProgressManager.Progress addedProgress;
    [SerializeField] private bool dontDestroyOnSolve;
    [SerializeField] private bool isSolved;
    
    [Header("Dialogue")]
    [SerializeField] private int start_pid;
    
    [Header("Animation")]
    [SerializeField] private AnimationClip interactAnimation;
    
    private void Awake()
    {
        progressManager ??= FindObjectOfType<ProgressManager>();
        inventoryLayout ??= FindObjectOfType<GridLayoutGroup>();
        //textBox ??= FindObjectOfType<TextBox>();

        miniGameTrigger = GetComponent<MiniGameTrigger>();
        
        
    }

    private void OnDestroy()
    {
        progressManager.OnProgressChanged -= UnlockInteracability;
        
        GameStateManager.Instance.OnSetUninteractible -= SetUninteractible;
        GameStateManager.Instance.OnSetInteractible -= SetInteractible;
    }

    public void SetUninteractible()
    {
        isInteractable = false;
    }

    public void SetInteractible()
    {
        isInteractable = true;
        if (requiredProgress != ProgressManager.Progress.None)
            isInteractable = false;
    }
    

    private void Start()
    {
        progressManager.OnProgressChanged += UnlockInteracability;
        GameStateManager.Instance.OnSetUninteractible += SetUninteractible;
        GameStateManager.Instance.OnSetInteractible += SetInteractible;
        
        if (requiredProgress != ProgressManager.Progress.None)
            isInteractable = false;
    }

    private void UnlockInteracability(ProgressManager.Progress progress)
    {
        if (progress == requiredProgress)
        {
            requiredProgress = ProgressManager.Progress.None; 
            isInteractable = true;
        }
    }
    
    public void Interact()
    {
        if (isInteractable)
        {
            if (isPickup)
            {
                inInventory = true;
            
                inSceneObject.gameObject.SetActive(false);
                inInventoryObject.gameObject.SetActive(true);
                gameObject.transform.SetParent(inventoryLayout.transform);
            }

            if (start_pid != 0)
            {
                DialogManager.Instance.StartDialog(start_pid);
            }
        
            if (interactAnimation)
            {
                /*play anim*/
            }

            if (isDoor)
            {
                RoomManager.Instance.LoadRoom(targetroom.myRoom);
            }

            if (miniGameTrigger != null && isSolved)
            {
                miniGameTrigger.StartMiniGame();
            }
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
                    return;
                
                Destroy(otherObject.gameObject);
                
                progressManager.AddProgress(addedProgress);
                otherObject.isSolved = true;
                print("solved");
            }
        }
    }

    
}

