using ClientManager.WebApi.Model;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Model;

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
                yield return new CustomerDto()
                {
                    Id = customer.Id,
                    Name = $"{customer.Firstname} {customer.Lastname}",
                    Email = customer.Email
                };
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
            Customer cust = new() // I would have avoided using the customer type directly to lower coupling
            {
                Firstname = customer.Name?.Substring(0, customer.Name.IndexOf(' ')),
                Lastname = customer.Name?.Substring(customer.Name.IndexOf(' ')),
                Address = customer.Address,
                City = customer.City,
                Email = customer.Email,
                Phone = customer.Phone,
                Zip = customer.Zip
            };
            int id = dao.Insert(cust); // Vi bruger ikke id'et her, men det er med for eksemplets skyld
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CustomerDto customerDto)
        {
            var customer = dao.GetById(id);
            if (customer != null)
            {
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
