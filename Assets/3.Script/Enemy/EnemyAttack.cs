using UnityEngine;

public class EnemyAttack : MonoBehaviour,IState
{
    private int damage;
    private float attackSpeed;
    private float attackRange;
    private float curTime;
    private IState moveState;
    private IGetDamage player;
    private Animator anim;
    public void Init(EnemyStat stat)
    {
        damage = stat.Damage;
        attackSpeed = stat.AttackSpeed;
        attackRange = stat.AttackRange;
        moveState = GetComponent<EnemyMove>();
        anim = GetComponent<Animator>();
    }

    public IState CheckTransition()
    {
        if (GetTarget() == null)
            return moveState;
        else
            return null;
    }

    public void EnterState()
    {
        anim.Play("Attack");
    }

    public void ExitState()
    {

    }

    public void OnUpdate()
    {
       
    }

    private void CheckCanAttack()
    {
        if (player == null)
        {
            player = GetTarget();
            if (player == null)
                return;
        }
        if (curTime <= 0)
        {
            Debug.Log("몬스터 공격");
            player.Hp -= damage;
            curTime = attackSpeed;
        }
        else
            curTime -= Time.deltaTime;
    }

    public void Attack()
    {
        CheckCanAttack();
    }

    private IGetDamage GetTarget()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position - new Vector3(attackRange / 2, 0), new Vector3(attackRange, attackRange), 0);
        foreach (Collider2D col in cols)
        {
            if (col.TryGetComponent<PlayerStat>(out var enemy))
                return enemy.GetComponent<IGetDamage>();
        }
        return null;
    }

}
