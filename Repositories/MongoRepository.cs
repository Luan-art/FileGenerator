using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace Repositories
{
    public class MongoRepository : IRadarRepository
    {
        string strConn = "mongodb://root:Mongo%402024%23@localhost:27017/";
        public MongoRepository() { }

        public List<Radar> Recuperar()
        {
            Console.WriteLine("Recuperando dados do MongoDB");
            try
            {
                MongoClient client = new MongoClient(strConn);
                var database = client.GetDatabase("ListaRadares");
                var collection = database.GetCollection<BsonDocument>("Radar");

                var filter = new BsonDocument();
                var cursor = collection.Find(filter);

                List<Radar> radares = new List<Radar>();
                foreach (BsonDocument document in cursor.ToList())
                {
                    radares.Add(new Radar
                    {
                        Concessionaria = document["Concessionaria"].ToString(),
                        ano_do_pnv_snv = document["ano_do_pnv_snv"].ToInt32(),
                        tipo_de_radar = document["tipo_de_radar"].ToString(),
                        Rodovia = document["Rodovia"].ToString(),
                        Uf = document["Uf"].ToString(),
                        km_m = document["km_m"].ToDouble(),
                        Municipio = document["Municipio"].ToString(),
                        tipo_pista = document["tipo_pista"].ToString(),
                        Sentido = document["Sentido"].ToString(),
                        Situacao = document["Situacao"].ToString(),
                        data_da_inativacao = document["data_da_inativacao"].IsBsonArray ? document["data_da_inativacao"].AsBsonArray.Select(x => x.ToLocalTime()).ToArray() : null,
                        Latitude = document["Latitude"].ToDouble(),
                        Longitude = document["Longitude"].ToDouble(),
                        velocidade_leve = document["velocidade_leve"].ToInt32(),
                    });
                }

                return radares;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao recuperar dados do MongoDB: " + ex.Message);
                return null;
            }
        }
    }
}