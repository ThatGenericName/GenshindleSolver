namespace GenshindleSolver;


public enum Element
{
    Anemo,
    Cryo,
    Dendro,
    Electro,
    Geo,
    Hydro,
    Pyro,
    None
}

public enum Weapon
{
    Bow,
    Catalyst,
    Claymore,
    Polearm,
    Sword
}

public enum Region
{
    Mondstadt,
    Liyue,
    Inazuma,
    Sumeru,
    Fontaine,
    Natlan,
    NodKrai,
    Snezhnaya,
    Other,
}

public static class EnumConverters
{
    public static string ElementToString(Element element)
    {
        return element switch
        {
            Element.Anemo => "Anemo",
            Element.Cryo => "Cryo",
            Element.Dendro => "Dendro",
            Element.Electro => "Electro",
            Element.Geo => "Geo",
            Element.Hydro => "Hydro",
            Element.Pyro => "Pyro",
            _ => "None"
        };
    }

    public static Element StringToElement(string element)
    {
        return element.ToLower() switch
        {
            "anemo" => Element.Anemo,
            "a" => Element.Anemo,
            "cryo" => Element.Cryo,
            "c" => Element.Cryo,
            "dendro" => Element.Dendro,
            "d"=> Element.Dendro,
            "electro" => Element.Electro,
            "e"=> Element.Electro,
            "geo" => Element.Geo,
            "g" => Element.Geo,
            "hydro" => Element.Hydro,
            "h"=>Element.Hydro,
            "pyro" => Element.Pyro,
            "p"=>Element.Pyro,
            "none" => Element.None,
            _ => throw new InvalidCastException($"{element} is not a valid element!")
        };
    }

    public static string WeaponToString(Weapon weapon)
    {
        return weapon switch
        {
            Weapon.Bow => "Bow",
            Weapon.Catalyst => "Catalyst",
            Weapon.Claymore => "Claymore",
            Weapon.Polearm => "Polearm",
            Weapon.Sword => "Sword",
            _ => "N/A"
        };
    }

    public static Weapon StringToWeapon(string weapon)
    {
        return weapon.ToLower() switch
        {
            "bow" => Weapon.Bow,
            "b"=>Weapon.Bow,
            "catalyst" => Weapon.Catalyst,
            "ca"=>Weapon.Catalyst,
            "claymore" => Weapon.Claymore,
            "cl"=>Weapon.Claymore,
            "polearm" => Weapon.Polearm,
            "p"=>Weapon.Polearm,
            "sword" => Weapon.Sword,
            "s"=>Weapon.Sword,
            _ => throw new InvalidCastException($"{weapon} is not a valid weapon!")
        };
    }

    public static string RegionToString(Region region)
    {
        return region switch
        {
            Region.Fontaine => "Fontaine",
            Region.Inazuma => "Inazuma",
            Region.Liyue => "Liyue",
            Region.Mondstadt => "Mondstadt",
            Region.Natlan => "Natlan",
            Region.NodKrai => "Nod-Krai",
            Region.Snezhnaya => "Snezhnaya",
            Region.Sumeru => "Sumeru",
            _ => "Other",
        };
    }

    public static Region StringToRegion(string region)
    {
        return region.ToLower().Replace("-", "").Replace(" ", "") switch
        {
            "fontaine" => Region.Fontaine,
            "f"=>Region.Fontaine,
            "inazuma" => Region.Inazuma,
            "i"=>Region.Inazuma,
            "liyue" => Region.Liyue,
            "l"=>Region.Liyue,
            "mondstadt" => Region.Mondstadt,
            "m"=>Region.Mondstadt,
            "natlan" => Region.Natlan,
            "na"=>Region.Natlan,
            "nodkrai" => Region.NodKrai,
            "no"=>Region.NodKrai,
            "snezhnaya" => Region.Snezhnaya,
            "sn"=>Region.Snezhnaya,
            "sumeru" => Region.Sumeru,
            "su"=>Region.Sumeru,
            "other" => Region.Other,
            "o"=>Region.Other,
            _ => throw new InvalidCastException($"{region} is not a valid region!")
        };
    }
}

public struct VersionNum
{
    public readonly int Major;
    public readonly int Minor;

    public static bool operator >(VersionNum v1, VersionNum v2)
    {
        return (v1.Major > v2.Major) || ((v1.Major == v2.Major) && (v1.Minor > v2.Minor));
    }

    public static bool operator <(VersionNum v1, VersionNum v2)
    {
        return (v1.Major < v2.Major) || ((v1.Major == v2.Major) && (v1.Minor < v2.Minor));
    }

