using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public float hp = 100f;

    [SerializeField] private Slider _hpBar;

    public float Hp
    {
        get => hp;
        // HP는 PlayerController에서만 수정 하도록 private으로 처리
        // Math.Clamp 함수를 사용해서 hp가 0보다 아래로 떨어지지 않게 합니다.
        private set => hp = Math.Clamp(value, 0, hp);
    }

    private void Awake()
    {
        // MaxValue를 세팅하는 함수입니다.
        SetMaxHealth(hp);
    }

    public void SetMaxHealth(float health)
    {
        _hpBar.maxValue = health;
        _hpBar.value = health;
    }

    // 플레이어가 대미지를 받으면 대미지 값을 전달 받아 HP에 반영합니다.
    public void GetDamage(float damage)
    {
        float getDamagedHp = Hp - damage;
        Hp = getDamagedHp;
        _hpBar.value = Hp;
    }
}
