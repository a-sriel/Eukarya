using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider healthBar, staminaBar;
    public Image healthFill, staminaFill;

    // Evolution progress bar consists of 5 individual stars
    public Image evo1, evo2, evo3, evo4, evo5;

    private int maxHealth, maxStamina;
    private int currentHealth, currentStamina,
                currentEvolutionProgress;

    public GameObject player;
    private PlayerMechanics playerMechanics;

    public Sprite emptyEvo, fullEvo;

    public TextMeshProUGUI lifeStageText;

    void SetText()
    {
        lifeStageText.text = "Tiktaalik";
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMechanics = player.GetComponent<PlayerMechanics>();
        maxHealth = playerMechanics.GetHealth();
        maxStamina = playerMechanics.GetStamina();
        currentEvolutionProgress = playerMechanics.GetEvolutionProgress();

        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        currentHealth = maxHealth;

        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerMechanics.GetHealth();
        healthBar.value = currentHealth;

        currentStamina = playerMechanics.GetStamina();
        staminaBar.value = currentStamina;

        currentEvolutionProgress = playerMechanics.GetEvolutionProgress();
        switch(currentEvolutionProgress)
        {
            case 0:
                evo1.sprite = emptyEvo;
                evo2.sprite = emptyEvo;
                evo3.sprite = emptyEvo;
                evo4.sprite = emptyEvo;
                evo5.sprite = emptyEvo;
                break;
            case 1:
                evo1.sprite = fullEvo;
                evo2.sprite = emptyEvo;
                evo3.sprite = emptyEvo;
                evo4.sprite = emptyEvo;
                evo5.sprite = emptyEvo;
                break;
            case 2:
                evo1.sprite = fullEvo;
                evo2.sprite = fullEvo;
                evo3.sprite = emptyEvo;
                evo4.sprite = emptyEvo;
                evo5.sprite = emptyEvo;
                break;
            case 3:
                evo1.sprite = fullEvo;
                evo2.sprite = fullEvo;
                evo3.sprite = fullEvo;
                evo4.sprite = emptyEvo;
                evo5.sprite = emptyEvo;
                break;
            case 4:
                evo1.sprite = fullEvo;
                evo2.sprite = fullEvo;
                evo3.sprite = fullEvo;
                evo4.sprite = fullEvo;
                evo5.sprite = emptyEvo;
                break;
            case 5:
                evo1.sprite = fullEvo;
                evo2.sprite = fullEvo;
                evo3.sprite = fullEvo;
                evo4.sprite = fullEvo;
                evo5.sprite = fullEvo;
                break;
        }
    }
}
