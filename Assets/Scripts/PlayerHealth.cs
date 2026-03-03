using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float invulnerabilityDuration = 1f; 
    [SerializeField] private float blinkInterval = 0.1f;

    public float currentHealth;
    float invulnerabilityTimer;
    SpriteRenderer spriteRenderer;
    float blinkTimer;
    bool blinking;
    public Slider healthSlider;

    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

    }
    void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(invulnerabilityTimer > 0f)
        {
            invulnerabilityTimer -= Time.deltaTime;
            HandleBlink();
        }
    }
    public bool ApplyDamage(float amount)
    {
        if(currentHealth <= 0f || invulnerabilityTimer > 0f)
        return false;

        currentHealth -= amount;

        if(healthSlider != null)
            healthSlider.value = currentHealth;

        if(currentHealth <= 0f)
        {
            Die();
            return true;
        }
        invulnerabilityTimer = invulnerabilityDuration;
        StartBlink(invulnerabilityDuration);
        return true;
    }
    void StartBlink(float duration)
    {
        blinking = true;
        blinkTimer = duration;
    }
    void HandleBlink()
    {
        if(!blinking) 
        {
            return;
        }
        blinkTimer -= Time.deltaTime;
        if(blinkTimer <= 0f)
        {
            blinking = false;
            spriteRenderer.enabled = true;
            return;
        }
        spriteRenderer.enabled = Mathf.FloorToInt(blinkTimer/blinkInterval) % 2 == 0;
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
}