    public static bool operator ==(VersionNum v1, VersionNum v2)
    {
        return v1.Major == v2.Major && v1.Minor == v2.Minor;
    }

    public static bool operator !=(VersionNum v1, VersionNum v2)
    {
        return !(v1 == v2);
    }

    public override string ToString()
    {
        return $"{Major}.{Minor}";
    }


    public override int GetHashCode()
    {
        return (Major, Minor).GetHashCode();
    }


    public VersionNum(int maj, int min)
    {
        Major = maj;
        Minor = min;
    }
    
    public VersionNum(string stringVal)
    {
        var ksplit = stringVal.Split('.');
        if (ksplit.Length == 2)
        {
            int.TryParse(ksplit[0], out Major);
            int.TryParse(ksplit[1], out Minor);
        }
        else
        {
            int.TryParse(stringVal, out Major);
            Minor = 0;
        }
    }
}


public readonly struct Bucket(int rarity, Element element, Weapon weapon, Region region, VersionNum version)
    : IEquatable<Bucket>
{
    public readonly int Rarity = rarity;

    public readonly Element Element = element;

    public readonly Weapon Weapon = weapon;

    public readonly Region Region = region;

    public readonly VersionNum Version = version;

    public override int GetHashCode()
    {
        return (Rarity, Element, Weapon, Region, Version).GetHashCode();
    }
        
    public static bool operator ==(Bucket bucket1, Bucket bucket2)
    {
        return bucket1.Rarity == bucket2.Rarity && bucket1.Element == bucket2.Element
                                                && bucket1.Weapon == bucket2.Weapon && bucket1.Region == bucket2.Region
                                                && bucket1.Version == bucket2.Version;
    }

    public static bool operator !=(Bucket bucket1, Bucket bucket2)
    {
        return !(bucket1 == bucket2);
    }
        
    public bool Equals(Bucket other)
    {
        return this == other;
    }

    public override bool Equals(object? obj)
    {
        return obj is Bucket other && Equals(other);
    }
}


public enum HighMidLow
{
    High,
    Mid,
    Low
}

