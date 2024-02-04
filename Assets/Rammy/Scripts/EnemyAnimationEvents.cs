/* EnemyAnimationEvents.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script contains the methods called by the enemy's animation events
 * 
 */
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    public void DamageCharacter()
    {
        _enemy.DamageCharacter();
    }
}
