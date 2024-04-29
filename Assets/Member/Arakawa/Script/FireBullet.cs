using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class FireBullet : MonoBehaviour
{
    [SerializeField]
    //’e‚Ì”­ËêŠ
    private GameObject firingPoint;

    [SerializeField]
    //‚½‚Ü
    private GameObject bullet;

    [SerializeField]
    //’e‚Ì‘¬‚³
    private float speed = 30f;

    //private float _repeatSpan;
    //private float _timeElapsed;
    public int maxInstance = 1;
    private int currentInstance = 0;

    void Start()
    {
        //_repeatSpan = 3;
        //_timeElapsed = 0;
    }

    private void Update()
    {
        //_timeElapsed += Time.deltaTime;

        if(currentInstance < maxInstance)
        {
            Vector3 bulletPosition = firingPoint.transform.position;
            currentInstance++;
            //’e‚ğ”­Ë‚·‚éêŠ
            //Vector3 bulletPosition = firingPoint.transform.position;
            //ã‚Åæ“¾‚µ‚½êŠ‚É"bullet"‚Ìprefab‚ğoŒ»‚³‚¹‚éABullet‚ÌŒü‚«
            GameObject newBullet = Instantiate(bullet, bulletPosition, this.gameObject.transform.rotation);
            //oŒ»‚³‚¹‚½’e‚Ìup(y)‚ğæ“¾
            Vector3 direction = newBullet.transform.up;
            //’e‚Ì”­Ë•ûŒü‚ÉnewBall‚ÌY•ûŒü‚ğ“ü‚êA’e‚ÌƒIƒuƒWƒFƒNƒg‚Ìrigidoby‚ÉÕŒ‚—Í‚ğ‰Á‚¦‚é
            newBullet.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
            //oŒ»‚³‚¹‚½’e‚Ì‚È‚Ü‚ğ"bullet"‚É•ÏX
            newBullet.name = bullet.name;
            //oŒ»‚³‚¹‚½’e‚ğ5•bŒã‚ÉÁ‚·
            Destroy(newBullet, 5f);

            //_timeElapsed = 0;
        }
        
    }
    
}
