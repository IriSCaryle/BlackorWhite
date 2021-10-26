using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ブロックを管理するスクリプト
/// </summary>
public class BulletPoolManager : MonoBehaviour
{
    [SerializeField] List<GameObject> bullets;//ブロック
    [SerializeField] List<Bullet> bulletsScripts;//各ブロックのスクリプト
    [SerializeField] int bulletLimits;//生成する弾の数
    [SerializeField] float bulletLifeTime;//弾の生存時間
    [SerializeField] GameObject bullet;//ブロックのプレハブ
    [SerializeField] GameObject defaultPos;//プール位置
    [SerializeField] PleyerSclipt playerScript;
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
            Invert tmpInvert = tmpBullet.GetComponent<Invert>();
            tmpInvert.playerSclipt = playerScript;
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
