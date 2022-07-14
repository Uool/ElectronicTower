using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    [Tooltip("�����ٴ� ǥ���� ��Ÿ�� ü�� ����")]
    public float criticalHealthRatio = 0.3f;

    public UnityAction<float, GameObject> onDamaged;
    public UnityAction<float> onHealed;
    public UnityAction onDie;

    public float currentHealth { get; private set; }

    public float getRatio() => currentHealth / maxHealth;           // ���� ���� ü�� ����
    public bool isCritical() => getRatio() <= criticalHealthRatio;  // ġ���� �������� ������ ���� ��Ȳ True

    bool m_IsDead;

    private void OnEnable()
    {
        m_IsDead = false;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public void Heal(float healAmount)
    {
        float healthBefore = currentHealth;
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // Call OnHeal Action
        float trueHealAmount = currentHealth - healthBefore;
        if (trueHealAmount > 0f && null != onHealed)
            onHealed.Invoke(trueHealAmount);
    }

    public void TakeDamage(float damage, GameObject damageSource)
    {
        float healthBefore = currentHealth;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // Call OnDamage Action
        float trueDamageAmount = healthBefore - currentHealth;
        if (trueDamageAmount > 0f && null != onDamaged)
            onDamaged.Invoke(trueDamageAmount, damageSource);

        // �׾����� ���� Ȯ��
        HandleDeath();
    }

    // �ѹ濡 ���� ��� (���� or ��� ��ų ��..)
    public void Kill()
    {
        currentHealth = 0f;

        if (null != onDamaged)
            onDamaged.Invoke(maxHealth, null);

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
