using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        BibliotecaContext db = new BibliotecaContext();
        public ActionResult Index()
        {
            ViewBag.admins = db.Admins.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult reporteVentas()
        {
            //haz dos viewbags, una para ventas y otra para ventasLibros
            ViewBag.Ventas = db.Ventas.ToList();
            ViewBag.VentasLibros = db.VentaLibroes.ToList();
            ViewBag.Libros = db.Libroes.ToList();
            ViewBag.Personal = db.Personals.ToList();
            ViewBag.Clientes = db.Clientes.ToList();
            return View();
        }
        public ActionResult listaLibros()
        {
            var libros = db.Libroes.Include(l => l.Categoria_libro);
            return View(libros.ToList());
        }
        public ActionResult listaCategorias()
        {
            var categorias = db.Categoria_libro.ToList();
            return View(categorias);
        }
        public ActionResult listaClientes()
        {
            var clientes = db.Clientes.ToList();
            return View(clientes);
        }
        public ActionResult listaPersonal()
        {
            var personal = db.Personals.ToList();
            return View(personal);
        }
        public ActionResult listaChecadas()
        {
            var checada = db.Checadas.Include(l => l.Personal);
            return View(checada);
        }
        public ActionResult listaTipoPrestamo()
        {
            var tipoprestamo = db.Tipo_De_Prestamo.ToList();
            return View(tipoprestamo);
        }
        public ActionResult formularioVenta()
        {
            //llenar viewbags con libros, personal y clientes
            ViewBag.Libros = db.Libroes.ToList();
            ViewBag.Personal = db.Personals.ToList();
            ViewBag.Clientes = db.Clientes.ToList();


            return View();
        }
        public ActionResult formularioLibros()
        {
            var IDLibro = Request.Params["IDLibro"];
            if (IDLibro != null)
            {
                int id = int.Parse(IDLibro);
                var Libro = db.Libroes.Where(X => X.id_libro == id).FirstOrDefault();
                ViewBag.Libro = Libro;
            }
            ViewBag.Categorias = db.Categoria_libro.ToList();
            return View();
        }
        public ActionResult formularioCategorias()
        {
            var IDCategoria = Request.Params["IDCategoria"];
            if (IDCategoria != null)
            {
                int id = int.Parse(IDCategoria);
                var Categoria = db.Categoria_libro.Where(X => X.id_categoria_libro == id).FirstOrDefault();
                ViewBag.Categoria = Categoria;
            }
            return View();
        }
        public ActionResult formularioClientes()
        {
            var IDCliente = Request.Params["IDCliente"];
            if (IDCliente != null)
            {
                int id = int.Parse(IDCliente);
                var Cliente = db.Clientes.Where(X => X.id_cliente == id).FirstOrDefault();
                ViewBag.Cliente = Cliente;
            }
            return View();
        }
        public ActionResult formularioPersonal()
        {
            var IDPersonal = Request.Params["IDPersonal"];
            if (IDPersonal != null)
            {
                int id = int.Parse(IDPersonal);
                var Personal = db.Personals.Where(X => X.id_personal == id).FirstOrDefault();
                ViewBag.Personal = Personal;
            }
            return View();
        }
        public ActionResult formularioChecadas()
        {
            var IDChecada = Request.Params["IDChecada"];
            if (IDChecada != null)
            {
                int id = int.Parse(IDChecada);
                var Checada = db.Checadas.Where(X => X.id_checada == id).FirstOrDefault();
                ViewBag.Checada = Checada;
            }
            ViewBag.Personal = db.Personals.ToList();
            return View();
        }
        public ActionResult formularioTipoPrestamo()
        {
            var IDTipoPrestamo = Request.Params["IDTipoPrestamo"];
            if(IDTipoPrestamo != null)
            {
                int id = int.Parse(IDTipoPrestamo);
                var TipoPrestamo = db.Tipo_De_Prestamo.Where(x=> x.id_tipo_prestamo== id).FirstOrDefault();
                ViewBag.TipoPrestamo = TipoPrestamo;
            }
            return View();
        }
        
        public JsonResult hacerVenta(int? id_venta, int id_cliente, int id_personal, int[] arrayids, int[] arraycantidades)
        {

            var books = db.Libroes.ToList();
            if (id_venta != null)
            {
                var Venta = db.Ventas.Where(x => x.id_venta == id_venta).FirstOrDefault();
                Venta.id_cliente = id_cliente;
                Venta.id_personal = id_personal;
                var limit = arrayids.Length;
                for(int i = 0; i < limit; i++)
                {
                    var VentaLibro = new VentaLibro();
                    VentaLibro.id_venta = (int)id_venta;
                    VentaLibro.id_libro = arrayids[i];
                    VentaLibro.cantidad = arraycantidades[i];
                    db.VentaLibroes.Add(VentaLibro);
                    //restar la cantidad de libros vendidos a la cantidad de libros en existencia
                    var libro = books.Where(x => x.id_libro == arrayids[i]).FirstOrDefault();
                    libro.numero_copias = libro.numero_copias - arraycantidades[i];
                }
                db.SaveChanges();
            }
            else
            {
                Venta venta = new Venta();
                //no se incluye el id, porque es autoincrementable, se genera solo
                venta.id_cliente = id_cliente;
                venta.id_personal = id_personal;
                venta.fecha_venta = DateTime.Now;
                db.Ventas.Add(venta);
                db.SaveChanges();
                var limit = arrayids.Length;
                for (int i = 0; i < limit; i++)
                {
                    var VentaLibro = new VentaLibro();
                    VentaLibro.id_venta = venta.id_venta;
                    VentaLibro.id_libro = arrayids[i];
                    VentaLibro.cantidad = arraycantidades[i];
                    //restamos la cantidad de libros vendidos a la cantidad de libros en existencia
                    var libro = books.Where(x => x.id_libro == arrayids[i]).FirstOrDefault();
                    libro.numero_copias = libro.numero_copias - arraycantidades[i];
                    db.VentaLibroes.Add(VentaLibro);
                }
                //una vez realizadas las operaciones, guardamos los cambios
                db.SaveChanges();
            }
            return Json("");
        }
        public JsonResult guardarLibro(int? id_libro, int id_categoria_libro, string titulo, string autor,
            string editorial, int numero_copias, decimal precio)//id cat, titulo, autor, editorial,num copias, precio
        {
            if (id_libro != null)
            {
                var Libro = db.Libroes.Where(x => x.id_libro == id_libro).FirstOrDefault();
                Libro.id_categoria_libro = id_categoria_libro;
                Libro.titulo = titulo;
                Libro.autor = autor;
                Libro.editorial = editorial;
                Libro.numero_copias = numero_copias;
                Libro.precio = precio;
                db.SaveChanges();
            }
            else
            {

                Libro book = new Libro();
                book.id_categoria_libro = id_categoria_libro;
                book.titulo = titulo;
                book.autor = autor;
                book.editorial = editorial;
                book.numero_copias = numero_copias;
                book.precio = precio;
                book.Activo = true;
                db.Libroes.Add(book);

                db.SaveChanges();
            }


            return Json("");
        }
        public JsonResult guardarCategoria(int? id_categoria_libro, string nombre, string descripcion)//id cat, titulo, autor, editorial,num copias, precio
        {
            if (id_categoria_libro != null)
            {
                var Categoria = db.Categoria_libro.Where(x => x.id_categoria_libro == id_categoria_libro).FirstOrDefault();
                Categoria.nombre = nombre;
                Categoria.descripcion = descripcion;
                db.SaveChanges();
            }
            else
            {
                Categoria_libro categoria = new Categoria_libro();
                categoria.nombre = nombre;
                categoria.descripcion = descripcion;
                categoria.Activo = true;
                db.Categoria_libro.Add(categoria);

                db.SaveChanges();
            }
            

            return Json("");
        }
        public JsonResult guardarCliente(int? id_cliente, string nombre, string apellido, string direccion, string telefono, string email)
        {
            if (id_cliente != null)
            {
                var Cliente = db.Clientes.Where(x => x.id_cliente == id_cliente).FirstOrDefault();
                Cliente.nombre = nombre;
                Cliente.apellido = apellido;
                Cliente.direccion = direccion;
                Cliente.telefono = telefono;
                Cliente.email = email;
                db.SaveChanges();
            }
            else
            {
                Cliente cliente = new Cliente();
                cliente.nombre = nombre;
                cliente.apellido = apellido;
                cliente.direccion = direccion;
                cliente.telefono = telefono;
                cliente.email = email;
                cliente.Activo = true;
                db.Clientes.Add(cliente);
                db.SaveChanges();
            }
            return Json("");
        }
        public JsonResult guardarPersonal(int? id_personal, string nombre, string apellido, string puesto, decimal sueldo)
        {
            if (id_personal != null)
            {
                var Personal = db.Personals.Where(x => x.id_personal == id_personal).FirstOrDefault();
                Personal.nombre = nombre;
                Personal.apellido = apellido;
                Personal.puesto = puesto;
                Personal.sueldo = sueldo;
                db.SaveChanges();
            }
            else
            {
                Personal personal = new Personal();
                personal.nombre = nombre;
                personal.apellido = apellido;
                personal.puesto = puesto;
                personal.sueldo = sueldo;
                personal.Activo = true;
                db.Personals.Add(personal);
                db.SaveChanges();
            }
            return Json("");
        }
        public JsonResult guardarChecada(int? id_checada, int id_personal, DateTime fecha, TimeSpan hora_entrada, TimeSpan hora_salida)
        {
            if(id_checada != null)
            {
                var Checada = db.Checadas.Where(x => x.id_checada == id_checada).FirstOrDefault();
                Checada.id_personal = id_personal;
                Checada.fecha = fecha;
                Checada.hora_entrada = hora_entrada;
                Checada.hora_salida = hora_salida;
                db.SaveChanges();
            }
            else
            {
                Checada checada = new Checada();
                checada.id_personal =id_personal;
                checada.fecha = fecha;
                checada.hora_entrada =hora_entrada;
                checada.hora_salida=hora_salida;
                checada.Pagado = false;
                db.Checadas.Add(checada);
                db.SaveChanges();
            }
            return Json("");
        }
        public JsonResult guardarTipoPrestamo(int? id_tipo_prestamo, string tipo_prestamo,string descripcion)
        {
            if (id_tipo_prestamo != null)
            {
                var TipoPrestamo = db.Tipo_De_Prestamo.Where(x => x.id_tipo_prestamo == id_tipo_prestamo).FirstOrDefault();
                TipoPrestamo.tipo_prestamo = tipo_prestamo;
                TipoPrestamo.descripcion = descripcion;
                db.SaveChanges();
            }
            else
            {
                Tipo_De_Prestamo tipoPrestamo = new Tipo_De_Prestamo();
                tipoPrestamo.tipo_prestamo= tipo_prestamo;
                tipoPrestamo.descripcion = descripcion;
                tipoPrestamo.activo = true;
                db.Tipo_De_Prestamo.Add(tipoPrestamo);
                db.SaveChanges();
            }
            return Json("");
        }
        public JsonResult AdminLogin(int? id_admin, string email, string pword)
        {
            var Admin = db.Admins.Where(x => x.email == email && x.pword == pword).FirstOrDefault();
            if (Admin != null)
            {
                Admin.activo = true;
                db.SaveChanges();
                GoToIndex();
            }
            return Json("");
        }
        public JsonResult AdminLogout(int? id_admin)
        {
            foreach(var admin in db.Admins)
            {
                if (admin.activo == true)
                {
                    admin.activo = false; 
                }
                
            }
            db.SaveChanges();
            GoToIndex();
            return Json("");
        }
        public ActionResult GoToIndex()
        {
            return RedirectToAction("Index","Home");
        }
        public ActionResult EliminarLibro(int? idLibro)
        {
            var Book = db.Libroes.Where(x=>x.id_libro==idLibro).FirstOrDefault();
            Book.Activo = false;
            db.SaveChanges();
            return RedirectToAction("listaLibros", "Home");
        }
        public ActionResult EliminarCategoria(int? IDCategoria)
        {
            var Cat = db.Categoria_libro.Where(x => x.id_categoria_libro == IDCategoria).FirstOrDefault();
            Cat.Activo = false;
            db.SaveChanges();
            return RedirectToAction("listaCategorias", "Home");
        }
        public ActionResult EliminarCliente(int? idCliente)
        {
            var Client = db.Clientes.Where(x=>x.id_cliente==idCliente).FirstOrDefault();
            Client.Activo = false;
            db.SaveChanges();
            return RedirectToAction("listaClientes", "Home");
        }
        public ActionResult EliminarPersonal(int? idPersonal)
        {
            var Personal = db.Personals.Where(x=>x.id_personal==idPersonal).FirstOrDefault();
            Personal.Activo = false;
            db.SaveChanges();
            return RedirectToAction("listaPersonal", "Home");
        }
        public ActionResult pagarChecada(int? idChecada)
        {
            var Checada = db.Checadas.Where(x=>x.id_checada==idChecada).FirstOrDefault();
            Checada.Pagado = true;
            db.SaveChanges();
            return RedirectToAction("listaChecadas","Home");
        }
        public ActionResult EliminarTipoPrestamo(int? idtipoprestamo)
        {
            var TipoPrestamo = db.Tipo_De_Prestamo.Where(x=>x.id_tipo_prestamo==idtipoprestamo).FirstOrDefault();
            TipoPrestamo.activo= false;
            db.SaveChanges();
            return RedirectToAction("listaTipoPrestamo","Home");
        }
    }
}