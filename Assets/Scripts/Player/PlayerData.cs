using System;

[Serializable]
public class PlayerData
{
    public int playerLevel;
    public int playerExperience;
    public int playerCoins;
    public int BlueBottle;
    public int GreenBottle;
    public int RedBottle;
    public int BrounBottle;
    public int Food;
    public int Mushrooms;

    public PlayerData(int level, int experience, int coins, int blueBottle, int greenBottle, int redBottle, int brounBottle, int food, int mushrooms)
    {
        playerLevel = level;
        playerExperience = experience;
        playerCoins = coins;
        BlueBottle = blueBottle;
        GreenBottle = greenBottle;
        RedBottle = redBottle;
        BrounBottle = brounBottle;
        Food = food;
        Mushrooms = mushrooms;
    }

    public void AddExperience(int amount)
    {
        playerExperience += amount;
        CheckLevelUp();
    }

    public void AddCoins(int amount)
    {
        playerCoins += amount;
    }

    public void AddBlueBottle(int amount)
    {
        BlueBottle += amount;
    }

    public void AddGreenBottle(int amount)
    {
        GreenBottle += amount;
    }

    public void AddRedBottle(int amount)
    {
        RedBottle += amount;
    }

    public void AddBrounBottle(int amount)
    {
        BrounBottle += amount;
    }

    public void AddFood(int amount)
    {
        Food += amount;
    }

    public void AddMushrooms(int amount)
    {
        Mushrooms += amount;
    }

    private void CheckLevelUp()
    {
        int experienceToNextLevel = playerLevel * 100;
        if (playerExperience >= experienceToNextLevel)
        {
            playerExperience -= experienceToNextLevel;
            playerLevel++;
        }
    }
}