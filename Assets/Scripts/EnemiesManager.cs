using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using KM.Utility;

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

        [SerializeField] bool enemiesSpawned = false;
        [SerializeField, SimpleButton("SpawnEnemies")] bool spawnEnemiesButton;

        public int Columns => columns;
        public int Rows => rowConfig.Length;


        List<List<BasicEnemyBehaviour>> _enemies = new List<List<BasicEnemyBehaviour>>();
        Rigidbody2D _rb;
        int enemyCount;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            SpawnEnemies();
            StartCoroutine(EnemyArrayMovement());
        }

        private IEnumerator EnemyArrayMovement()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                yield return new WaitForFixedUpdate();

                // Move right
                Vector3 origin = transform.position;
                Vector3 target = origin + Vector3.right * leftRightDistance;
                while (transform.position.x < target.x)
                {
                    _rb.MovePosition(transform.position + Vector3.right * movementSpeed * Time.deltaTime);
                    yield return new WaitForFixedUpdate();
                }

                // Move left
                origin = target;
                target += Vector3.left * leftRightDistance;
                while (transform.position.x > target.x)
                {
                    _rb.MovePosition(transform.position + Vector3.left * movementSpeed * Time.deltaTime);
                    yield return new WaitForFixedUpdate();
                }

                // Move down
                origin = target;
                target += Vector3.down * stepDownDistance;
                while (transform.position.y > target.y)
                {
                    _rb.MovePosition(transform.position + Vector3.down * movementSpeed * Time.deltaTime);
                    yield return new WaitForFixedUpdate();
                }
            }
        }

        public void SpawnEnemies()
        {
            if (enemiesSpawned) 
                return;

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
                _enemies.Add(column);
                position.x += columnDistance;
                position.y = transform.position.y;
            }
            enemyCount = Columns * Rows;
            enemiesSpawned = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
            {
                GameManager.Instance.AdjustPoints(1);
                collision.otherCollider.GetComponent<BasicEnemyBehaviour>().Deactivate();
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                //pointEvent.Invoke(-2 * enemies[collision.otherCollider.GetComponent<BasicEnemyBehaviour>().Column].Count(e => e.gameObject.activeInHierarchy));
                GameManager.Instance.AdjustPoints(-2 * Rows);
                collision.otherCollider.GetComponent<BasicEnemyBehaviour>().Deactivate();
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("GameOver"))
            {
                OnGameOver();
                GameManager.Instance.GameOver("lost");
            }

            collision.otherCollider.gameObject.SetActive(false);
            if (--enemyCount == 0)
            {
                OnGameOver();
                GameManager.Instance.GameOver("won");
            }
        }

        private void OnGameOver()
        {
            StopAllCoroutines();
            for (int i = 0; i < columns; i++)
                for (int j = 0; j < rowConfig.Length; j++)
                    _enemies[i][j].Deactivate();
            return;
        }
    }
}
