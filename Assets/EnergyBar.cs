using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    public void setHealth(float energy)
    {
        slider.value = energy;
    }
}
