using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fill;
    public void ChangeFillAmount(float percentage) => fill.fillAmount = percentage;
    

}
