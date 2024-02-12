using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class g2_ground : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy_boss") || collision.gameObject.CompareTag("enemy_mid") || collision.gameObject.CompareTag("enemy_low") || collision.gameObject.CompareTag("enemy_skill") || collision.gameObject.CompareTag("fast_skill_enemy") || collision.gameObject.CompareTag("range_skill_enemy")|| collision.gameObject.CompareTag("upgrade_skill_enemy"))
        {
          
            g2_enemy.canGO = true;
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            Destroy(collision.gameObject.GetComponent<Rigidbody>());

            if (g2_towers.tower_Count == 4)
            {
                if (collision.gameObject.CompareTag("enemy_boss") || collision.gameObject.CompareTag("enemy_mid") || collision.gameObject.CompareTag("enemy_low"))
                {
                    collision.gameObject.GetComponent<NavMeshAgent>().speed = 3.5f;
                }
            }
            if (g2_towers.tower_Count == 3)
            {
                if (collision.gameObject.CompareTag("enemy_boss") || collision.gameObject.CompareTag("enemy_mid") || collision.gameObject.CompareTag("enemy_low"))
                {
                    collision.gameObject.GetComponent<NavMeshAgent>().speed = 4f;

                }
            }
            if (g2_towers.tower_Count == 2)
            {
                if (collision.gameObject.CompareTag("enemy_boss") || collision.gameObject.CompareTag("enemy_mid") || collision.gameObject.CompareTag("enemy_low"))
                {
                    collision.gameObject.GetComponent<NavMeshAgent>().speed = 5f;

                }
            }
            if (g2_towers.tower_Count == 1)
            {
                if (collision.gameObject.CompareTag("enemy_boss") || collision.gameObject.CompareTag("enemy_mid") || collision.gameObject.CompareTag("enemy_low"))
                {
                    collision.gameObject.GetComponent<NavMeshAgent>().speed = 6f;

                }
            }
        }
    }
}
