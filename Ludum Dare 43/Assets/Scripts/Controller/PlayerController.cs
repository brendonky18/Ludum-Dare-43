using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //all the stats that I could think of
    private Dictionary<Stats, int> playerStats = new Dictionary<Stats, int> {
        {Stats.Debt, 0 },
        {Stats.Family, 50 },
        {Stats.Friends, 10 },
        {Stats.Grades, 75 },
        {Stats.Happiness, 50 },
        {Stats.Health, 90 },
        {Stats.Money, 1000 },
        {Stats.SocialLife, 50 },
        {Stats.Stress, 0}
    };

    public int Debt {
        get { return playerStats[Stats.Debt]; }
        set { playerStats[Stats.Debt] = value; }
    }
    public int Family {
        get { return playerStats[Stats.Family]; }
        set { playerStats[Stats.Family] = value; }
    }
    public int Friends {
        get { return playerStats[Stats.Friends]; }
        set { playerStats[Stats.Friends] = value; }
    }
    public int Grades {
        get { return playerStats[Stats.Grades]; }
        set { playerStats[Stats.Grades] = value; }
    }
    public int Happiness {
        get { return playerStats[Stats.Happiness]; }
        set { playerStats[Stats.Happiness] = value; }
    }
    public int Health {
        get { return playerStats[Stats.Health]; }
        set { playerStats[Stats.Health] = value; }
    }
    public int Money {
        get { return playerStats[Stats.Money]; }
        set { playerStats[Stats.Money] = value; }
    }
    public int SocialLife {
        get { return playerStats[Stats.SocialLife]; }
        set { playerStats[Stats.SocialLife] = value; }
    }
    public int Stress {
        get { return playerStats[Stats.Stress]; }
        set { playerStats[Stats.Stress] = value; }
    }
}
