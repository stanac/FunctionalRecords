using System.Text;

namespace FunctionalRecords;

internal class TypeName
{
    public string ShortName { get; }

    public TypeName(Type type)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));

        if (type.IsArray)
        {
            Type elementType = type.GetElementType()!;

            TypeName elementTypeName = new TypeName(elementType);

            ShortName = elementTypeName + "[]";
        }
        else if (type.IsGenericType)
        {
            Type[] args = type.GetGenericArguments();
            TypeName[] names = args.Select(t => new TypeName(t)).ToArray();

            StringBuilder sb = new StringBuilder();
            sb.Append(type.Name.Split('`')[0]).Append("<");

            for (int i = 0; i < names.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(",");
                }

                sb.Append(names[i]);
            }

            sb.Append(">");

            ShortName = sb.ToString();
        }
        else
        {
            ShortName = type.Name;
        }
    }

    public static Type GetTypeFromName(string name, params Type[] availableTypes)
    {
        if (availableTypes == null) throw new ArgumentNullException(nameof(availableTypes));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        if (availableTypes.Length < 2)
            throw new ArgumentException("Value cannot be an empty collection. It must have at least 2 unique items.",
                nameof(availableTypes));
        if (availableTypes.Length != availableTypes.Distinct().Count())
            throw new ArgumentException("Value must have unique items", nameof(availableTypes));

        TypeName[] names = availableTypes.Select(x => new TypeName(x)).ToArray();

        foreach (TypeName typeName in names)
        {
            if (names.Count(x => x == typeName) > 1)
            {
                throw new ArgumentException($"Two types give equal name `{typeName}`.", nameof(availableTypes));
            }
        }

        for (int i = 0; i < availableTypes.Length; i++)
        {
            if (name == names[i].ShortName)
            {
                return availableTypes[i];
            }
        }

        throw new InvalidOperationException($"Cannot get type from name `{name}`.");
    }

    public override string ToString() => ShortName;

    public override int GetHashCode()
    {
        return ShortName.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is TypeName tn)
        {
            return tn.ShortName == ShortName;
        }

        return false;
    }

    public static bool operator ==(TypeName? t1, TypeName? t2)
    {
        if (t1 is null && t2 is null) return true;
        if (t1 is null || t2 is null) return false;

        return t1.ShortName == t2.ShortName;
    }

    public static bool operator !=(TypeName? t1, TypeName? t2)
    {
        return !(t1 == t2);
    }
}