using GenshindleSolver;

Console.WriteLine("Hello, World!");
Console.WriteLine("");

// var brs = BucketResult.GenBucketResults();
// 
// for (int i = 0; i < brs.Length; i++)
// {
//     Console.WriteLine(brs[i].GetPrintedString(i));
// }

string fileName = "CharacterList.csv";


List<Character> characters = new();
using (var reader = new StreamReader(fileName))
{
    var firstLine = reader.ReadLine();
    while (!reader.EndOfStream)
    {
        var line = reader.ReadLine();
        var character = Character.FromStringArray(line!.Split(","));
        characters.Add(character);
        // Console.WriteLine(character.PrintString());
    }
}

// AnalysisMode.RunAnalysis(characters, false);
// AnalysisMode.RunAnalysis(characters, true);
// AnalysisMode.RunRandomizedHMAnalysis(characters, false);
// AnalysisMode.RunRandomizedHMAnalysis(characters, true);

// return;

bool normalMode = false;

while (true)
{
    CharacterStatisticalAnalysis analysis = new CharacterStatisticalAnalysis(characters);
    var init = analysis.RunStatisticalAnalysis();
    if (normalMode)
    {
        // statistical analysis of each character. 
        // basically, theres 2 parts 
        // probability of getting the individual elements correct 
        // and the expected number of characters eliminated given 
        // whatever combination of getting those elements correct.

        Console.WriteLine($"Initial Guess Recommendations:");
        for (int i = 0; i < 5 && i < init.Count; i++)
        {
            Console.WriteLine(
                $"{i} - {init[i].Item1.Name} - {EnumConverters.ElementToString(init[i].Item1.Element)} : {init[i].Item2:F3}");
        }
    }
    else
    {
        Console.WriteLine("Hard Mode");
        while (true)
        {
            try
            {
                Console.WriteLine("Enter Bucket");
                Console.Write("Enter nothing for autofill completion\n>>> ");
                string line1 = Console.ReadLine()!;
                if (line1.Length == 0)
                {
                    break;
                }

                Bucket bucket = ParseLineBucket(line1);
                Console.Write("Enter Bucket Result\n>>> ");
                string line2 = Console.ReadLine()!;
                var br = ParseLineResult("N/A," + line2);

                List<Character> remaining = new();
                foreach (var (c, _) in init)
                {
                    if (CharacterStatisticalAnalysis.BucketKeyMatch(
                            bucket,
                            c.GetCharacterBucket(),
                            br.Item2)
                       )
                    {
                        remaining.Add(c);
                    }
                }

                analysis = new CharacterStatisticalAnalysis(remaining);
                init = analysis.RunStatisticalAnalysis();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bad Inputs!");
            }
        }

        Console.WriteLine($"{init.Count} possible characters remaining");
        Console.WriteLine($"Initial Guess Recommendations:");
        for (int i = 0; i < 5 && i < init.Count; i++)
        {
            Console.WriteLine(
                $"{i} - {init[i].Item1.Name} - {EnumConverters.ElementToString(init[i].Item1.Element)} : {init[i].Item2:F3}");
        }
    }

    while (true)
    {
        Console.Write("Input Results of Previous Guess, enter [c] to complete round:\n>>> ");
        string lineRes = Console.ReadLine()!;
        if (lineRes == "c")
        {
            break;
        }
        try
        {
            var (charName, res) = ParseLineResult(lineRes);
            var selectedCharacter = init.Find(e =>
                string.Equals(e.Item1.Name, charName, StringComparison.CurrentCultureIgnoreCase)).Item1;
            List<Character> remaining = new();
            foreach (var (c, _) in init)
            {
                if (CharacterStatisticalAnalysis.BucketKeyMatch(
                        selectedCharacter.GetCharacterBucket(),
                        c.GetCharacterBucket(),
                        res)
                   )
                {
                    remaining.Add(c);
                }
            }

            Console.WriteLine($"Remaining Characters {remaining.Count}");

            analysis = new CharacterStatisticalAnalysis(remaining);
            init = analysis.RunStatisticalAnalysis();
            Console.WriteLine("Recommended Guesses:");
            for (int i = 0; i < 5 && i < init.Count; i++)
            {
                Console.WriteLine(
                    $"{i} - {init[i].Item1.Name} - {EnumConverters.ElementToString(init[i].Item1.Element)} : {init[i].Item2:F3}");
            }
        }
        catch (Exception ex) // lol
        {
            Console.WriteLine($"input [{lineRes}] Was not valid!");
            continue;
        }
    }
}

Bucket ParseLineBucket(string line)
{
    var stringArr = line.Split(",");
    int rarity = int.Parse(stringArr[0]);
    Element element = EnumConverters.StringToElement(stringArr[1]);
    Weapon weapon = EnumConverters.StringToWeapon(stringArr[2]);
    Region region = EnumConverters.StringToRegion(stringArr[3]);
    VersionNum version = new VersionNum(stringArr[4]);
    return new Bucket(rarity, element, weapon, region, version);
}

(string, BucketResult) ParseLineResult(string line)
{
    var stringArr = line.Split(",");
    var characterName = stringArr[0];
    bool rarityRes = stringArr[1].ToLower().StartsWith("t");
    bool elementRes = stringArr[2].ToLower().StartsWith("t");
    bool wepRes = stringArr[3].ToLower().StartsWith("t");
    bool regRes = stringArr[4].ToLower().StartsWith("t");
    HighMidLow hml = Enum.Parse<HighMidLow>(stringArr[5]);
    var br = new BucketResult(rarityRes, elementRes, wepRes, regRes, hml);
    return (characterName, br);
}