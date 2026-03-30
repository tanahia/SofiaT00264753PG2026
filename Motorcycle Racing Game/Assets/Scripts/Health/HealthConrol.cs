using UnityEngine;
using UnityEngine.UI;

public class HealthConrol : MonoBehaviour
{
    public Slider slider;
    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void setHealth(int health)
    {
        slider.value = health;
    }
}
