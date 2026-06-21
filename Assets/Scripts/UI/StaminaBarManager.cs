using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarManager : MonoBehaviour
{
    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDrainRate = 20f;
    [SerializeField] private float staminaRegenRate = 10f;
    public float currentStamina;

    [SerializeField] private Slider staminaBar;


    private bool isSprinting;

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
    }

    // Update is called once per frame
    void Update()
    {
        //check for sprint input
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
        //Handle stamina drain/regen
        if (isSprinting)
        {
            //drain stamina
            currentStamina -= staminaDrainRate * Time.deltaTime;
        }
        else if (currentStamina < maxStamina)
        {
            //regenerate stamina
            currentStamina += staminaRegenRate * Time.deltaTime;
        }

        // Clamp stamina between 0 and max
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        // Update UI
        staminaBar.value = currentStamina;
    }

    // Public method to check if player can sprint
    public bool CanSprint()
    {
        bool allowed = true;
        if (currentStamina == 0)
        {
            allowed = false;
            ExhaustCoroutine();
        }
        return allowed;
    }

    // makes player wait to use stamina again if fully exhausted
    IEnumerator ExhaustCoroutine()
    {
        yield return new WaitForSeconds(3);
    }
}

