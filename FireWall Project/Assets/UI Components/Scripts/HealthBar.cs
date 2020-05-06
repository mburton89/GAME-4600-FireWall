using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance;

    public float healthAmount;
    public Image healthBarFill;

    public Color blue;
    public Color yellow;
    public Color red;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DetermineColor();
    }

    public void DecreaseHealth(float amountToDecrease)
    {
        healthAmount -= amountToDecrease;
        healthBarFill.fillAmount = healthAmount;
        DetermineColor();
    }

    public void IncreaseHealth(float amountToIncrease)
    {
        healthAmount += amountToIncrease;
        healthBarFill.fillAmount = healthAmount;
        DetermineColor();
    }

    public void UpdateHealthBar(float healthAmount)
    {
        healthBarFill.fillAmount = healthAmount;
        DetermineColor();
    }

    void DetermineColor()
    {
        if (healthAmount > .66f)
        {
            healthBarFill.color = blue;
        }
        else if (healthAmount > .33f)
        {
            healthBarFill.color = yellow;
        }
        else
        {
            healthBarFill.color = red;
        }
    }
}