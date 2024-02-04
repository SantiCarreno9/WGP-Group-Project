/* GameManager.cs
 * Author: Ramandeep Singh
 * Student Number: 301364879
 * Last modified: 02/04/2024
 * 
 * This script controls the minimap camera
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }
}
