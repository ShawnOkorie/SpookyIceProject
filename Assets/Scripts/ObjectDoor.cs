using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ObjectDoor : InteractableObject
{
    public delegate void DoorStateChange(bool isOpen);
    public event DoorStateChange OnDoorStateChange;
    
    private Collider2D myCollider;
    private Image myImage;
    [SerializeField] private bool DestroyonOpen;

    public bool isOpen;
  
    [SerializeField] private GameObject isOpend;
    [SerializeField] private GameObject isClosed;
    
    [SerializeField] private List<GameObject> myObjects;
    
    protected override void Awake()
    {
        if (isAwake)
        {
            
        }
    }

    protected override void Start()
    {
        base.Start();
        OnDoorStateChange += ChangeState;
        ChangeState(false);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        OnDoorStateChange -= ChangeState;
    }

    private void ChangeState(bool isOpen)
    {
        if (ProgressManager.Instance.ContainsProgress(requiredProgress) || requiredProgress != ProgressManager.Progress.None)
        {
            return;
        }

        if (DestroyonOpen)
        {
            Destroy(gameObject);
        }
        switch (isOpen)
        {
            case true:
                isClosed.gameObject.SetActive(false);
                isOpend.gameObject.SetActive(true);
                break;
            case false:
                isClosed.gameObject.SetActive(true);
                isOpend.gameObject.SetActive(false);
                break;
        }
    }
    
    public override void Interact()
    {
        if (isSolved == false)
        {
            base.Interact();
        }
        else
        {
            if (ProgressManager.Instance.ContainsProgress(requiredProgress) || requiredProgress != ProgressManager.Progress.None)
            {
                return;
            }
            switch (isOpen)
            {
                case true:
                    isOpen = false;
                    OnDoorStateChange.Invoke(isOpen);
                    
                    foreach (GameObject obj in myObjects)
                    {
                        InteractableObject interactableObject = obj.GetComponent<InteractableObject>();

                        if (interactableObject.collected == false)
                        {
                            obj.gameObject.SetActive(false);
                        }
                    }
                    break;
            
                case false:
                    isOpen = true;
                    OnDoorStateChange.Invoke(isOpen);
                    
                    foreach (GameObject obj in myObjects)
                    {
                        InteractableObject interactableObject = obj.GetComponent<InteractableObject>();

                        if (interactableObject.collected == false)
                        {
                            obj.gameObject.SetActive(true);
                        }
                    }
                    break;
            }
        }
    }
    
}
