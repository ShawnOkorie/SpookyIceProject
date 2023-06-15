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

    [SerializeField] private GameObject inSceneObject;
    [SerializeField] private GameObject inInventoryObject;

    public int objectID;
    
    public bool isInteractable;
    private ProgressManager.Progress requiredProgress;
    
    public bool isPickup;
    [HideInInspector] public bool inInventory;

    public bool isDoor;
    public Room targetroom;

    public int mergeableObjectID;
    [SerializeField] private GameObject mergedObject;

    public int solvingObjectID;
    private ProgressManager.Progress addedProgress;
    public bool dontDestroyOnSolve;
    public bool isSolved;

    public int start_pid;

    public AnimationClip interactAnimation;
    

    private void Awake()
    {
        progressManager ??= FindObjectOfType<ProgressManager>();
        inventoryLayout ??= FindObjectOfType<GridLayoutGroup>();
        //textBox ??= FindObjectOfType<TextBox>();

        miniGameTrigger = GetComponent<MiniGameTrigger>();
        
        progressManager.OnProgressChanged += UnlockInteracability;
        LoadingScreen.onFadeStart += MakeUninteractible;
        LoadingScreen.onFadeEnd += MakeInteractible;
        DialogManager.Instance.OnDialogStart += MakeUninteractible;
        DialogManager.Instance.OnDialogEnd += MakeUninteractible;
    }

    
    private void MakeUninteractible()
    {
        if (requiredProgress != ProgressManager.Progress.None)
            isInteractable = false;
        isInteractable = false;
    }
    private void MakeInteractible()
    {
        if (requiredProgress != ProgressManager.Progress.None)
            isInteractable = false;
        isInteractable = true;
        
    }

    private void OnDestroy()
    {
        progressManager.OnProgressChanged -= UnlockInteracability;
        LoadingScreen.onFadeStart -= MakeUninteractible;
        LoadingScreen.onFadeEnd -= MakeInteractible;
        DialogManager.Instance.OnDialogStart -= MakeUninteractible;
        DialogManager.Instance.OnDialogEnd -= MakeUninteractible;
    }

    private void Start()
    {
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

            if (isDoor && isInteractable)
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

