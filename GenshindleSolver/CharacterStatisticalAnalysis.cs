namespace GenshindleSolver;

public class CharacterStatisticalAnalysis
{
    private List<Character> CharacterList;


    public enum CharProperty
    {
        Rarity,
        Element,
        Weapon,
        Region,
        Version,
    }

    public double SumFuncExpectedValue(int[] counts)
    {
        double expectedValue = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            expectedValue += counts[i] * counts[i];
        }
        expectedValue /= CharacterList.Count;
        return expectedValue;
    }

    public double SumFuncEntropy(int[] counts)
    {
        double entropy = 0; 
        for (int i = 0; i < counts.Length; i++)
        {
            if (counts[i] == 0) continue;

            double prob = counts[i] / (double)CharacterList.Count;
            entropy += -prob * Math.Log2(prob); 
        }
        return entropy;
    }
    
    public List<(Character, double)> RunStatisticalAnalysis(bool useEntropy = false)
    {

        var bucketResArr = BucketResult.GenBucketResults();

        List<(Character, double)> ExpectedValues = new();
        
        foreach (var character in CharacterList)
        {
            Bucket characterBucket = character.GetCharacterBucket();
            int[] counts = new int[bucketResArr.Length];
            
            foreach (var character2 in CharacterList)
            {
                Bucket character2Bucket = character2.GetCharacterBucket();
                
                BucketResult br = BucketResult.GenBucketResult(characterBucket, character2Bucket);
                int asKey = br.AsKey();
                counts[asKey]++;
            }

            var objectiveValue = useEntropy ? SumFuncEntropy(counts) : SumFuncExpectedValue(counts);
            
            ExpectedValues.Add((character, objectiveValue));
        }
        
        ExpectedValues.Sort((a, b) => a.Item2 < b.Item2 ? -1 : 1);
        if (useEntropy) ExpectedValues.Reverse();
        
        return ExpectedValues;
    }
    

    public static bool BucketKeyMatch(Bucket bucket1, Bucket bucket2, BucketResult bucketResult)
    {
        bool rarityMatch = !bucketResult.RarityResult ^ (bucket1.Rarity == bucket2.Rarity);
        bool elementMatch = !bucketResult.ElementResult ^ (bucket1.Element == bucket2.Element);
        bool weaponMatch = !bucketResult.WeaponResult ^ (bucket1.Weapon == bucket2.Weapon);
        bool regionMatch = !bucketResult.RegionResult ^ (bucket1.Region == bucket2.Region);
        bool versionMatch = bucketResult.VersionResult switch
        {
            HighMidLow.High => bucket2.Version > bucket1.Version,
            HighMidLow.Mid => bucket2.Version == bucket1.Version,
            HighMidLow.Low => bucket2.Version < bucket1.Version,
            _ => throw new ArgumentOutOfRangeException()
        };

        return rarityMatch && elementMatch && weaponMatch && regionMatch && versionMatch;
    }
    
    

    public CharacterStatisticalAnalysis(List<Character> characterList)
    {
        CharacterList = characterList;
    }
}