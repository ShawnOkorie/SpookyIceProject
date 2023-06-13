
using UnityEngine;


public class ClickManager : MonoBehaviour
{
    public Vector3 mousePos;
    private RaycastHit2D hit;
    public Camera mainCamera;
    public Vector3 mousePosWorld;
    public Vector2 mousePosWorld2D;

    public InteractableObject interactable;
    public InteractableObject lastClicked;
    
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            print(mousePos);
            
            mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);
            print(mousePosWorld);
            
            mousePosWorld2D = new Vector2(mousePosWorld.x, mousePosWorld.y);
            
            hit = Physics2D.Raycast(mousePosWorld2D, Vector2.zero);
            
            if (hit.collider != null)
            {
                print("hit");
                print(hit.collider.gameObject.name);
                
                interactable = hit.transform.gameObject.GetComponentInParent<InteractableObject>();

                if (lastClicked?.mergeableObjectID == interactable.mergeableObjectID)
                {
                    interactable.Merge(lastClicked);
                }

                if (lastClicked != null)
                {
                    if (lastClicked.inInventory && interactable.isPickup == false)
                    {
                        interactable.Solve(lastClicked);
                    }    
                }
                
                if (interactable.inInventory)
                {
                    lastClicked = interactable;
                }
                
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
           
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            lastClicked = null;
        }
    }
}
