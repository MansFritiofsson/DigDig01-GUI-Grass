using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{

    public float damageAmount = 10f; // Hur mycket skada som DMG-knappen ska ge
    public float healAmount = 15f; // Hur mycket health som HEAL-knappen ska fylla på

    public GameObject enemy; // Global variabel för fiende-objektet, värdet sätts i Unity
    EnemyScript enemyS; // Värde för fiende-scriptet, värdet sätts i Start()-metoden och även i SetEnemy() ifall fienden byts till någon annan

    public GameObject healthbar; // Global variabel för GUI-healthbaren, värdet sätts i Unity
    Slider slider; // Värde för Slider-komponenten av healthbaren, värdet sätts i Start()-metoden

    public GameObject fireParticles; // Global variabel för objektet som innehåller ett particle-systems för eld- och vattenpartiklar som aktiveras när DMG och HEAL knappen trycks ned
    public GameObject waterParticles;

    void Start() // Körs när spelet startar
    {
        // Uppdaterar variablerna som deklarerades tidigare och hämtar komponenterna
        slider = healthbar.GetComponent<Slider>();
        enemyS = enemy.GetComponent<EnemyScript>();
        UpdateHealthbarAndEnemy(); // Uppdaterar heathbar till att matcha fiendens HP
    }

    public void SetEnemy(GameObject newEnemy) // Ifall fiende-variabeln skulle behöva sättas till en annan fiende (finns i wishlist) så körs den här funktionen som uppdaterar variablerna för fiendens script osv. och uppdaterar healthbaren till den nya fiendens HP
    {
        enemy = newEnemy;
        enemyS = newEnemy.GetComponent<EnemyScript>();
        UpdateHealthbarAndEnemy();
    }

    public void TakeDamage() // Metod som körs när DMG-knappen trycks ned
    {
        enemyS.health -= damageAmount; // Minskar fiendens health
        fireParticles.GetComponent<ParticleSystem>().Play(); // Startar eld-particle systemet
        UpdateHealthbarAndEnemy();
    }

    public void Heal() // Metod som körs när HEAL-knappen trycks ned
    {
        if (enemyS.health <= 0) // Returnerar (avbryter funktionen) ifall fienden redan är död, man kan inte heala någon som inte lever
        {
            return;
        }
        enemyS.health += healAmount; // Ökar fiendens health
        waterParticles.GetComponent<ParticleSystem>().Play();
        UpdateHealthbarAndEnemy();
    }

    public void ResetButton() // Metod som körs när RESET-knappen trycks ned
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Laddar om scenen
    }

    void UpdateHealthbarAndEnemy() // Uppdaterar fiendens sprite och healthbarens värde till att matcha health-variabeln på fienden
    {
        enemyS.health = Mathf.Clamp(enemyS.health, 0, enemyS.maxHealth); // Håller HP-värdet inom maximum- och minimum-värdena

        slider.maxValue = enemyS.maxHealth; // Sätter sliderns värde till att representera fiendens HP
        slider.value = enemyS.health;

        Image enemyImage = enemy.GetComponent<Image>();

        if (enemyS.health <= 0) // Ifall fienden är död
        {
            enemyImage.enabled = false; // Gör fienden osynlig
        }
        else
        {
            // Ifall fienden inte är död
            enemyImage.enabled = true; // Gör fienden synlig (ifall den inte redan var det)
            if (enemyS.health > enemyS.maxHealth / 2) // Om HP är mer än 50% av max-HP
            {
                enemyImage.sprite = enemyS.healthySprite; // Sätter fiendens sprite till Healthy sprite (definierad i EnemyScript)
            }
            else
            {
                enemyImage.sprite = enemyS.damagedSprite; // Sätter fiendens sprite till Damaged sprite (definierad i EnemyScript)
            }
        }
    }

}
