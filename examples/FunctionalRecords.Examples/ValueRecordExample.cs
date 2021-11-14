namespace FunctionalRecords.Examples;

public static class ValueRecordExample
{
    // must be a record and NOT a class
    public record PersonName : ValueRecord<(string FirstName, string LastName)>
    {
        public PersonName((string FirstName, string LastName) value) : base(value)
        {
        }
        
        protected override IEnumerable<string> GetValidationErrors((string FirstName, string LastName) value)
        {
            // abstract method
            // if something is returned from this method FunctionalRecords.ValidationException will be thrown

            if (string.IsNullOrEmpty(value.FirstName))
            {
                yield return "FirstName not set";
            }

            if (string.IsNullOrEmpty(value.LastName))
            {
                yield return "LastName not set";
            }
        }

        protected override void AfterSuccessfulValidation()
        {
            // virtual method, optionally throw some other exception here
            // called only if GetValidationErrors does not return anything
        }

        protected override (string FirstName, string LastName) TransformValue((string FirstName, string LastName) value)
        {
            // virtual method called before validation
            // optionally transform object
            return (value.FirstName, value.LastName?.ToUpper());
        }

        // optionally implement conversion
        public static implicit operator PersonName ((string, string) value) => new(value);

        // optionally override to string
        public override string ToString() => $"Person: {Value.LastName}, {Value.FirstName}";
    }

    public static void Example()
    {
        PersonName pn = ("Jane", "Doe");
        Console.WriteLine(pn);
        // Prints: `Person: DOE, Jane`
    }
}
