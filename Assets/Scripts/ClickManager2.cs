
using UnityEngine;


public class ClickManager2 : MonoBehaviour
{
    public Vector3 mousePos;
    private RaycastHit2D hit;
    public Camera mainCamera;
    public Vector3 mousePosWorld;
    public Vector2 mousePosWorld2D;
    
   
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
                print("objekt mit collider hit");
                print(hit.collider.gameObject.name);
                
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                
                
                if (interactable != null)
                {
                    interactable.ShowInteractability();
                    interactable.Interact();
                    
                    
                }
            }
           
        }
    }
}
