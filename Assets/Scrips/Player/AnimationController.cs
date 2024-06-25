using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
   private Animator _animationController;

   private void Start()
   {
      _animationController = GetComponent<Animator>();
   }

   public void Move()
   {
      _animationController.SetBool("isRun", true);
   }

   public void StopMove()
   {
      _animationController.SetBool("isRun", false);
   }
   
}
