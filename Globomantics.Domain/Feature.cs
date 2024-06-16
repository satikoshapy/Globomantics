namespace Globomantics.Domain
{
    public record Feature(string Title,
        string Descripton,
        int Priority,
        User CreatedBy,
        User AssignedTo): TodoTask(Title, DateTimeOffset.MinValue, CreatedBy);
}
