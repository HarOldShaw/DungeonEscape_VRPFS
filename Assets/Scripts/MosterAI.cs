using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterAI : MonoBehaviour
{
    public bool viewFlag;
    public GameObject target;
    public Animator anim;
    public float attackDis;
    public int viewAngle;
    public float viewRadius;
    public float attackRadius;
    public float moveSpeed;
    private float timer = 10f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (this.tag == "Boss")
        {
            StartCoroutine(MosterBossGo());
        }else if(this.tag == "Solider")
        {
            StartCoroutine(MosterGO());
        }
    }
    private void FixedUpdate()
    {
   
    }
    //寻路坐标点数组
    public Transform[] points;
    //坐标索引
    public int pointIndex = 0;

    void MoveSelf()
    {
        //看向选定坐标点
        anim.SetBool("Moving",true);
        Vector3 nextPosition = points[pointIndex].position;
        this.transform.LookAt(nextPosition);
        //前进,自身坐标系，使用Vector3.forward即可
        this.transform.Translate(moveSpeed * Vector3.forward * Time.deltaTime, Space.Self);
        //判断距离
        if (Vector3.Distance(this.transform.position, nextPosition) < 0.1)
        {
            //更换下一个坐标点
            pointIndex = Random.Range(0, points.Length);

        }
    }

    void FollowTarget()
    {
        //获取调用时目标坐标
        anim.SetBool("Moving", true);
        //anim.SetBool("Runing", true);
        Vector3 targetPostion = new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z);
        // Debug.Log(targetPostion);
        this.transform.LookAt(targetPostion);
        this.transform.Translate(moveSpeed * Vector3.forward * Time.deltaTime, Space.Self);
    }


    void Death()
    {
        anim.SetBool("Death", true);
        AnimatorStateInfo animatorInfo;
        animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
        if ((animatorInfo.normalizedTime > 1.0f))
        {
            this.gameObject.SetActive(false);
        }
    }

    void Attack()
    {
        //自由添加内容，例如动画播放等
        //需要注意动画播放等可能需要判断攻击状态是否完成再执行下一次攻击
        //否则在攻击范围内每帧都会打一下 
        anim.SetTrigger("Attack");

        
    }

    void SpecialAttack()
    {
        //计算距离
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        //求怪物指向角色的向量
        Vector3 myVector3 = target.transform.position - this.transform.position;
        //计算两个向量的角度
        float angle = Vector3.Angle(myVector3, this.transform.forward);
        //距离小于半径
        if (distance <= viewRadius && angle <= viewAngle / 2)
        {
            Debug.Log("SpecialAttackHurt!!!");
        }
        anim.SetTrigger("SpecialAttack");
    }

    IEnumerator MosterGO()
    {
        while (true)
        {
            //等待固定帧更新完成
            yield return new WaitForFixedUpdate();
            if (viewFlag)
            {
                //求二者距离
                float distance = Vector3.Distance(target.transform.position, this.transform.position);
                AnimatorStateInfo animatorInfo;
                if (distance <= attackDis) {
                    //anim.SetBool("Runing", false);
                    //anim.SetBool("Moving", false);
                    //animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
                   /* if ((animatorInfo.normalizedTime > 1.0f))
                    {     
                    }*/
                    Debug.Log("Attack");
                    Attack();
                    yield return new WaitForSeconds(2f);
                    continue;
                   
                }
                FollowTarget();
                continue;
            }
            //没有看到目标，自由移动
            MoveSelf();
        }
    }

    IEnumerator SpecialAttackCountDown(){
        timer -= Time.deltaTime;
        if(timer<=0){
            SpecialAttack();
        }
        yield return new WaitForFixedUpdate();
        timer = 20f;
    }

    IEnumerator MosterBossGo()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (viewFlag)
            {   
                float distance = Vector3.Distance(target.transform.position, this.transform.position);
                
                timer -= Time.deltaTime;
                AnimatorStateInfo animatorInfo;
                Debug.Log(timer);
                // StartCoroutine(SpecialAttackCountDown());
                if (distance <= attackDis)
                {
                    //anim.SetBool("Runing", false);
                    //anim.SetBool("Moving", false);
                    //animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
                    /* if ((animatorInfo.normalizedTime > 1.0f))
                     {     
                     }*/
                    Attack();
                    yield return new WaitForSeconds(2f);
                    continue;

                }
                if (timer <= 0)
                {
                    /*animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
                    if ((animatorInfo.normalizedTime > 1.0f))
                    {
                        SpecialAttack();
                    }*/
                    SpecialAttack();
                    yield return new WaitForSeconds(3.0f);
                    timer = 10f;
                    continue;
                }
                FollowTarget();     
                continue;

            }
            timer = 20f;
            MoveSelf();
        }
    }


}
