using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
   public GridLayoutGroup myLayout;
   public List<InteractableObject> invObjects = new List<InteractableObject>();

   protected override void Awake()
   {
      base.Awake();
      myLayout = GetComponent<GridLayoutGroup>();
      invObjects = GetComponentsInChildren<InteractableObject>().ToList();
   }
   
}
