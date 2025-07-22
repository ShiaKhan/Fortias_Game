using UnityEngine;

public abstract class AutoCombatTurn
{
    public void Execute()
    {
        StartTurn();
        AutoPlay();
        EndTurn();
    }

    protected virtual void StartTurn() { /* Hiển thị UI "Your Turn" */ }
    protected abstract void AutoPlay(); // AI sẽ tự động chọn skill
    protected virtual void EndTurn() { /* Chuyển sang turn tiếp theo */ }
}

public class CombatDetector : MonoBehaviour
{
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask enemyLayer;
    
    public CombatEntity FindNearestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);
        
        // Tìm enemy gần nhất
        CombatEntity nearestEnemy = null;
        float minDistance = float.MaxValue;
        
        foreach(var hit in hits)
        {
            var enemy = hit.GetComponent<CombatEntity>();
            if(enemy != null && enemy.IsAlive)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if(distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }
        
        return nearestEnemy;
    }
}

