namespace ClientManager.WebApi.Model
{
    public class CustomerDto
    {
        public int? Id { get; set; } 
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Zip { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public static CustomerDto Map(dynamic? obj) // dynamic type is resolved run time
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            return new CustomerDto
            {
                Id = obj.Id,
                Name = $"{obj.Firstname} {obj.Lastname}",
                Email = obj.Email,
                Phone = obj.Phone,
                City = obj.City,
                Address = obj.Address,
                Zip = obj.Zip                
            };
        }
    }
}
