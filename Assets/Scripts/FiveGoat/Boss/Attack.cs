using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float timeSkill;
    [SerializeField] private GameObject fireBall,Nam;
    [SerializeField] private float spaceXSkill, spaceYSkill;
    [SerializeField] private List<string> skillKeys;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject khien;
    private float timer;
    public static string KEY_ANIMATION_IDLE = "Idle";
   
    private bool isAnimationComplate;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeSkill)
        {
            if (HeartManager.instance.GetHeart()>=phanTramMau(50))
            {
                animator.Play(skillKeys[0]);
               
            }else if (HeartManager.instance.GetHeart() < phanTramMau(50))
            {
                int i = Random.Range(0, skillKeys.Count);
                animator.Play(skillKeys[i]);
            }
            timer = 0;
            Move.istan.SetTocDo(0);
        }
        if (isAnimationComplate)
        {
            animator.Play(KEY_ANIMATION_IDLE);
            HoanThanhAnimation();
        }
    }
    public void HoanThanhAnimation()
    {
        isAnimationComplate = !isAnimationComplate;
        Move.istan.SetTocDo(Move.istan.GetValueTocDo());
    }
    public void Skill1()
    {
        GameObject fire1,fire2;
        Vector3 pointSpawnA = new Vector3 (transform.localPosition.x+spaceXSkill, transform.localPosition.y + spaceYSkill);
        Vector3 pointSpawnB = new Vector3 (transform.localPosition.x+spaceXSkill*-1f, transform.localPosition.y + spaceYSkill/spaceYSkill);
        fire1 = Instantiate(fireBall, pointSpawnA, Quaternion.identity);
        fire2 = Instantiate(fireBall, pointSpawnB, Quaternion.identity);
        fire1.GetComponent<FireBallAttackLeft>().enabled = true;
        fire2.GetComponent<FireBallAttackRight>().enabled = true;
    }
    public void Skill2()
    {
        GameObject enemy=
        Instantiate(Nam,spawnPoint.position,Quaternion.identity);
        Destroy(enemy,3f);
    }
    public void Skill3()
    {
        int i = Random.Range(0, 2);
        if (i==0)
        {
            HeartManager.instance.SetHeart(10);
        }
        else
        {
            khien.SetActive(true);
        }
       

    }
    public float phanTramMau(float phanTram)
    {
        return (HeartManager.instance.getMaxHeart()* phanTram) / 100;
    }
}
