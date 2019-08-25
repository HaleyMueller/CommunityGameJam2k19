using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBase : MonoBehaviour
{

    #region Health
    [SerializeField]
    private float _health = 10f;

    public float MaxHealth = 10f;

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health += value;

            if (_health > MaxHealth)
                _health = MaxHealth;
        }
    }
    #endregion

    public string Name = "Default";

    void Start()
    {
        
    }

    void Update()
    {

    }
}
