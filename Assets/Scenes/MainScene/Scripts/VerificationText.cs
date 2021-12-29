using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerificationText : MonoBehaviour
{
   public GameObject fieldObject;
 
   void Start () {
      fieldObject = GameObject.Find("Canvas");
      fieldObject.SetActive(false);
   }
 
   
   public void FieldActive()
   {
      fieldObject.SetActive(true);
   }
 
}