using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository;
using Microsoft.AspNetCore.OData.Query;
using BussinessObject.Models;
using DTO;
using Microsoft.AspNetCore.OData.Formatter;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class EmployeesController : ODataController
	{
		private readonly IEmployeeRepository employeeRepository;
		public EmployeesController(IEmployeeRepository employeeRepository)
		{
			this.employeeRepository = employeeRepository;
		}

		// GET: api/Employees
		[HttpGet]
		[EnableQuery]
		[ProducesResponseType(typeof(IEnumerable<Employee>), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> GetEmployees()
		{
			try
			{
				//IEnumerable<Employee> users = await employeeRepository.GetEmployeesAsync();
				return StatusCode(200, await employeeRepository.GetEmployeesAsync());
			}
			catch (ApplicationException ae)
			{
				return StatusCode(400, ae.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		//[EnableQuery]
		//[ODataRoute("Employees", RouteName = "Login")]
		[HttpPost("Login")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(AuthorizeUser), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> PostLogin(UserLoginDTO userLogin)
		{
			try
			{
				string memberRole = "USER";
				Employee loginEmployee = await employeeRepository.LoginAsync(userLogin.Email, userLogin.Password);

				Employee defaultEmployee = employeeRepository.GetDefaultEmployee();

				if (loginEmployee.EmailAddress.Equals(defaultEmployee.EmailAddress))
				{
					memberRole = "ADMIN";
				}
				if (loginEmployee == null)
				{
					throw new ApplicationException("Failed to login! Please check the information again...");
				}


				
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Email, loginEmployee.EmailAddress),
					new Claim(ClaimTypes.NameIdentifier, loginEmployee.EmployeeID.ToString()),
					new Claim(ClaimTypes.Role, memberRole)
				};
				var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var authProperties = new AuthenticationProperties
				{
					AllowRefresh = false,
					IsPersistent = true
				};

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity), authProperties);

				//loginEmployee.MemberId = 0;
				loginEmployee.Password = "";
				AuthorizeUser authorizeEmployee = new AuthorizeUser(loginEmployee);
				authorizeEmployee.AuthorizeRole = memberRole;

				return StatusCode(200, authorizeEmployee);
			}
			catch (ApplicationException ae)
			{
				return StatusCode(401, ae.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		//[EnableQuery]
		[HttpPost("Logout")]
		[Authorize]
		public async Task<IActionResult> PostLogout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return StatusCode(204);
		}

		// GET: api/Employees/5
		[HttpGet("{id}")]
		[EnableQuery]
		[ProducesResponseType(typeof(Employee), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> GetEmployee([FromODataUri] int key)
		{
			try
			{
				string role = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role)).Value;
				if (role.Equals("USER"))
				{
					int id = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.NameIdentifier)).Value);
					if (key != id)
					{
						return Unauthorized();
					}
				}
				Employee user = await employeeRepository.GetEmployeeAsync(key);
				if (user == null)
				{
					return StatusCode(404, "Employee is not existed!!");
				}
				return StatusCode(200, user);
			}
			catch (ApplicationException ae)
			{
				return StatusCode(400, ae.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		// PUT: api/Employees/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		[EnableQuery]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> PutEmployee([FromODataUri] int key, Employee user)
		{
			if (key != user.EmployeeID)
			{
				return StatusCode(400, "ID is not the same!!");
			}

			try
			{
				string role = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role)).Value;
				if (role.Equals("USER"))
				{
					int id = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.NameIdentifier)).Value);
					if (key != id)
					{
						return Unauthorized();
					}
				}
				await employeeRepository.UpdateEmployeeAsync(user);
				return StatusCode(204, "Update successfully!");
			}
			catch (ApplicationException ae)
			{
				return StatusCode(400, ae.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}

		}

		// POST: api/Employees
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		[EnableQuery]
		[ProducesResponseType(typeof(Employee), 201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> PostEmployee(Employee user)
		{
			try
			{
				Employee createdEmployee = await employeeRepository.AddEmployeeAsync(user);
				return StatusCode(201, createdEmployee);
			}
			catch (ApplicationException ae)
			{
				return StatusCode(400, ae.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		// DELETE: api/Employees/5
		[HttpDelete("{id}")]
		[EnableQuery]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> DeleteEmployee([FromODataUri] int key)
		{
			try
			{
				await employeeRepository.DeleteEmployeeAsync(key);
				return StatusCode(204, "Delete successfully!");
			}
			catch (ApplicationException ae)
			{
				return StatusCode(400, ae.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
