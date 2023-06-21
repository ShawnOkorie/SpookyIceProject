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
    private CloseUpTrigger closeUpTrigger;
    
    [Header("References")]
    [SerializeField] private GameObject inSceneObject;
    [SerializeField] private GameObject inInventoryObject;

    public int objectID;
    
    [Header("Interaction")]
    [SerializeField]
    public bool isInteractable;
    public ProgressManager.Progress requiredProgress;
    
    [Header("Pickup")]
    public bool isPickup;
    public bool collected => inInventory;
    private bool inInventory;
    public ProgressManager.Progress addProgress;
    
    [Header("Door")]
    [SerializeField] private bool isDoor;
    [SerializeField] private Room targetroom;

    [Header("Merge")]
    [SerializeField] public int mergeableObjectID;
    [SerializeField] private GameObject mergedObject;

    [Header("Riddle")]
    public int solvingObjectID;
    public ProgressManager.Progress addedProgress;
    [SerializeField] protected bool dontDestroyOnSolve;
    public bool isSolved;
    
    
    [Header("Dialogue")]
    [SerializeField] private int start_PID;
    [SerializeField] private int solve_PID;
    
    [Header("Animation")]
    [SerializeField] private AnimationClip interactAnimation;
    
    protected virtual void Awake()
    {
        if (isAwake == false)
        {
            inventoryLayout ??= FindObjectOfType<GridLayoutGroup>();
            //textBox ??= FindObjectOfType<TextBox>();

            miniGameTrigger = GetComponent<MiniGameTrigger>();
            closeUpTrigger = GetComponent<CloseUpTrigger>();
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
            if (start_PID != 0 && inInventory == false)
            {
                DialogManager.Instance.StartDialog(start_PID);
            }
            
            if (isPickup)
            {
                inInventory = true;
                OnInventoryStateChange?.Invoke(inInventory);
                gameObject.transform.SetParent(inventoryLayout.transform);
                ProgressManager.Instance.AddProgress(addProgress);
            }
            
            if (isDoor)
            {
                RoomManager.Instance.LoadRoom(targetroom.myRoom);
                RoomManager.Instance.myAudioSource.Play();
            }
            
            if (miniGameTrigger != null && isSolved)
            {
                miniGameTrigger.StartMiniGame();
            }

            if (closeUpTrigger != null && isSolved)
            {
                closeUpTrigger.OpenCloseUp();
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
            if (otherObject.objectID == solvingObjectID)
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

                isSolved = true;
                
                ProgressManager.Instance.AddProgress(addedProgress);
                DialogManager.Instance.StartDialog(solve_PID);
            }
        }
    }
    
}

