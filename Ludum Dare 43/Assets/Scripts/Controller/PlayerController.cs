using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //all the stats that I could think of
    private Dictionary<Stats, float> playerStats = new Dictionary<Stats, float> {
        {Stats.Family, 50 },
        {Stats.Friends, 10 },
        {Stats.Grades, 75 },
        {Stats.Happiness, 50 },
        {Stats.Health, 90 },
        {Stats.Money, 1000 },
        {Stats.Relationship, -1 },
        {Stats.SocialLife, 50 },
        {Stats.Stress, 0}
    };

    public void ModifyStat(Stats stat, float value) {
        switch (stat) {
            case Stats.Family:
                Family += value;
                return;
            case Stats.Friends:
                Friends += value;
                return;
            case Stats.Grades:
                Grades += value;
                return;
            case Stats.Happiness:
                Happiness += value;
                return;
            case Stats.Health:
                Health += value;
                return;
            case Stats.Money:
                Money += value;
                return;
            case Stats.Relationship:
                Relationship += value;
                return;
            case Stats.SocialLife:
                SocialLife += value;
                return;
            case Stats.Stress:
                Stress += value;
                return;
            default:
                Debug.LogError("Something went wrong in PlayerController.ModifyStats()");
                return;
        }
    }

    private List<Modifier> allModifiers = new List<Modifier>();
    public void AddModifier(Modifier m) {
        allModifiers.Add(m);
    }


    public float Family {
        get { return playerStats[Stats.Family]; }
        set { playerStats[Stats.Family] = value; }
    }
    public float Friends {
        get { return playerStats[Stats.Friends]; }
        set {
            playerStats[Stats.Friends] = Clamp(value, 0, float.MaxValue);
            SocialLife += value * 5;//can modify later
        }
    }
    public float Grades {
        get { return playerStats[Stats.Grades]; }
        set {
            playerStats[Stats.Grades] = Clamp(value, 0, 100);
        }
    }
    public float Happiness {
        get { return playerStats[Stats.Happiness]; }
        set { playerStats[Stats.Happiness] = Clamp(value, 0, 100); }
    }
    public float Health {
        get { return playerStats[Stats.Health]; }
        set { playerStats[Stats.Health] = Clamp(value, 0, 100); }
    }

    public float Money {
        get { return playerStats[Stats.Money]; }
        set {
            if (playerStats[Stats.Money] + value < 0)
                debt += playerStats[Stats.Money] + value;
            playerStats[Stats.Money] = Clamp(value, 0, float.MaxValue);
        }
    }
    private float debt = 0;//hidden modifier
    private float Debt {
        get { return debt; }
        set {
            debt = value;
            //some rational equation to model stress
        }
    }

    private bool hasRelationship = false;
    public float Relationship {
        get { return playerStats[Stats.Relationship]; }
        set {
            hasRelationship = value != -1;
            playerStats[Stats.Relationship] = Clamp(value, 0, 100);
        }
    }
    public float SocialLife {
        get { return playerStats[Stats.SocialLife]; }
        set {
            playerStats[Stats.SocialLife] = Clamp(value, 0, 100);
        }
    }
    public float Stress {
        get { return playerStats[Stats.Stress]; }
        set { playerStats[Stats.Stress] = Clamp(value, 0, 100); }
    }

    
    [SerializeField] private List<string> getFriendName = new List<string>();
    public List<string> GetFriendName {
        get {
            return getFriendName;
        }

        set {
            getFriendName = value;
        }
    }

    public string RandomFriendName {
        get { return getFriendName[UnityEngine.Random.Range(0, getFriendName.Count)]; }
    }

    public void Start() {
        World.Instance.OnDayChange = () => {//checks to see which modifiers have expired
            allModifiers.RemoveAll((curModifier) => {//remove all modifiers that have expired
                return curModifier.IsExpired;
            });
        };
    }

    private static float Clamp(float value, float min, float max) {
        return (value < min) ? min : (value > max) ? max : value;
    }
}
