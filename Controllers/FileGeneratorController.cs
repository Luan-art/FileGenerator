
using Models;
using Services;

namespace Controllers
{
    public class FileGeneratorController
    {
        private FileGeneratorService sileGeneratorService;

        public FileGeneratorController() { 
        
            sileGeneratorService = new FileGeneratorService();
        }

        public List<Radar> RecuperarMongo()
        {
            List<Radar> radares = sileGeneratorService.RecuperarMongo();

            return radares;
        }

        public List<Radar> RecuperarSQL()
        {
            List<Radar> radares = sileGeneratorService.RecuperarSQL();

            return radares;
        }
    }
}
