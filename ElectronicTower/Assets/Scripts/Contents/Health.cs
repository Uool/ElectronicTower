using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float _maxHealth;
    private float _criticalHealthRatio = 0.3f;

    public UnityAction<float, GameObject> onDamaged;
    public UnityAction<float> onHealed;
    public UnityAction onDie;

    public float currentHealth { get; private set; }

    public float getRatio() => currentHealth / _maxHealth;           // 현재 남은 체력 비율
    public bool isCritical() => getRatio() <= _criticalHealthRatio;  // 치명적 비율보다 낮으면 위험 상황 True

    bool m_IsDead;

    private void OnEnable()
    {
        m_IsDead = false;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this._maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public void Heal(float healAmount)
    {
        float healthBefore = currentHealth;
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, _maxHealth);

        // Call OnHeal Action
        float trueHealAmount = currentHealth - healthBefore;
        if (trueHealAmount > 0f && null != onHealed)
            onHealed.Invoke(trueHealAmount);
    }

    public void TakeDamage(float damage, GameObject damageSource)
    {
        float healthBefore = currentHealth;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, _maxHealth);

        // Call OnDamage Action
        float trueDamageAmount = healthBefore - currentHealth;
        if (trueDamageAmount > 0f && null != onDamaged)
            onDamaged.Invoke(trueDamageAmount, damageSource);

        // 죽었는지 여부 확인
        HandleDeath();
    }

    // 한방에 죽은 경우 (낙사 or 즉사 스킬 등..)
    public void Kill()
    {
        currentHealth = 0f;

        if (null != onDamaged)
            onDamaged.Invoke(_maxHealth, null);

        HandleDeath();
    }

    public void HandleDeath()
    {
        if (m_IsDead)
            return;

        // Call OnDie Action
        if (currentHealth <= 0f)
        {
            if (null != onDie)
            {
                m_IsDead = true;
                onDie.Invoke();
            }
        }
    }
}
