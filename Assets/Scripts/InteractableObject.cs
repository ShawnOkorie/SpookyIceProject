using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogSystem;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour,IShouldForceAwake
{
    public delegate void InventoryStateChange(bool inInventory);
    public event InventoryStateChange OnInventoryStateChange;
    
    private static GridLayoutGroup inventoryLayout;
    protected bool isAwake;
    private MiniGameTrigger miniGameTrigger;
    
    [Header("References")]
    [SerializeField] private GameObject inSceneObject;
    [SerializeField] private GameObject inInventoryObject;

    public int objectID;
    
    [Header("Interaction")]
    [SerializeField] protected bool isInteractable;
    [SerializeField] private ProgressManager.Progress requiredProgress;
    
    [Header("Pickup")]
    public bool isPickup;
    public bool collected => inInventory;
    private bool inInventory;
   

    [Header("Door")]
    [SerializeField] private bool isDoor;
    [SerializeField] private Room targetroom;

    [Header("Merge")]
    [SerializeField] public int mergeableObjectID;
    [SerializeField] private GameObject mergedObject;

    [Header("Riddle")]
    [SerializeField] protected int solvingObjectID;
    [SerializeField] protected ProgressManager.Progress addedProgress;
    [SerializeField] protected bool dontDestroyOnSolve;
    [SerializeField] protected bool isSolved;
    
    [Header("Dialogue")]
    [SerializeField] private int start_pid;
    
    [Header("Animation")]
    [SerializeField] private AnimationClip interactAnimation;
    
    protected virtual void Awake()
    {
        if (isAwake == false)
        {
            inventoryLayout ??= FindObjectOfType<GridLayoutGroup>();
            //textBox ??= FindObjectOfType<TextBox>();

            miniGameTrigger = GetComponent<MiniGameTrigger>();
        }
        isAwake = true;
    }
    
    public void ForceAwake()
    {
        Awake();
    }
    
    protected virtual void Start()
    {
        OnInventoryStateChange += ChangeActiveObject;
        
        ProgressManager.Instance.OnProgressChanged += UnlockInteracability;
       
        GameStateManager.Instance.OnSetUninteractible += SetUninteractible;
        GameStateManager.Instance.OnSetInteractible += SetInteractible;
        
        if (requiredProgress != ProgressManager.Progress.None)
            isInteractable = false;
    }
    
    protected virtual void OnDestroy()
    {
        OnInventoryStateChange -= ChangeActiveObject;
        
        ProgressManager.Instance.OnProgressChanged -= UnlockInteracability;
        
        GameStateManager.Instance.OnSetUninteractible -= SetUninteractible;
        GameStateManager.Instance.OnSetInteractible -= SetInteractible;
    }

    private void ChangeActiveObject(bool inInventory)
    {
        switch (inInventory)
        {
            case true:
                inSceneObject.gameObject.SetActive(false);
                inInventoryObject.gameObject.SetActive(true);
                break;
            case false:
                inSceneObject.gameObject.SetActive(true);
                inInventoryObject.gameObject.SetActive(false);
                break;
        }
    }
    
    private void UnlockInteracability(ProgressManager.Progress progress)
    {
        if (progress == requiredProgress)
        {
            requiredProgress = ProgressManager.Progress.None; 
            isInteractable = true;
        }
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
    
    public virtual void Interact()
    {
        if (isInteractable)
        {
            if (isPickup)
            {
                inInventory = true;
                //OnInventoryStateChange.Invoke(inInventory);
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
                {
                    otherObject.dontDestroyOnSolve = false;
                    return;  
                }
                if (otherObject.dontDestroyOnSolve == false)
                {
                    Destroy(otherObject.gameObject);
                }
                
                ProgressManager.Instance.AddProgress(addedProgress);
                isSolved = true;
                print("solved");
            }
        }
    }
}

