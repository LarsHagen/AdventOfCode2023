namespace Day12;

public class ConditionRecord
{
    public List<int> Groups;
    public string Condition;

    public ulong PossibleConditions = 0;
    
    public ConditionRecord(string input)
    {
        Condition = input.Split(" ")[0];
        
        Groups = input.Split(" ")[1].Split(",").Select(v => int.Parse(v)).ToList();
        CalculatePossibilities();
    }

    private void CalculatePossibilities()
    {
        PossibleConditions = RecursiveCalculate(Condition.ToArray(), 0);
    }

    private Dictionary<int, Dictionary<int, ulong>> cache = new();
    
    private ulong RecursiveCalculate(char[] condition, int i)
    {
        int primaryCacheKey = i;
        if (!cache.ContainsKey(primaryCacheKey))
        {
            cache.Add(primaryCacheKey, new());
        }
        
        //Check groups still valid
        List<int> groups = new();
        int currentGroup = 0;
        int unfinishedGroup = 0;
        
        for (int j = 0; j < condition.Length; j++)
        {
            if (condition[j] == '?')
            {
                if (currentGroup > 0)
                    unfinishedGroup = 1;
                
                currentGroup = 0;
                break;
            }
            
            if (condition[j] == '#')
            {
                currentGroup++;
            }
            else if (currentGroup > 0)
            {
                groups.Add(currentGroup);
                currentGroup = 0;
            }
        }
        
        if (currentGroup > 0)
        {
            groups.Add(currentGroup);
        }

        if (groups.Count + unfinishedGroup > Groups.Count)
            return 0;

        for (int j = 0; j < groups.Count; j++)
        {
            if (groups[j] != Groups[j])
                return 0;
        }

        if (groups.Count == Groups.Count && !condition.Contains('?'))
        {
            return 1;
        }

        if (unfinishedGroup == 0 && cache[primaryCacheKey].ContainsKey(groups.Count))
        {
            return cache[primaryCacheKey][groups.Count];
        }
        
        ulong v = 0;
        
        for (; i < condition.Length; i++)
        {
            if (condition[i] == '?')
            {
                char[] conditionA = new char[condition.Length];
                condition.CopyTo(conditionA, 0);
                conditionA[i] = '#';
                
                v += RecursiveCalculate(conditionA, i + 1);
                
                char[] conditionB = new char[condition.Length];
                condition.CopyTo(conditionB, 0);
                conditionB[i] = '.';
                v += RecursiveCalculate(conditionB, i + 1);

                break;
            }
        }

        if (unfinishedGroup == 0)
            cache[primaryCacheKey].Add(groups.Count, v);

        return v;
    }
}