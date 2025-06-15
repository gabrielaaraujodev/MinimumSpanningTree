using System.Security.Cryptography.X509Certificates;

public class No
{
    private string nome { get; set; }

    public No (string nome)
    {
        this.nome = nome;
    }
    public string Nome
    {
        get { return nome; }
        set { nome = value; }
    }

}

public class Aresta
{
    private No origem { get; set; }
    private No destino { get; set; }
    private int peso { get; set; }

    public No Origem
    {
        get { return origem; }
        set { origem = value; }
    }

    public No Destino
    {
        get { return destino; }
        set { destino = value; }
    }

    public int Peso
    {
        get { return peso; }
        set { peso = value; }
    }

    public Aresta (No origem, No destino, int peso)
    {
        Origem = origem;
        Destino = destino;
        Peso = peso;
    }
}

public class Grafo : IManipuladorGrafo
{
    private List<No> nos { get; set; }
    private List<Aresta> arestas { get; set; }

    public Grafo()
    {
        nos = new List<No>();
        arestas = new List<Aresta>();
    }

    public void LerArquivoDoGrafo(string caminhoArquivo)
    {
        string[] linhas = File.ReadAllLines(caminhoArquivo);

        for (int i = 1; i < linhas.Length; i++)
        {
            string linhaIndividual = linhas[i];

            string[] linhaIndividualSeparada = linhaIndividual.Split(',');

            string nomeOrigem = linhaIndividualSeparada[0];
            string nomeDestino = linhaIndividualSeparada[1];


            No noOrigem = nos.FirstOrDefault(n => n.Nome == nomeOrigem);

            if (!nos.Contains(noOrigem))
            {
                noOrigem = new No(nomeOrigem);
                nos.Add(noOrigem);
            }

            No noDestino = nos.FirstOrDefault(n => n.Nome == nomeDestino); 

            if (!nos.Contains(noDestino))
            {
                noDestino = new No(nomeDestino);
                nos.Add(noDestino);
            }

            if (int.TryParse(linhaIndividualSeparada[2], out int valor))
            {
                Aresta novaAresta = new Aresta(noOrigem, noDestino, valor);
                arestas.Add(novaAresta);
            }

        }
    }

    public void DesenharGrafo()
    {
        foreach (var aresta in arestas)
        {
            Console.WriteLine($"{aresta.Origem.Nome} --{aresta.Peso}-- {aresta.Destino.Nome}");
        }
    }
}

public interface IManipuladorGrafo
{
    public void LerArquivoDoGrafo(string caminhoArquivo);
    public void DesenharGrafo();
}