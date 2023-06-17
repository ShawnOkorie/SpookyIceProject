
using UnityEngine;


public class ClickManager : Singleton<ClickManager>
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

            mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);

            mousePosWorld2D = new Vector2(mousePosWorld.x, mousePosWorld.y);
            
            hit = Physics2D.Raycast(mousePosWorld2D, Vector2.zero);
            
            if (hit.collider != null)
            {
                print(hit.collider.gameObject.name);
                
                interactable = hit.transform.gameObject.GetComponentInParent<InteractableObject>();

                if (lastClicked?.mergeableObjectID == interactable.mergeableObjectID)
                {
                    if (interactable.mergeableObjectID == 0 || lastClicked?.mergeableObjectID == 0)
                    {
                        return;
                    }
                    interactable.Merge(lastClicked);
                }

                if (lastClicked != null)
                {
                    if (lastClicked.collected && interactable.isPickup == false)
                    {
                        interactable.Solve(lastClicked);
                    }    
                }
                
                if (interactable.collected)
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
