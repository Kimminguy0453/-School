using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField] private Image StaminaBar;

    public void UseStamina(float player_stamina)
    {
        float stamina = player_stamina;
        StaminaBar.fillAmount = stamina / 100;
    }
}
