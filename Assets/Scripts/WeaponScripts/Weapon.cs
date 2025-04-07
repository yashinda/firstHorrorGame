using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public PistolSounds pistolSounds;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private float shootForce;
    public int bulletsInMag;
    [SerializeField] private int bulletsInInventory = 100;
    [SerializeField] private int maxBulletsInMag = 9;
    [SerializeField] private int maxBulletsInInventory = 100;
    [SerializeField] private TMP_Text currentAmmoText;
    [SerializeField] private TMP_Text ammoInventoryText;
    public InventoryManager inventoryManager;

    private void Start()
    {
        bulletsInMag = maxBulletsInMag;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && bulletsInMag > 0 && !inventoryManager.isOpen)
        {
            Shoot();
        }
        else if (Input.GetMouseButtonDown(0) && bulletsInMag == 0 && !inventoryManager.isOpen)
        {
            pistolSounds.EmptyMag();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        currentAmmoText.text = bulletsInMag.ToString();
        ammoInventoryText.text = bulletsInInventory.ToString();
    }

    private void Shoot()
    {
        Debug.Log("Пуля вылетела");

        bulletsInMag--;
        pistolSounds.Shooting();

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(150.0f);

        Vector3 directionBullet = targetPoint - spawnBulletPosition.position;

        GameObject currentBullet = Instantiate(bulletPrefab, spawnBulletPosition.position, Quaternion.identity);

        currentBullet.transform.forward = directionBullet.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionBullet.normalized * shootForce, ForceMode.Impulse);
    }

    private void Reload()
    {
        pistolSounds.Reloading();
        int reloadAmount = maxBulletsInMag - bulletsInMag;
        reloadAmount = (bulletsInInventory - reloadAmount) >= 0 ? reloadAmount : bulletsInInventory;
        bulletsInMag += reloadAmount;
        bulletsInInventory -= reloadAmount;
    }

    public void AddAmmo(int ammoAmount)
    {
        bulletsInInventory += ammoAmount;
        if (bulletsInInventory > maxBulletsInInventory)
        {
            bulletsInInventory = maxBulletsInInventory;
        }
    }
}
