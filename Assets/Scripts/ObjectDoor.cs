using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ObjectDoor : InteractableObject
{
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private Image myImage;
    
    [SerializeField] private bool isOpen;
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
                    myCollider.enabled = true;
                    myImage.sprite = closedSprite;

                    foreach (GameObject obj in myObjects)
                    {
                        obj.gameObject.SetActive(true);
                    }
                    break;
            
                case false:
                    isOpen = true;
                    myCollider.enabled = false;
                    myImage.sprite = openSprite;
                    
                    foreach (GameObject obj in myObjects)
                    {
                        obj.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
    
}
