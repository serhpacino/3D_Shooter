using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Shotgun : MonoBehaviour
{

    public GameObject bloodSplash;
    public Sprite idleShotgun;
    public Sprite shotShotgun;
    public Sprite anim1Shotgun;
    public Sprite anim2Shotgun;
    public float shotgunDamage;
    public float shotgunRange;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;
    public Text ammoText;

    public int ammoAmount;
    public GameObject bulletHole;
    int ammoLeft;
    bool isShot;
    bool isReloading;
    AudioSource source;
    void Awake()
    {
        source = GetComponent<AudioSource>();
        ammoLeft = ammoAmount;
    }
    private void OnEnable()
    {
        isReloading = false;
        isShot = false;
    }
    void Update()
    {
        ammoText.text =  "Ammo:"+ ammoLeft;
        if (Input.GetButtonDown("Fire1") && isReloading == false)
            isShot = true;
    }
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (isShot == true && ammoLeft > 0 && isReloading == false)
        {
            isShot = false;
            ammoLeft--;
            source.PlayOneShot(shotSound);
            
            if (Physics.Raycast(ray, out hit, shotgunRange))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Instantiate(bloodSplash, hit.point, Quaternion.identity);
                    if (hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().patrolState ||
                        hit.collider.gameObject.GetComponent<EnemyStates>().currentState == hit.collider.gameObject.GetComponent<EnemyStates>().alertState)
                    {
                        hit.collider.gameObject.SendMessage("HiddenShot", transform.parent.position, SendMessageOptions.DontRequireReceiver);
                    }
                    hit.collider.gameObject.SendMessage("AddDamage", shotgunDamage, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.parent = hit.collider.gameObject.transform;
                }
                
            }
            StartCoroutine("ShotReloadWeapon");
        }
        else if (isShot == true && isReloading == false)
        {
           
            isShot = false;
            Reload();
        }
    }
    void Reload()
    {   if (ammoLeft <= 0)
        {
            source.PlayOneShot(emptyGunSound);
        }

    }
    IEnumerator ShotReloadWeapon()
    {
        isReloading = true;
        GetComponent<SpriteRenderer>().sprite = shotShotgun;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = anim1Shotgun;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = anim2Shotgun;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = anim1Shotgun;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = idleShotgun;
        yield return new WaitForSeconds(0.1f);
        isReloading = false;
    }
   
}
