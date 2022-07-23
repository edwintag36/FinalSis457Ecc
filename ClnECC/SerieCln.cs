using CadECC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnECC
{
    public class SerieCln
    {
        public static int insertar(Serie serie) // INSERT INTO Producto VALUES (...)
        {
            using (var contexto = new LabECCEntities())
            {
                contexto.Serie.Add(serie);
                contexto.SaveChanges();
                return serie.id;
            }
        }

        public static int actualizar(Serie serie) // UPDATE Producto SET ...=.... WHERE id=..
        {
            using (var contexto = new LabECCEntities())
            {
                var existente = contexto.Serie.Find(serie.id);
                existente.titulo = serie.titulo;
                existente.sinopsis = serie.sinopsis;
                existente.director = serie.director;
                existente.duracion = serie.duracion;
                
                existente.usuarioRegistro = serie.usuarioRegistro;

                return contexto.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuario) // UPDATE Producto SET ...=.... WHERE id=..
        {
            using (var contexto = new LabECCEntities())
            {
                var existente = contexto.Serie.Find(id);
                existente.registroActivo = false;
                existente.usuarioRegistro = usuario;
                return contexto.SaveChanges();
            }
        }

        public static Serie get(int id) // SELECT * FROM Producto WHERE id=..
        {
            using (var contexto = new LabECCEntities())
            {
                return contexto.Serie.Find(id);
            }
        }

        public static List<Serie> listar() // SELECT * FROM Producto WHERE registroActivo=1
        {
            using (var contexto = new LabECCEntities())
            {
                return contexto.Serie.Where(x => x.registroActivo.Value).ToList();
            }
        }

        public static List<paSerieListar_Result> listarPa(string parametro) // EXEC paProductoListar ''
        {
            using (var contexto = new LabECCEntities())
            {
                return contexto.paSerieListar(parametro).ToList();
            }
        }
    }
}
