using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawnPushPuzzleManager : MonoBehaviour
{
   private ChangeColor[] pressurePlates;

   [SerializeField] private VoidEventChannelSO puzzleCompleted;

   private void Start()
   {
      pressurePlates = GetComponentsInChildren<ChangeColor>();
   }

   /// <summary>
   /// Checks all the pressure plates to see if they have been unlocked
   /// </summary>
   public void CheckPlates()
   {
      for (int i = 0; i < pressurePlates.Length; i++)
      {
         bool complete = pressurePlates[i].IsCorrect;

         if (!complete) return;
      }
      
      puzzleCompleted.RaiseEvent();
   }
   
   
}
