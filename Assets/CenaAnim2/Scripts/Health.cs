using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Text healthText;
    public Slider healthBar;

    private void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        UpdateHealthBarText();
    }

    private void Update()
    {
        healthBar.gameObject.transform.LookAt(Camera.main.transform.position);
        if(Input.GetKeyDown(KeyCode.Plus))
        {
            AddHealth(1);
        }
        else if(Input.GetKeyDown(KeyCode.Minus))
        {
            LoseHealth(1);
        }
    }

    public void UpdateHealthBarText()
    {
        healthText.text = health + "/" + maxHealth;
    }

    public void UpdateHealthBarValue()
    {
        healthBar.value = health;
    }

    public void LoseHealth(int damage)
    {
        if(health - damage <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        health -= damage;
        UpdateHealthBarValue();
    }

    public void AddHealth(int heal)
    {
        if(health + heal <= maxHealth)
        {
            health += heal;
            UpdateHealthBarValue();
        }
    }
}
