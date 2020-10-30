using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Only allow this script to be attached to the object with the healthbar slider:
[RequireComponent(typeof(Slider))]
public class BarraVitaOggettoLanciabile : MonoBehaviour
{

    // Visible health bar ui:
    private Slider healthbarDisplay;

    public float health=10;
    public int healthPercentage=100;
    public float minimumHealth=0;
    public float maximumHealth=10;

    public int lowHealth=3;
    public int highHealth=6;


    /*public bool regenerateHealth;
    public float healthPerSecond;*/

    public Color highHealthColor = new Color(0.35f, 1f, 0.35f);
    public Color mediumHealthColor = new Color(0.9450285f, 1f, 0.4481132f);
    public Color lowHealthColor = new Color(1f, 0.259434f, 0.259434f);

    private void Start()
    {

        if (healthbarDisplay == null)
        {
            healthbarDisplay = GetComponent<Slider>();
        }

        healthbarDisplay.minValue = minimumHealth;
        healthbarDisplay.maxValue = maximumHealth;

        UpdateHealth();
    }


    private void Update()
    {
        healthPercentage = (int)(100 * health / maximumHealth);

        if (health < minimumHealth)
        {
            health = minimumHealth;
        }

        if (health > maximumHealth)
        {
            health = maximumHealth;
        }

        /*if (health < maximumHealth && regenerateHealth)
        {
            health += healthPerSecond * Time.deltaTime;

            // Each time the health is changed, update it visibly:
            UpdateHealth();
        }*/
    }

    public void UpdateHealth()
    {
        if (healthPercentage <= lowHealth && health >= minimumHealth && transform.Find("Bar").GetComponent<Image>().color != lowHealthColor)
        {
            ChangeHealthbarColor(lowHealthColor);
        }
        else if (healthPercentage <= highHealth && health > lowHealth)
        {
            float lerpedColorValue = (float.Parse(healthPercentage.ToString()) - 25) / 41;
            ChangeHealthbarColor(Color.Lerp(lowHealthColor, mediumHealthColor, lerpedColorValue));
        }
        else if (healthPercentage > highHealth && health <= maximumHealth)
        {
            float lerpedColorValue = (float.Parse(healthPercentage.ToString()) - 67) / 33;
            ChangeHealthbarColor(Color.Lerp(mediumHealthColor, highHealthColor, lerpedColorValue));
        }

        healthbarDisplay.value = health;
    }

    public void GainHealth(float amount)
    {
        // Add 'amount' hitpoints, then update the characters health:
        health += amount;
        UpdateHealth();
    }

    public void TakeDamage(int amount)
    {
        // Remove 'amount' hitpoints, then update the characters health:
        health -= float.Parse(amount.ToString());
        UpdateHealth();
    }

    public void ChangeHealthbarColor(Color colorToChangeTo)
    {
        transform.Find("Bar").GetComponent<Image>().color = colorToChangeTo;
    }

   /* public void ToggleRegeneration()
    {
        regenerateHealth = !regenerateHealth;
    }*/

    public void SetHealth(float value)
    {
        health = value;
        UpdateHealth();
    }

    public float  GetHealth( )
    {
        return health;
    }



}