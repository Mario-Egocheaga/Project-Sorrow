using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CombatScript : MonoBehaviour
{
    private PlayerMovement movement;
    private Animator anim;

    private bool activateTimerToReset;

    private float default_Combo_Timer = .4f;
    private float current_Combo_Timer;

    private LightComboState current_LightCombo_State;
    private HardComboState current_HardCombo_State;

    public enum LightComboState { 
        NONE,
        LIGHTATTACK_1,
        LIGHTATTACK_2,
        LIGHTATTACK_3,
        KICK_1
    }

    public enum HardComboState { 
        NONE,
        HARDATTACK_1,
        HARDKICK_1
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();

        current_Combo_Timer = default_Combo_Timer;
        current_LightCombo_State = LightComboState.NONE;
        current_HardCombo_State = HardComboState.NONE;
    }

    private void Update()
    {
        ResetComboState();
    }

    void preformLightCombo()
    {
        if (current_LightCombo_State == LightComboState.KICK_1)
        {
            return;
        }


        current_LightCombo_State++;
        activateTimerToReset = true;
        current_Combo_Timer = default_Combo_Timer;

        switch (current_LightCombo_State)
        {
            case (LightComboState)4:
                if (!PlayerMovement.crawl)
                {
                    anim.SetTrigger("LightKick");
                }
                break;
            case (LightComboState)3:
                anim.SetTrigger("LightAttack_3");
                break;
            case (LightComboState)2:
                anim.SetTrigger("LightAttack_2");
                break;
            case (LightComboState)1:
                anim.SetTrigger("LightAttack");
                break;
            default:
                current_LightCombo_State = LightComboState.NONE;
                break;
        }
    }

    void preformHardCombo()
    {
        if (current_HardCombo_State == HardComboState.HARDKICK_1)
        {
            return;
        }


        current_HardCombo_State++;
        activateTimerToReset = true;
        current_Combo_Timer = default_Combo_Timer;

        switch (current_HardCombo_State)
        {
            case (HardComboState)2:
                anim.SetTrigger("HardKick");
                break;
            case (HardComboState)1:
                anim.SetTrigger("HardAttack");
                break;
            default:
                current_HardCombo_State = HardComboState.NONE;
                break;
        }
    }

    void ResetComboState()
    {
        if (activateTimerToReset)
        {
            current_Combo_Timer -= Time.deltaTime;

            if (current_Combo_Timer <= 0f)
            {
                current_LightCombo_State = LightComboState.NONE;
                current_HardCombo_State = HardComboState.NONE;

                activateTimerToReset = false;
                current_Combo_Timer = default_Combo_Timer;
            }
        }
    }


    #region Input
    public void OnLightAttack()
    {
        preformLightCombo();
    }

    public void OnHardAttack()
    {
        preformHardCombo();
    }
    #endregion
}
