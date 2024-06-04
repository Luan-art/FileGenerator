using Models;
using Repositories;

namespace Services
{
    public class FileGeneratorService
    {
        private IRadarRepository iRadarRepository;
        private IRadarRepository iMongoRepository;

        public FileGeneratorService()
        {
            iRadarRepository = new SqlRepository();
            iMongoRepository = new MongoRepository();
        }

        public List<Radar> RecuperarSQL()
        {
            try
            {
                List<Radar> radares = iRadarRepository.Recuperar();

                return radares;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Radar> RecuperarMongo()
        {
            try
            {
                List<Radar> radares = iMongoRepository.Recuperar();

                return radares;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
