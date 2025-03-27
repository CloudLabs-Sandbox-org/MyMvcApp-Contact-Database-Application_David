using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        // GET: User
        public ActionResult Index()
        {
            // Devuelve la lista de usuarios a la vista Index
            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Busca el usuario por ID en la lista
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(user); // Pasa el usuario encontrado a la vista Details
        }

        // GET: User/Create
        public ActionResult Create()
        {
            // Devuelve la vista para crear un nuevo usuario
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Agrega el nuevo usuario a la lista
                user.Id = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1; // Genera un nuevo ID
                userlist.Add(user);
                return RedirectToAction(nameof(Index)); // Redirige a la lista de usuarios
            }
            return View(user); // Si hay errores de validación, vuelve a la vista Create
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // Busca el usuario por ID en la lista
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(user); // Pasa el usuario encontrado a la vista Edit
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            if (ModelState.IsValid)
            {
                // Busca el usuario existente por ID
                var existingUser = userlist.FirstOrDefault(u => u.Id == id);
                if (existingUser == null)
                {
                    return NotFound(); // Devuelve un error 404 si no se encuentra el usuario
                }

                // Actualiza los datos del usuario
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;

                return RedirectToAction(nameof(Index)); // Redirige a la lista de usuarios
            }
            return View(user); // Si hay errores de validación, vuelve a la vista Edit
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Busca el usuario por ID en la lista
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(user); // Pasa el usuario encontrado a la vista Delete
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            // Busca el usuario por ID en la lista
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }

            // Elimina el usuario de la lista
            userlist.Remove(user);

            return RedirectToAction(nameof(Index)); // Redirige a la lista de usuarios
        }
        // GET: User/Search
        public ActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                // Si no hay consulta, devuelve la lista completa
                return View("Index", userlist);
            }

            // Filtra la lista de usuarios por nombre, email o teléfono
            var results = userlist.Where(u =>
                u.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                u.Email.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                u.Phone.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            // Devuelve los resultados a la vista Index
            return View("Index", results);
        }
}
