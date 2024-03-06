using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

public class Enumeration<T> : IEquatable<Enumeration<T>>
{
    public int Value { get; }
    public string Name { get; }

    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll()
    {
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }

    public static T FromValue(int value)
    {
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        foreach (var field in fields)
        {
            var item = (T)field.GetValue(null);
            var instance = item as Enumeration<T>;
            if (instance != null && instance.Value == value)
            {
                return item;
            }
        }
        throw new ArgumentOutOfRangeException($"Value {value} is not in the enumeration {typeof(T).Name}");
    }

    public static T FromName(string name)
    {
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        foreach (var field in fields)
        {
            var item = (T)field.GetValue(null);
            var instance = item as Enumeration<T>;
            if (instance != null && instance.Name == name)
            {
                return item;
            }
        }
        throw new ArgumentOutOfRangeException($"Name {name} is not in the enumeration {typeof(T).Name}");
    }

    public override bool Equals(object obj)
    {
        if (obj is Enumeration<T> other) return Equals(other);
        return false;
    }

    public bool Equals(Enumeration<T> other)
    {
        if (other == null) return false;
        return GetType() == other.GetType() && Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(Enumeration<T> a, Enumeration<T> b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(Enumeration<T> a, Enumeration<T> b) => !(a == b);
}
