using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdventureManager : MonoBehaviour
{
    public Vector3 mousePos;
    private RaycastHit2D hit;
    public Camera mainCamera;
    public Vector3 mousePosWorld;
    public Vector2 mousePosWorld2D;
    public string nextScene;
   
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
                
                if (hit.collider.gameObject.tag == "Door")
                {
                    SceneManager.LoadScene(nextScene);
                }
            }
            else
            {
                print("object mit keinem collider hit");
            }
        }
    }
}
