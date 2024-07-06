using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
   private Animator _animationController;

   public GameObject Gun;

   private void Start()
   {
      _animationController = GetComponent<Animator>();
   }

   private void Awake()
   {
      Gun.SetActive(false);
   }

   
   public void Move(bool isRunning)
   {
      _animationController.SetBool("isWalk", !isRunning);
      _animationController.SetBool("isRun", isRunning);
   }

   public void StopMove()
   {
      _animationController.SetBool("isWalk", false);
      _animationController.SetBool("isRun", false);
   }
   
   public void Jump()
   {
      _animationController.SetBool("isJump", true);
   }
   public void StopJump()
   {
      _animationController.SetBool("isJump", false);
   }
}
