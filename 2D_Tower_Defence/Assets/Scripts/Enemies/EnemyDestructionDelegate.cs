/**
 * Enemy Destruction Delegate Script
 * 
 * Delegate script to handle enemy destruction
 * 
 * Joshua Verhoeff
 * 
 * 2021-03-05
 */
using UnityEngine;

public class EnemyDestructionDelegate : MonoBehaviour {

    public delegate void EnemyDelegate(GameObject enemy);
    public EnemyDelegate enemyDelegate;

    private void OnDestroy() {
        if(enemyDelegate != null) {
            enemyDelegate(gameObject);
        }
    }
}
