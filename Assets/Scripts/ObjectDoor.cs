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

    public bool isOpen;
  
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    
    [SerializeField] private List<GameObject> myObjects;
    
    protected override void Awake()
    {
        if (isAwake)
        {
            myCollider = GetComponent<Collider2D>();
        }
    }

    protected override void Start()
    {
        base.Start();
        OnDoorStateChange += ChangeSprite;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        OnDoorStateChange -= ChangeSprite;
    }

    private void ChangeSprite(bool isOpen)
    {
        switch (isOpen)
        {
            case true:
                myCollider.enabled = false;
                myImage.sprite = openSprite;
                break;
            case false:
                myCollider.enabled = true;
                myImage.sprite = closedSprite;
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
                            obj.gameObject.SetActive(true);
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
                            obj.gameObject.SetActive(false);
                        }
                    }
                    break;
            }
        }
    }
    
}
