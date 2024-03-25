namespace CatalogR.Models
{
    public struct CustomFieldValues<T>
    {
        public readonly bool State;
        public readonly string? Name;
        public readonly T Value;

        public CustomFieldValues(bool state, string? name, T value)
        {
            State = state;
            Name = name;
            Value = value;
        }
    }
}
