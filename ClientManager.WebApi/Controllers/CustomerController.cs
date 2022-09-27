using ClientManager.WebApi.Model;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        CustomerDao dao = CustomerDao.Create();

        [HttpGet]
        public IEnumerable<CustomerDto> Get()
        {
            // var type is resolved compile time
            foreach (var customer in dao.GetAll())
            {
                // The yield keyword tells the compiler that the method in which it appears is an iterator block
                yield return CustomerDto.Map(customer);
            }
        }

        [HttpGet("{id}")]
        public CustomerDto Get(int id)
        {
            return CustomerDto.Map(dao.GetById(id));
        }

        [HttpPost]
        public void Post([FromBody] CustomerDto customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            if (customer.Name == null)
            {
                throw new ArgumentException("Customer must have a name");
            }
            // extracting values from dto
            string firstname = customer.Name.Substring(0, customer.Name.IndexOf(' '));
            string lastname = customer.Name.Substring(customer.Name.IndexOf(' '));
            string? address = customer.Address;
            string? city = customer.City;
            string? email = customer.Email;
            string? phone = customer.Phone;
            string? zip = customer.Zip;

            // To avoid using the customer type directly for lower coupling
            // I have created a factory method in the CustomerDao class
            var newCustomer = CustomerDao.CreateModel(firstname, lastname, address, zip, city, email, phone);
            
            int id = dao.Insert(newCustomer); // Vi bruger ikke id'et her, men det er med for eksemplets skyld
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CustomerDto customerDto)
        {
            var customer = dao.GetById(id);
            if (customer != null)
            {
                // it is only allowed to update these data
                customer.Address = customerDto.Address;
                customer.City = customerDto.City;   
                customer.Zip = customerDto.Zip;
                customer.Phone = customerDto.Phone;
                customer.Email = customerDto.Email;
                dao.Update(customer);
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var customer = dao.GetById(id);
            if (customer != null)
            {
                dao.Delete(customer);
            }
        }
    }
}
