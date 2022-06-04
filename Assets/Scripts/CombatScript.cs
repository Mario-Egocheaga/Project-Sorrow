using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CombatScript : MonoBehaviour
{
    private PlayerMovement movement;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        
    }

    #region Input
    public void OnLightAttack()
    {
        anim.SetTrigger("LightAttack");
    }
    #endregion
}
