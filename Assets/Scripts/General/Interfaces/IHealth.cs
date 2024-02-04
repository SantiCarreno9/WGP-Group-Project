/* IHealth.cs
 * Author: Santiago Carreno
 * Student Number: 301283698
 * Last modified: 02/04/2024
 * 
 * This script contains all the properties and methods associated to the health system
 * 
 */
public interface IHealth
{
    public int HealthPoints { get; }
    public void Damage(int points);
}
