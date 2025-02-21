namespace Shared.Helpers;

public static class EnumHelpers
{
    public static TAnotherEnum ToAnother<TAnotherEnum>(this Enum enumValue, bool ignoreCase) where TAnotherEnum : Enum
    {
        return (TAnotherEnum)Enum.Parse(typeof(TAnotherEnum), enumValue.ToString(), ignoreCase);
    }

    public static TAnotherEnum ToAnother<TAnotherEnum>(this Enum enumValue) where TAnotherEnum : Enum
    {
        return enumValue.ToAnother<TAnotherEnum>(true);
    }

    public static bool TryToAnother<TAnotherEnum>(this Enum enumValue, bool ignoreCase, out TAnotherEnum anotherEnum) where TAnotherEnum : struct
    {
        return Enum.TryParse(enumValue.ToString(), ignoreCase, out anotherEnum);
    }

    public static bool TryToAnother<TAnotherEnum>(this Enum enumValue, out TAnotherEnum anotherEnum) where TAnotherEnum : struct
    {
        return enumValue.TryToAnother(true, out anotherEnum);
    }

    public static TAnotherEnum ToAnotherOrDefault<TAnotherEnum>(this Enum enumValue) where TAnotherEnum : struct
    {
        return enumValue.ToAnotherOrDefault<TAnotherEnum>(true);
    }

    public static TAnotherEnum ToAnotherOrDefault<TAnotherEnum>(this Enum enumValue, bool ignoreCase) where TAnotherEnum : struct
    {
        return enumValue.TryToAnother(ignoreCase, out TAnotherEnum anotherEnum)
            ? anotherEnum
            : default;
    }

    public static TEnum ToEnum<TEnum>(this string stringValue, bool ignoreCase)
    {
        return (TEnum)Enum.Parse(typeof(TEnum), stringValue, ignoreCase);
    }

    public static TEnum ToEnum<TEnum>(this string stringValue)
    {
        return stringValue.ToEnum<TEnum>(true);
    }
}