using Microsoft.AspNetCore.Mvc;
using Users_Api.Models;
using Users_Api.Repositories;

namespace Users_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos las usuarios.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        /// <response code="200">Devuelve la lista de usuarios.</response>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _repository.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario.</param>
        /// <returns>El usuario correspondiente al ID proporcionado.</returns>
        /// <response code="200">Devuelve el usuario encontrada.</response>
        /// <response code="404">No se encontró el usuario.</response>
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _repository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="user">El objeto usuario a crear.</param>
        /// <returns>El usuario creada.</returns>
        /// <response code="201">Devuelve el usuario creado.</response>
        /// <response code="400">La solicitud es inválida.</response>
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="id">El ID del usuario a actualizar.</param>
        /// <param name="tarea">El objeto usuario con los nuevos datos.</param>
        /// <returns>El usuario actualizado.</returns>
        /// <response code="200">Devuelve el usuario actualizado.</response>
        /// <response code="404">No se encontró el usuario.</response>
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingTarea = _repository.GetUserById(id);
            if (existingTarea == null)
            {
                return NotFound();
            }
            _repository.UpdateUser(user);
            return Ok(user);
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario a eliminar.</param>
        /// <response code="204">El usuario se ha eliminado correctamente.</response>
        /// <response code="404">No se encontró el usuario.</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _repository.DeleteUser(id);
            return NoContent();
        }
    }
}

