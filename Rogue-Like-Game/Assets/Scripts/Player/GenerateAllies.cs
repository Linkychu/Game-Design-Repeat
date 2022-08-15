using System;
using System.Collections;
using System.Collections.Generic;
using General;
using Player;
using TMPro;
using UnityEngine;

public class GenerateAllies : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI levelText;


    public void Generate()
    {
        levelText = GetComponentInChildren<TextMeshProUGUI>();
        
    }

   
    
   
    
    
}
