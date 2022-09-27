namespace Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public override string ToString()
        {
            return $"ID      - {Id}\n" +
                   $"Name    - {Firstname} {Lastname}\n" +
                   $"Address - {Address}\n" +
                   $"          {Zip} {City}\n\n" +
                   $"Phone   - {Phone}\n" +
                   $"Email   - {Email}\n" +
                   $"---\n";
        }


    }
}