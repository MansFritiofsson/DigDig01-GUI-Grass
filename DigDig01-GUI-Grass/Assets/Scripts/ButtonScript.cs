using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{

    public float damageAmount = 10f; // Hur mycket skada som DMG-knappen ska ge
    public float healAmount = 15f; // Hur mycket health som HEAL-knappen ska fylla p�

    public GameObject enemy; // Global variabel f�r fiende-objektet, v�rdet s�tts i Unity
    EnemyScript enemyS; // V�rde f�r fiende-scriptet, v�rdet s�tts i Start()-metoden och �ven i SetEnemy() ifall fienden byts till n�gon annan

    public GameObject healthbar; // Global variabel f�r GUI-healthbaren, v�rdet s�tts i Unity
    Slider slider; // V�rde f�r Slider-komponenten av healthbaren, v�rdet s�tts i Start()-metoden

    public GameObject fireParticles; // Global variabel f�r objektet som inneh�ller ett particle-systems f�r eld- och vattenpartiklar som aktiveras n�r DMG och HEAL knappen trycks ned
    public GameObject waterParticles;

    void Start() // K�rs n�r spelet startar
    {
        // Uppdaterar variablerna som deklarerades tidigare och h�mtar komponenterna
        slider = healthbar.GetComponent<Slider>();
        enemyS = enemy.GetComponent<EnemyScript>();
        UpdateHealthbarAndEnemy(); // Uppdaterar heathbar till att matcha fiendens HP
    }

    public void SetEnemy(GameObject newEnemy) // Ifall fiende-variabeln skulle beh�va s�ttas till en annan fiende (finns i wishlist) s� k�rs den h�r funktionen som uppdaterar variablerna f�r fiendens script osv. och uppdaterar healthbaren till den nya fiendens HP
    {
        enemy = newEnemy;
        enemyS = newEnemy.GetComponent<EnemyScript>();
        UpdateHealthbarAndEnemy();
    }

    public void TakeDamage() // Metod som k�rs n�r DMG-knappen trycks ned
    {
        enemyS.health -= damageAmount; // Minskar fiendens health
        fireParticles.GetComponent<ParticleSystem>().Play(); // Startar eld-particle systemet
        UpdateHealthbarAndEnemy();
    }

    public void Heal() // Metod som k�rs n�r HEAL-knappen trycks ned
    {
        if (enemyS.health <= 0) // Returnerar (avbryter funktionen) ifall fienden redan �r d�d, man kan inte heala n�gon som inte lever
        {
            return;
        }
        enemyS.health += healAmount; // �kar fiendens health
        waterParticles.GetComponent<ParticleSystem>().Play();
        UpdateHealthbarAndEnemy();
    }

    public void ResetButton() // Metod som k�rs n�r RESET-knappen trycks ned
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Laddar om scenen
    }

    void UpdateHealthbarAndEnemy() // Uppdaterar fiendens sprite och healthbarens v�rde till att matcha health-variabeln p� fienden
    {
        enemyS.health = Mathf.Clamp(enemyS.health, 0, enemyS.maxHealth); // H�ller HP-v�rdet inom maximum- och minimum-v�rdena

        slider.maxValue = enemyS.maxHealth; // S�tter sliderns v�rde till att representera fiendens HP
        slider.value = enemyS.health;

        Image enemyImage = enemy.GetComponent<Image>();

        if (enemyS.health <= 0) // Ifall fienden �r d�d
        {
            enemyImage.enabled = false; // G�r fienden osynlig
        }
        else
        {
            // Ifall fienden inte �r d�d
            enemyImage.enabled = true; // G�r fienden synlig (ifall den inte redan var det)
            if (enemyS.health > enemyS.maxHealth / 2) // Om HP �r mer �n 50% av max-HP
            {
                enemyImage.sprite = enemyS.healthySprite; // S�tter fiendens sprite till Healthy sprite (definierad i EnemyScript)
            }
            else
            {
                enemyImage.sprite = enemyS.damagedSprite; // S�tter fiendens sprite till Damaged sprite (definierad i EnemyScript)
            }
        }
    }

}
