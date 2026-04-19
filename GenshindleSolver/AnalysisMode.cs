namespace GenshindleSolver;

public static class AnalysisMode
{


    public static void RunRandomizedHMAnalysis(List<Character> characters, bool useEntropy)
    {
        int[] counters = new int[5];
        int totalIters = 0;
        Random rand0 = new Random();
        int seed = 1057392914; // rand0.Next();
        Random random = new Random(seed);
        while (totalIters < 10000000)
        {
            
            var c1 = characters[random.Next(characters.Count)].GetCharacterBucket();
            var c2 = characters[random.Next(characters.Count)].GetCharacterBucket();
            var c3 = characters[random.Next(characters.Count)].GetCharacterBucket();
            var targetChar = characters[random.Next(characters.Count)];
            var tBucket = targetChar.GetCharacterBucket();
            
            var r1 = BucketResult.GenBucketResult(c1, tBucket);
            var r2 = BucketResult.GenBucketResult(c2, tBucket);
            var r3 = BucketResult.GenBucketResult(c3, tBucket);
            
            
            List<Character> nList = new();
            foreach (var c in characters)
            {
                var cBucket = c.GetCharacterBucket();
                if (
                    CharacterStatisticalAnalysis.BucketKeyMatch(c1, cBucket, r1)
                    && CharacterStatisticalAnalysis.BucketKeyMatch(c2, cBucket, r2)
                    && CharacterStatisticalAnalysis.BucketKeyMatch(c3, cBucket, r3)
                    )
                {
                    nList.Add(c);
                }
            }
            
            CharacterStatisticalAnalysis analysis = new CharacterStatisticalAnalysis(nList);
            var init = analysis.RunStatisticalAnalysis(useEntropy);

            bool exitEarly = false;
            for (int i = 0; i < 4; i++)
            {
                if (init.Count == 1)
                {
                    counters[i]++;
                    exitEarly = true;
                    break;
                }
                var ck = init[0].Item1.GetCharacterBucket();
                var res = BucketResult.GenBucketResult(ck, tBucket);
                List<Character> nlist2 = new();
                foreach (var (c, _) in init)
                {
                    if (CharacterStatisticalAnalysis.BucketKeyMatch(
                            ck, 
                            c.GetCharacterBucket(), 
                            res
                        ))
                    {
                        nlist2.Add(c);
                    }
                }
                analysis = new CharacterStatisticalAnalysis(nlist2);
                init = analysis.RunStatisticalAnalysis(useEntropy);
            }

            if (!exitEarly)
            {
                counters[4]++;
            }
            totalIters++;
        }
        
        Console.WriteLine($"Analysis Results after {totalIters} iterations, useEntropy={useEntropy}");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"{i}: {counters[i]}, {counters[i] * 100 / (double)totalIters:F4}");
        }
    }
    
    public static void RunAnalysis(List<Character> characters, bool useEntropy)
    {
        int[] counters = new int[7];
        int totalIters = 0;

        foreach (var targetChar in characters)
        {
            var tBucket = targetChar.GetCharacterBucket();
            
            List<Character> nList = new(characters);
            
            CharacterStatisticalAnalysis analysis = new CharacterStatisticalAnalysis(nList);
            var init = analysis.RunStatisticalAnalysis(useEntropy);

            
            bool exitEarly = false;
            for (int i = 0; i < 6; i++)
            {
                if (init.Count == 1)
                {
                    counters[i]++;
                    exitEarly = true;
                    break;
                }
                var ck = init[0].Item1.GetCharacterBucket();
                var res = BucketResult.GenBucketResult(ck, tBucket);
                List<Character> nlist2 = new();
                foreach (var (c, _) in init)
                {
                    if (CharacterStatisticalAnalysis.BucketKeyMatch(
                            ck, 
                            c.GetCharacterBucket(), 
                            res
                        ))
                    {
                        nlist2.Add(c);
                    }
                }
                analysis = new CharacterStatisticalAnalysis(nlist2);
                init = analysis.RunStatisticalAnalysis(useEntropy);
            }

            if (!exitEarly)
            {
                counters[6]++;
            }

            totalIters++;
        }
        
        
        Console.WriteLine($"Analysis Results after {totalIters} iterations, useEntropy={useEntropy}");
        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine($"{i}: {counters[i]}, {counters[i] * 100 / (double)totalIters:F4}");
        }
    }
}