using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KM.SpaceInvaders
{
    public class EnemiesManager : MonoBehaviour
    {
        [Header("Enemy array")]

        [SerializeField] int columns = 8;

        [SerializeField, Tooltip("Prefabs to spawn in columns.")]
        BasicEnemyBehaviour[] rowConfig;

        [SerializeField] float columnDistance = 1f;
        [SerializeField] float rowDistance = 1f;


        [Header("Movement")]

        [SerializeField] float leftRightDistance = 1f;
        [SerializeField] float stepDownDistance = 1f;
        [SerializeField] float movementSpeed = 10f;


        [SerializeField] UnityEvent<int> playerHitEnemy;
        [SerializeField] UnityEvent<int> enemyHitPlayer;


        List<List<BasicEnemyBehaviour>> enemies = new List<List<BasicEnemyBehaviour>>();


        private void Awake()
        {
            SpawnEnemies();
        }

        private void FixedUpdate()
        {

        }

        private void SpawnEnemies()
        {
            Vector3 position = transform.position;
            for (int i = 0; i < columns; i++)
            {
                List<BasicEnemyBehaviour> column = new List<BasicEnemyBehaviour>();
                for (int j = 0; j < rowConfig.Length; j++)
                {
                    var enemy = Instantiate(rowConfig[j], position, Quaternion.Euler(0, 0, 180), transform);
                    enemy.Initialize(j, i);
                    column.Add(enemy);
                    position.y -= rowDistance;
                }
                enemies.Add(column);
                position.x += columnDistance;
                position.y = transform.position.y;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
            {
                playerHitEnemy.Invoke(1);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                enemyHitPlayer.Invoke(-2 * enemies[collision.otherCollider.GetComponent<BasicEnemyBehaviour>().Column].Count(e => e.gameObject.activeInHierarchy));
            }

            collision.otherCollider.gameObject.SetActive(false);
        }
    }
}
