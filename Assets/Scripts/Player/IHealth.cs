using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public int HealthPoints { get; }
    public void Damage(int points);
}
