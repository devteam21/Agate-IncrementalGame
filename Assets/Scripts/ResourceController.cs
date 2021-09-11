using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour

{
    public AudioSource AudioUpgrade;
    public AudioSource AudioFailUpgrade;
    public Text ResourceDescription;
    public Text ResourceUpgradeCost;
    public Text ResourceUnlockCost;
    public Button ResourceButton;
    public Image ResourceImage;

    private ResourceConfig _config;

    private int _level = 1;
    public bool IsUnlocked { get; private set; }

    private void Start ()

    {

        ResourceButton.onClick.AddListener (() =>

        {

            if (IsUnlocked)

            {

                UpgradeLevel ();
                
                

            }

            else

            {
                
                UnlockResource ();

            }

        });

        ResourceButton.onClick.AddListener (UpgradeLevel);

    }

    public void SetConfig (ResourceConfig config)

    {

        _config = config;

        // ToString("0") berfungsi untuk membuang angka di belakang koma

        ResourceDescription.text = $"{ _config.Name } LV. { _level }\n+{ GetOutput ().ToString ("0") }";

        ResourceUnlockCost.text = $"BIAYA MEMBUKA\n{ _config.UnlockCost }";

        ResourceUpgradeCost.text = $"BIAYA PENINGKATAN\n{ GetUpgradeCost () }";

        SetUnlocked (_config.UnlockCost == 0);

    }

 

    public double GetOutput ()

    {

        return _config.Output * _level;

    }

 

    public double GetUpgradeCost ()

    {

        return _config.UpgradeCost * _level;

    }

 

    public double GetUnlockCost ()

    {

        return _config.UnlockCost;

    }

     public void UpgradeLevel ()

    {

        double upgradeCost = GetUpgradeCost ();

        if (GameManager.Instance._totalGold < upgradeCost)

        {

            return;

        }



        GameManager.Instance.AddGold (-upgradeCost);

        _level++;
        AudioUpgrade.Play();

 

        ResourceUpgradeCost.text = $"BIAYA PENINGKATAN\n{ GetUpgradeCost () }";

        ResourceDescription.text = $"{ _config.Name } LV. { _level }\n+{ GetOutput ().ToString ("0") }";

    }

    // Script by Devaldi Akbar Suryadi - 2021
    // https://github.com/devteam21

    public void UnlockResource ()

    {

        double unlockCost = GetUnlockCost ();

        if (GameManager.Instance._totalGold < unlockCost)

        {

            return;

        }

 

        SetUnlocked (true);

        GameManager.Instance.ShowNextResource ();

        AchievementController.Instance.UnlockAchievement (AchievementType.UnlockResource, _config.Name);

    }

 

    public void SetUnlocked (bool unlocked)

    {

        IsUnlocked = unlocked;

        ResourceImage.color = IsUnlocked ? Color.white : Color.grey;

        ResourceUnlockCost.gameObject.SetActive (!unlocked);

        ResourceUpgradeCost.gameObject.SetActive (unlocked);

    }


}