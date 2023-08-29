using CongTDev.ObjectPooling;
using UnityEngine;

public class PlayerLevel : GlobalReference<PlayerLevel>
{
    [SerializeField] LevelUI levelUI;
    [SerializeField] Character character;
    [SerializeField] AudioClip _lvUP;
    [SerializeField] Prefab prefab;
    [SerializeField] Transform spawnPosition;
    public int maxLevel = 10;
    public int currentLevel = 1;
    public int currentExperience = 0;
    public int[] experienceToLevelUp;
    public void GainExperience(int amount)
    {
        if (currentLevel < experienceToLevelUp.Length)
        {
            currentExperience += amount;
            while (currentLevel < maxLevel && currentExperience >= experienceToLevelUp[currentLevel - 1])
            {
                LevelUp();
                OnLevelUp();
            }
        }
        else if (currentLevel > experienceToLevelUp.Length)
        {
            currentExperience = 0;

        }
        else
        {
            Debug.Log("Player Reach Max Level");
        }
    }
    private void LevelUp()
    {
        currentExperience -= experienceToLevelUp[currentLevel - 1];
        currentLevel++;
        SoundManager.Instance.PlaySound(_lvUP);
        SpawnEffect();
    }
    private void Update()
    {
        if (currentLevel < maxLevel)
        {
            levelUI.SetCurrentlevel(currentLevel);
        }
        else
        {
            levelUI.SetCurrentlevel(maxLevel);
        }
    }
    private void SpawnEffect()
    {
        if (PoolManager.Get<PoolObject>(prefab, out var instance))
        {
            instance.transform.position = spawnPosition.position;
            instance.transform.SetParent(transform);
            instance.transform.localScale = Vector3.one;
        }
    }
    private void OnLevelUp()
    {
        int levelValue = currentLevel * 2;

        character.Strength.BaseValue += levelValue;
        character.Intelligence.BaseValue += levelValue;
        character.Agility.BaseValue += levelValue;
        character.Dexterity.BaseValue += levelValue;

        character.MaxHealth += 5 * (int)character.Agility.BaseValue;
        character.MaxMana += 2 * (int)character.Intelligence.BaseValue;

        character.UpdateStatValues();
    }
}
