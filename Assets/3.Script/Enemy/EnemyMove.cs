using UnityEngine;

public class EnemyMove : MonoBehaviour, IState
{
    public float moveSpeed;
    public float attackRange;
    private Rigidbody2D rigid;
    private IState attackState;
    private Animator anim;

    public void Init()
    {
        rigid = GetComponent<Rigidbody2D>();
        attackState = GetComponent<EnemyAttack>();
        anim = GetComponent<Animator>();
    }

    public IState CheckTransition()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position - new Vector3(attackRange / 2, 0), new Vector3(attackRange, attackRange), 0);
        foreach (Collider2D col in cols)
        {
            if (col.TryGetComponent<Player>(out var enemy))
                return attackState;
        }
        return null;
    }

    public void EnterState()
    {
        anim.Play("Move");
    }

    public void ExitState()
    {
       rigid.linearVelocity = Vector3.zero;
    }

    public void OnUpdate()
    {
       rigid.linearVelocity = Vector3.left * moveSpeed;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position - new Vector3(attackRange / 2, 0), new Vector3(attackRange, attackRange));
    }
}
