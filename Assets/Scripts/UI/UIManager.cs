using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider healthBar, staminaBar;
    public Slider overhealthBar;
    public Image healthFill, staminaFill;
    public Image heartIcon;
    public Sprite heartNormal, heartGold;

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
        string lifeStageName = player.transform.GetChild(3).name;
        lifeStageText.text = lifeStageName;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMechanics = player.GetComponent<PlayerMechanics>();
        maxHealth = playerMechanics.GetMaxHealth();
        maxStamina = playerMechanics.GetStamina();
        currentEvolutionProgress = playerMechanics.GetEvolutionProgress();

        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        currentHealth = maxHealth;

        if (overhealthBar != null)
        {
            overhealthBar.maxValue = playerMechanics.GetMaxOverhealth();
            overhealthBar.value = 0;
        }

        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
        currentStamina = maxStamina;

        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerMechanics.GetHealth();
        healthBar.value = Mathf.Min(currentHealth, maxHealth);

        if (overhealthBar != null)
        {
            int overhealth = Mathf.Max(0, currentHealth - maxHealth);
            overhealthBar.value = overhealth;
            overhealthBar.gameObject.SetActive(overhealth > 0);
        }

        if (heartIcon != null && heartNormal != null && heartGold != null)
            heartIcon.sprite = currentHealth > maxHealth ? heartGold : heartNormal;

        currentStamina = playerMechanics.GetStamina();
        staminaBar.value = currentStamina;

        currentEvolutionProgress = playerMechanics.GetEvolutionProgress();
        int filled = Mathf.Clamp(currentEvolutionProgress, 0, 5);
        evo1.sprite = filled >= 1 ? fullEvo : emptyEvo;
        evo2.sprite = filled >= 2 ? fullEvo : emptyEvo;
        evo3.sprite = filled >= 3 ? fullEvo : emptyEvo;
        evo4.sprite = filled >= 4 ? fullEvo : emptyEvo;
        evo5.sprite = filled >= 5 ? fullEvo : emptyEvo;
    }
}
