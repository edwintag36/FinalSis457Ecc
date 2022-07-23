using CadECC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnECC
{
    public class UsuarioCln
    {
        public static int insertar(Usuario usuario) 
        {
            using (var contexto = new LabECCEntities())
            {
                contexto.Usuario.Add(usuario);
                contexto.SaveChanges();
                return usuario.id;
            }
        }

        public static int actualizar(Usuario usuario) 
        {
            using (var contexto = new LabECCEntities())
            {
                var existente = contexto.Usuario.Find(usuario.id);
                existente.usuario = usuario.usuario;

                return contexto.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuario) 
        {
            using (var contexto = new LabECCEntities())
            {
                var existente = contexto.Usuario.Find(id);
                existente.registroActivo = false;
                
                return contexto.SaveChanges();
            }
        }

        public static Usuario get(int id)
        {
            using (var contexto = new LabECCEntities())
            {
                return contexto.Usuario.Find(id);
            }
        }

        public static Usuario validar(string usuario, string clave)
        {
            using (var contexto = new LabECCEntities())
            {
                return contexto.Usuario.Where(x => x.usuario == usuario && x.clave == clave)
                    .FirstOrDefault();
            }
        }

        public static List<Usuario> listar() 
        {
            using (var contexto = new LabECCEntities())
            {
                return contexto.Usuario.Where(x => x.registroActivo.Value).ToList();
            }
        }

        
    }
}
