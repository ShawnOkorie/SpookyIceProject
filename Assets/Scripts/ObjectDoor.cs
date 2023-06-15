using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDoor : InteractableObject
{
    [SerializeField] private Collider2D mycollider;
    [SerializeField] private Image myImage;
    
    [SerializeField] private bool isOpen;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    
    [SerializeField] private List<InteractableObject> myObjects;
    
    protected override void Awake()
    {
        mycollider = GetComponent<Collider2D>();
    }

    protected override void Interact()
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
                    mycollider.enabled = true;
                    myImage.sprite = closedSprite;

                    foreach (InteractableObject obj in myObjects)
                    {
                        obj.gameObject.SetActive(true);
                    }
                    break;
            
                case false:
                    isOpen = true;
                    mycollider.enabled = false;
                    myImage.sprite = openSprite;
                    
                    foreach (InteractableObject obj in myObjects)
                    {
                        obj.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
    
}
