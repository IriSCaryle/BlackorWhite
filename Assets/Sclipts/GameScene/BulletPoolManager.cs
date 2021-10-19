using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    [SerializeField] List<GameObject> bullets;
    [SerializeField] List<Bullet> bulletsScripts;
    [SerializeField] int bulletLimits;
    [SerializeField] float bulletLifeTime;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject defaultPos;
    // Start is called before the first frame update
    void Start()
    {
        InitBullet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitBullet()
    {
        for(int i = 0; i <= bulletLimits; i++)
        {
            GameObject tmpBullet = Instantiate(bullet, defaultPos.transform.position, Quaternion.identity, gameObject.transform);
            Bullet tmpBulletScript = tmpBullet.GetComponent<Bullet>();
            tmpBulletScript.isActive = false;
            tmpBulletScript.defaultPos = defaultPos.transform.position;
            tmpBulletScript.life = bulletLifeTime;
            bulletsScripts.Add(tmpBulletScript);
            bullets.Add(tmpBullet);
            tmpBullet.SetActive(false);
        }
    }

    public GameObject FindBullet()
    {
        for(int i = 0; i <= bulletLimits; i++)
        {
            if (!bulletsScripts[i].isActive)
            {
                bulletsScripts[i].isActive = true;
                bullets[i].SetActive(true);
                return bullets[i];
            }
            
        }

        return null;
    }
}
