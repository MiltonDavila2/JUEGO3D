using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoManager2 : MonoBehaviour
{
    public static AmmoManager2  Instance {get; set;}

    public TextMeshProUGUI  DisplayMunicion;

    private void Awake(){

        if(Instance !=null  && Instance!=this  ){
            Destroy(gameObject);
        }else{
            Instance = this;
        }
    }
}
