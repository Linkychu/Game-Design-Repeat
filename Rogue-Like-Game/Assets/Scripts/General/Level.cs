

using System;



namespace General
{
    

    [Serializable]
    public class Level
    {
        public int experience;
        public int currentLevel;
        public Action OnLevelUp;

        public int MAX_EXP;
        public int MAX_LEVEL = 100;

        public Level(int level, Action OnLevUp)
        {
            MAX_EXP = GetXPforLevel(MAX_LEVEL);
            currentLevel = level;
            experience = GetXPforLevel(level);
            OnLevelUp = OnLevUp;
        }

        public void AddLevel(int level)
        {

            if ((OnLevelUp != null) && level < MAX_LEVEL)
            {
                OnLevelUp.Invoke();
            }
        }
        public int GetXPforLevel(int level)
        {
            if (level > MAX_LEVEL)
                return 0;

            var firstPass = 0;
            var secondPass = 0;
            for (var levelCycle = 1; levelCycle < level; levelCycle++)
            {
                firstPass += (int) Math.Floor(levelCycle + 300.0f * Math.Pow(2.0f, levelCycle / 7.0f));
                secondPass = firstPass / 4;
            }

            if (secondPass > MAX_EXP && MAX_EXP != 0)
                return MAX_EXP;

            if (secondPass < 0)
                return MAX_EXP;

            return secondPass;
        }

        public int GetLevelForXP(int exp)
        {
            if (exp > MAX_EXP)
                return MAX_EXP;

            var firstPass = 0;
            var secondPass = 0;
            for (var levelCycle = 1; levelCycle <= MAX_LEVEL; levelCycle++)
            {
                firstPass += (int) Math.Floor(levelCycle + 300.0f * Math.Pow(2.0f, levelCycle / 7.0f));
                secondPass = firstPass / 4;
                if (secondPass > exp)
                    return levelCycle;
            }

            if (exp > MAX_LEVEL)
                return MAX_LEVEL;

            return 0;
        }

        public bool AddExp(int amount)
        {
            if (amount + experience < 0 || experience > MAX_EXP)
            {
                if (experience > MAX_EXP)
                    experience = MAX_LEVEL;
                return false;
            }

            var oldLevel = GetLevelForXP(experience);
            experience += amount;
            if (oldLevel < GetLevelForXP(experience))
                if (currentLevel < GetLevelForXP(experience))
                {
                    currentLevel = GetLevelForXP(experience);
                    if (OnLevelUp != null)
                        OnLevelUp.Invoke();
                    return true;
                }

            return false;
        }
    }
}