
namespace TEST_DEV_EPA_26072024.models
{

    public class PersonaFisica
    {
        public int IdPersonaFisica {get; set;}
        public DateTime FechaRegistro {get; set;} 
        public DateTime FechaActualizacion {get; set;}
        public string Nombre {get; set;}
        public string ApellidoPaterno {get; set;}
        public string ApellidoMaterno {get; set;}
        public string RFC {get; set;}
        public DateTime FechaNacimiento {get; set;}
        public int UsuarioAgrega {get; set;}
        public bool Activo {get; set;}

        public PersonaFisica(){
            Nombre??="";
            ApellidoPaterno??="";
            ApellidoMaterno??="";
            RFC??="";
        }

    }


}