public readonly struct BucketResult(bool rarityResult, bool elementResult, bool weaponResult, bool regionResult, HighMidLow versionResult)
{
    public readonly bool RarityResult = rarityResult;
    public readonly bool ElementResult = elementResult;
    public readonly bool WeaponResult = weaponResult;
    public readonly bool RegionResult = regionResult;
    public readonly HighMidLow VersionResult = versionResult;


    /// <summary>
    /// returns a bucket result based on bucket 2 to bucket 1
    /// </summary>
    /// <param name="bucket1"></param>
    /// <param name="bucket2"></param>
    public static BucketResult GenBucketResult(Bucket bucket1, Bucket bucket2)
    {
        bool rarityResult = bucket1.Rarity == bucket2.Rarity;
        bool elementResult = bucket1.Element == bucket2.Element;
        bool weaponResult = bucket1.Weapon == bucket2.Weapon;
        bool regionResult = bucket1.Region == bucket2.Region;
        HighMidLow versionResult;
        if (bucket2.Version < bucket1.Version)
        {
            versionResult = HighMidLow.Low;
        }
        else if (bucket2.Version > bucket1.Version)
        {
            versionResult = HighMidLow.High;
        }
        else
        {
            versionResult = HighMidLow.Mid;
        }

        return new BucketResult(rarityResult, elementResult, weaponResult, regionResult, versionResult);
    }
    
    public static BucketResult[] GenBucketResults()
    {
        int ct = 0;
        BucketResult[] bucketResults = new BucketResult[48];
        for (int r = 0; r < 2; r++)
        {
            bool rarity = r == 1;
            for (int e = 0; e < 2; e++)
            {
                bool element = e == 1;
                for (int w = 0; w < 2; w++)
                {
                    bool wep = w == 1;
                    for (int r2 = 0; r2 < 2; r2++)
                    {
                        bool reg = r2 == 1;
                        for (int v = 0; v < 3; v++)
                        {
                            HighMidLow hml = (HighMidLow)v;
                            bucketResults[ct++] = new BucketResult(rarity, element, wep, reg, hml);
                        }
                    }
                }
            }
        }
        return bucketResults;
    }

    public string GetPrintedString(int i)
    {
        return $"{{ RarityResult: {RarityResult.ToString().ToLower()}, ElementResult: {ElementResult.ToString().ToLower()}, " +
               $"WeaponResult: {WeaponResult.ToString().ToLower()}, RegionResult: {RegionResult.ToString().ToLower()}, " +
               $"VersionResult: HighMidLow.{VersionResult} }} => {i},";
    }

    public int AsKey()
    {
        return this switch
        {
            {
                RarityResult: false, ElementResult: false, WeaponResult: false, RegionResult: false,
                VersionResult: HighMidLow.High
            } => 0,
            {
                RarityResult: false, ElementResult: false, WeaponResult: false, RegionResult: false,
                VersionResult: HighMidLow.Mid
            } => 1,
            {
                RarityResult: false, ElementResult: false, WeaponResult: false, RegionResult: false,
                VersionResult: HighMidLow.Low
            } => 2,
            {
                RarityResult: false, ElementResult: false, WeaponResult: false, RegionResult: true,
                VersionResult: HighMidLow.High
            } => 3,
            {
                RarityResult: false, ElementResult: false, WeaponResult: false, RegionResult: true,
                VersionResult: HighMidLow.Mid
            } => 4,
            {
                RarityResult: false, ElementResult: false, WeaponResult: false, RegionResult: true,
                VersionResult: HighMidLow.Low
            } => 5,
            {
                RarityResult: false, ElementResult: false, WeaponResult: true, RegionResult: false,
                VersionResult: HighMidLow.High
            } => 6,
            {
                RarityResult: false, ElementResult: false, WeaponResult: true, RegionResult: false,
                VersionResult: HighMidLow.Mid
            } => 7,
            {
                RarityResult: false, ElementResult: false, WeaponResult: true, RegionResult: false,
                VersionResult: HighMidLow.Low
            } => 8,
            {
                RarityResult: false, ElementResult: false, WeaponResult: true, RegionResult: true,
                VersionResult: HighMidLow.High
            } => 9,
            {
                RarityResult: false, ElementResult: false, WeaponResult: true, RegionResult: true,
                VersionResult: HighMidLow.Mid
            } => 10,
            {
                RarityResult: false, ElementResult: false, WeaponResult: true, RegionResult: true,
                VersionResult: HighMidLow.Low
            } => 11,
            {
                RarityResult: false, ElementResult: true, WeaponResult: false, RegionResult: false,
                VersionResult: HighMidLow.High
            } => 12,
            {
                RarityResult: false, ElementResult: true, WeaponResult: false, RegionResult: false,
                VersionResult: HighMidLow.Mid
            } => 13,
            {
                RarityResult: false, ElementResult: true, WeaponResult: false, RegionResult: false,
                VersionResult: HighMidLow.Low
            } => 14,
            {
                RarityResult: false, ElementResult: true, WeaponResult: false, RegionResult: true,
                VersionResult: HighMidLow.High
            } => 15,
            {
                RarityResult: false, ElementResult: true, WeaponResult: false, RegionResult: true,
                VersionResult: HighMidLow.Mid
            } => 16,
            {
                RarityResult: false, ElementResult: true, WeaponResult: false, RegionResult: true,
                VersionResult: HighMidLow.Low
            } => 17,
            {
                RarityResult: false, ElementResult: true, WeaponResult: true, RegionResult: false,
                VersionResult: HighMidLow.High
            } => 18,
            {
                RarityResult: false, ElementResult: true, WeaponResult: true, RegionResult: false,
                VersionResult: HighMidLow.Mid
            } => 19,
            {
                RarityResult: false, ElementResult: true, WeaponResult: true, RegionResult: false,
                VersionResult: HighMidLow.Low
            } => 20,
            {
                RarityResult: false, ElementResult: true, WeaponResult: true, RegionResult: true,
                VersionResult: HighMidLow.High
            } => 21,
            {
                RarityResult: false, ElementResult: true, WeaponResult: true, RegionResult: true,
                VersionResult: HighMidLow.Mid
            } => 22,
            {
                RarityResult: false, ElementResult: true, WeaponResult: true, RegionResult: true,
                VersionResult: HighMidLow.Low
            } => 23,
            {
                RarityResult: true, ElementResult: false, WeaponResult: false, RegionResult: false,
                VersionResult: HighMidLow.High
            } => 24,
            {
                RarityResult: true, ElementResult: false, WeaponResult: false, RegionResult: false,
                VersionResult: HighMidLow.Mid
            } => 25,
            {
                RarityResult: true, ElementResult: false, WeaponResult: false, RegionResult: false,
                VersionResult: HighMidLow.Low
            } => 26,
            {
                RarityResult: true, ElementResult: false, WeaponResult: false, RegionResult: true,
                VersionResult: HighMidLow.High
            } => 27,
            {
                RarityResult: true, ElementResult: false, WeaponResult: false, RegionResult: true,
                VersionResult: HighMidLow.Mid
            } => 28,
            {
                RarityResult: true, ElementResult: false, WeaponResult: false, RegionResult: true,
                VersionResult: HighMidLow.Low
            } => 29,
            {
                RarityResult: true, ElementResult: false, WeaponResult: true, RegionResult: false,
                VersionResult: HighMidLow.High
            } => 30,
            {
                RarityResult: true, ElementResult: false, WeaponResult: true, RegionResult: false,
                VersionResult: HighMidLow.Mid
            } => 31,
            {
                RarityResult: true, ElementResult: false, WeaponResult: true, RegionResult: false,
                VersionResult: HighMidLow.Low
            } => 32,
            {
                RarityResult: true, ElementResult: false, WeaponResult: true, RegionResult: true,
                VersionResult: HighMidLow.High
            } => 33,
            {
                RarityResult: true, ElementResult: false, WeaponResult: true, RegionResult: true,
                VersionResult: HighMidLow.Mid
            } => 34,
            {
                RarityResult: true, ElementResult: false, WeaponResult: true, RegionResult: true,
                VersionResult: HighMidLow.Low
            } => 35,
            {
                RarityResult: true, ElementResult: true, WeaponResult: false, RegionResult: false,
                VersionResult: HighMidLow.High
            } => 36,
            {
                RarityResult: true, ElementResult: true, WeaponResult: false, RegionResult: false,
                VersionResult: HighMidLow.Mid
            } => 37,
            {
                RarityResult: true, ElementResult: true, WeaponResult: false, RegionResult: false,
                VersionResult: HighMidLow.Low
            } => 38,
            {
                RarityResult: true, ElementResult: true, WeaponResult: false, RegionResult: true,
                VersionResult: HighMidLow.High
            } => 39,
            {
                RarityResult: true, ElementResult: true, WeaponResult: false, RegionResult: true,
                VersionResult: HighMidLow.Mid
            } => 40,
            {
                RarityResult: true, ElementResult: true, WeaponResult: false, RegionResult: true,
                VersionResult: HighMidLow.Low
            } => 41,
            {
                RarityResult: true, ElementResult: true, WeaponResult: true, RegionResult: false,
                VersionResult: HighMidLow.High
            } => 42,
            {
                RarityResult: true, ElementResult: true, WeaponResult: true, RegionResult: false,
                VersionResult: HighMidLow.Mid
            } => 43,
            {
                RarityResult: true, ElementResult: true, WeaponResult: true, RegionResult: false,
                VersionResult: HighMidLow.Low
            } => 44,
            {
                RarityResult: true, ElementResult: true, WeaponResult: true, RegionResult: true,
                VersionResult: HighMidLow.High
            } => 45,
            {
                RarityResult: true, ElementResult: true, WeaponResult: true, RegionResult: true,
                VersionResult: HighMidLow.Mid
            } => 46,
            {
                RarityResult: true, ElementResult: true, WeaponResult: true, RegionResult: true,
                VersionResult: HighMidLow.Low
            } => 47,
            _ => -1,
        };
    }
}


public class Character
{
    public string Name { get; set; }
    
    public int Rarity { get; set; }
    
    public Element Element { get; set; }
    
    public Weapon Weapon { get; set; }
    
    public Region Region { get; set; }
    
    public VersionNum Version { get; set; }

    public static Character FromStringArray(string[] line)
    {
        var cName = line[0];
        var rarity = int.Parse(line[1]);
        var element = EnumConverters.StringToElement(line[2]);
        var weapon = EnumConverters.StringToWeapon(line[3]);
        var region = EnumConverters.StringToRegion(line[4]);
        var versionNum = new VersionNum(line[5]);
        return new Character()
        {
            Name = cName,
            Rarity = rarity,
            Element = element,
            Weapon = weapon,
            Region = region,
            Version = versionNum,
        };
    }

    public string PrintString()
    {
        return $"{Name}, {Rarity}, {EnumConverters.ElementToString(Element)}, " +
            $"{EnumConverters.WeaponToString(Weapon)}, {EnumConverters.RegionToString(Region)}, {Version}";
    }


    public Bucket GetCharacterBucket()
    {
        return new Bucket(Rarity, Element, Weapon, Region, Version);
    }
    
}