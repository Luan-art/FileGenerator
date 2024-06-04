using Controllers;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

Console.WriteLine("Inicio...");

int op = 0;
List<Radar> radars = new List<Radar>();

while (true)
{
    Console.WriteLine("Digite se deseja Recuperar do SQL ou do Mongo ( 1 ou 2 ) respectivamente");
    op = int.Parse(Console.ReadLine());

    switch (op)
    {
        case 1:
            Console.WriteLine("Digite se deseja Recuperar em JSONM XML OU CSV ( 1, 2 ou 3 ) respectivamente");
            op = int.Parse(Console.ReadLine());

            radars = new FileGeneratorController().RecuperarSQL();
            Thread thread1 = new Thread(() => GerarJSON(radars));
            Thread thread2 = new Thread(() => GerarXML(radars));
            Thread thread3 = new Thread(() => GerarCSV(radars));

            switch (op)
            {
                case 1:
                    thread1.Start();
                    thread1.Join();
                    break;
                case 2:
                    thread2.Start();
                    thread2.Join();
                    break;
                case 3:
                    thread3.Start();
                    thread3.Join();
                    break;
                default:
                    Console.WriteLine("Invalid");
                    break;
            }

            break;
        case 2:
            Console.WriteLine("Digite se deseja Recuperar em JSONM XML OU CSV ou do Mongo ( 1, 2 ou 3 ) respectivamente");
            op = int.Parse(Console.ReadLine());

            radars = new FileGeneratorController().RecuperarSQL();
            Thread thread4 = new Thread(() => GerarJSON(radars));
            Thread thread5 = new Thread(() => GerarXML(radars));
            Thread thread6 = new Thread(() => GerarCSV(radars));

            switch (op)
            {
                case 1:
                    thread4.Start();
                    thread4.Join();
                    break;
                case 2:
                    thread5.Start();
                    thread5.Join();
                    break;
                case 3:
                    thread6.Start();
                    thread6.Join();
                    break;
                default:
                    Console.WriteLine("Invalid");
                    break;
            }

            break;
        default:
            Console.WriteLine("Invalido");
            break;
    }
}

void GerarCSV(List<Radar> radars)
{
    if (radars.Count > 0)
    {
        StringBuilder csvData = new StringBuilder();
        csvData.AppendLine("Concessionaria,Ano do PNV/SNV,Tipo de Radar,Rodovia,UF,KM,Município,Tipo de Pista,Sentido,Situação,Data de Inativação,Latitude,Longitude,Velocidade Leve");

        foreach (Radar radar in radars)
        {
            string inactivatedDates = radar.data_da_inativacao?.Select(x => x.ToString("yyyy-MM-dd")).Aggregate((a, b) => $"{a},{b}");
            csvData.AppendLine($"{radar.Concessionaria},{radar.ano_do_pnv_snv},{radar.tipo_de_radar},{radar.Rodovia},{radar.Uf},{radar.km_m},{radar.Municipio},{radar.tipo_pista},{radar.Sentido},{radar.Situacao},{inactivatedDates}," +
                               $"{radar.Latitude},{radar.Longitude},{radar.velocidade_leve}");
        }

        Console.WriteLine(csvData.ToString()); 
    }

}

void GerarXML(List<Radar> radars)
{
    if (radars.Count > 0)
    {
        var penalidadeAplicada = new XElement("Root", from data in radars
                                                      select new XElement("Radar",
                                                          new XElement("concessionaria", data.Concessionaria),
                                                          new XElement("ano_do_pnv_snv", data.ano_do_pnv_snv),
                                                          new XElement("tipo_de_radar", data.tipo_de_radar),
                                                          new XElement("rodovia", data.Rodovia),
                                                          new XElement("uf", data.Uf),
                                                          new XElement("km_m", data.km_m),
                                                          new XElement("municipio", data.Municipio),
                                                          new XElement("tipo_pista", data.tipo_pista),
                                                          new XElement("sentido", data.Sentido),
                                                          new XElement("situacao", data.Situacao),
                                                          data.data_da_inativacao != null ? new XElement("data_da_inativacao", string.Join(",", data.data_da_inativacao.Select(x => x.ToString("yyyy-MM-dd")))) : null,
                                                          new XElement("latitude", data.Latitude),
                                                          new XElement("longitude", data.Longitude),
                                                          new XElement("velocidade_leve", data.velocidade_leve)));
      
        Console.WriteLine(penalidadeAplicada.ToString()); 
    }

}

void GerarJSON(List<Radar> radars)
{
    if (radars.Count > 0)
    {
        Console.WriteLine(JsonConvert.SerializeObject(radars, Formatting.Indented)); 
    }

}