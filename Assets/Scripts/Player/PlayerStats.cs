using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;
    public GameObject SoundBearer;
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;

   // public HpBarController healthbar;
   // public XPBarController xpbar;
    public XPBarController xpbar_player;
    public HpBarController healthbar_player;

    #region Current Stats Proprieties

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                if (GameManager.instance != null)  //make sure Gamemnager has been instantiated prior to Player stats script acces it.
                {
                    GameManager.instance.currentHealtDisplay.text = "Health: " + currentHealth;
                }
            }
        }
    }
    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }
        }
    }
    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "MovementSpeed: " + currentMoveSpeed;
                }
                // Debug.Log("Movemet speed has been changed");
            }
        }
    }
    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
                }
            }
        }
    }
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "ProjectileSpeed: " + currentProjectileSpeed;
                }
            }
        }
    }
    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnet range: " + currentMagnet;
                }
            }
        }
    }
    #endregion

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;
    public int experienceCapIncrease;
    public float experienceMultiplier = 1.2f;
    //Class for defining a level range and the corresponding experience cap increase for that range
    //[System.Serializable]
    //public class LevelRange
    //{
    //    public int startLevel;
    //    public int endLevel;
    //    public int experienceCapIncrease;
    //}

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    //public List<LevelRange> levelRanges;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    public GameObject secondWeaponTest;
    public GameObject firstPassiveItemTest, secondPassiveItemTest;
    [SerializeField]
    private AudioSource hitSoundEffect;

    private void Awake()
    {
        characterData = CharacterSelector.GetData();
        GetComponent<Animator>().runtimeAnimatorController = CharacterSelector.GetAnimator();
        GetComponent<SpriteRenderer>().sprite = CharacterSelector.GetSprite();
        CharacterSelector.instance.DestroySingleton();
        

        inventory = GetComponent<InventoryManager>();

        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;

        SpawnWeapon(characterData.StartingWeapon);
        SpawnWeapon(secondWeaponTest);
        SpawnPassiveItem(firstPassiveItemTest);
        SpawnPassiveItem(secondPassiveItemTest);
    }
    private void Start()
    {
        //Initialize the experience cap as the first experience cap increase
        experience = 0;
        experienceCapIncrease = 100;
        experienceCap = 100;
        GameManager.instance.index = characterData.Index;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.instance.currentHealtDisplay.text = "Health: " + currentHealth;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed : " + currentProjectileSpeed;
        GameManager.instance.currentMoveSpeedDisplay.text = "Movement Speed: " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
        GameManager.instance.currentLevelDisplay.text = "" + level;
        GameManager.instance.AssignChosenCharacterUI(characterData);
      //  healthbar.SetMaxHealth(CurrentHealth);
      //  xpbar.SetMaxXp(experienceCap);

     //   xpbar.SetXp(0);
        healthbar_player.SetMaxHealth(CurrentHealth);
        xpbar_player.SetMaxXp(experienceCap);

        xpbar_player.SetXp(0);
    }

    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvincible)
        {
            isInvincible = false;
        }
        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        if (experience < experienceCap)
        {
            //xpbar.SetXp(experience);
            xpbar_player.SetXp(experience);
        }
        LevelUpChecker();
    }

    void updateExperienceCapIncrease()
    {
        // Calculate the xp cap increase using a non linear formula
        experienceCapIncrease = Mathf.RoundToInt(experienceCapIncrease * Mathf.Sqrt(Mathf.Pow(experienceMultiplier, Mathf.Sqrt(level))));
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            SoundBearer.GetComponent<AudioSource>().Play();
            GameManager.instance.currentLevelDisplay.text = "" + level;
            experience -= experienceCap;
            updateExperienceCapIncrease();
            experienceCap += experienceCapIncrease;
            xpbar_player.SetMaxXp(experienceCap);
            xpbar_player.SetXp(experience);
            GameManager.instance.LevelUp();
        }
    }
    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            GetComponent<PlayerAnimator>().TriggerHit();
            CurrentHealth -= dmg;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
          //  healthbar.SetHealth(CurrentHealth);
            healthbar_player.SetHealth(CurrentHealth);

            if (CurrentHealth <= 0)
            {
                Kill();
            }
            else
            {
                hitSoundEffect.Play();
            }
        }
    }
    public void Kill()
    {
        if (!GameManager.instance.isOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignTimeSurvived();
            GameManager.instance.AssignChosenWeaponAndPassiveItemUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
            GameManager.instance.GameOver();
        }
        // Debug.Log("PLAYER IS DEAD");
    }

    public void RestoreHealth(float amount)
    {

        CurrentHealth += amount;
 
        if (CurrentHealth > characterData.MaxHealth)
        {
           
            CurrentHealth = characterData.MaxHealth;
            healthbar_player.SetHealth(CurrentHealth);
        }
        else
        {
            healthbar_player.SetHealth(CurrentHealth);
        }
    }
    void Recover()
    {
        if (CurrentHealth < characterData.MaxHealth)
        {

            CurrentHealth += CurrentRecovery * Time.deltaTime;
            healthbar_player.SetHealth(CurrentHealth);
        }
    }
    public void SpawnWeapon(GameObject weapon)
    {
        if (weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());

        weaponIndex++;
    }
    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;
    }
}
