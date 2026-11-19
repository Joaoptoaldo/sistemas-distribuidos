using System.Globalization;
using Encurtador.Models;
using Microsoft.Data.Sqlite;

namespace Encurtador.Repositories;

public sealed class LinkEncurtidoRepositorio
{
    private readonly string _connectionString;

    public string CaminhoBanco { get; }

    public LinkEncurtidoRepositorio()
    {
        string pastaDados = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData
            ),
            "Encurtador"
        );

        Directory.CreateDirectory(pastaDados);

        CaminhoBanco = Path.Combine(
            pastaDados,
            "encurtador.db"
        );

        var configuracao = new SqliteConnectionStringBuilder
        {
            DataSource = CaminhoBanco
        };

        _connectionString = configuracao.ToString();

        InicializarBanco();
    }

    private void InicializarBanco()
    {
        using var conexao = new SqliteConnection(_connectionString);

        conexao.Open();

        using var comando = conexao.CreateCommand();

        comando.CommandText = """
            CREATE TABLE IF NOT EXISTS Links (
                Codigo TEXT NOT NULL PRIMARY KEY,
                UrlOriginal TEXT NOT NULL,
                CriadoEm TEXT NOT NULL
            );

            CREATE UNIQUE INDEX IF NOT EXISTS IX_Links_UrlOriginal
            ON Links (UrlOriginal);
            """;

        comando.ExecuteNonQuery();
    }

    public bool TentarInserir(LinkEncurtado link)
    {
        using var conexao = new SqliteConnection(_connectionString);

        conexao.Open();

        using var comando = conexao.CreateCommand();

        comando.CommandText = """
            INSERT INTO Links (Codigo, UrlOriginal, CriadoEm)
            VALUES ($codigo, $urlOriginal, $criadoEm);
            """;

        comando.Parameters.AddWithValue(
            "$codigo",
            link.Codigo
        );

        comando.Parameters.AddWithValue(
            "$urlOriginal",
            link.UrlOriginal.AbsoluteUri
        );

        comando.Parameters.AddWithValue(
            "$criadoEm",
            link.CriadoEm.ToString(
                "O",
                CultureInfo.InvariantCulture
            )
        );

        try
        {
            comando.ExecuteNonQuery();
            return true;
        }
        catch (SqliteException ex) when (ex.SqliteErrorCode == 19)
        {
            // Código ou URL já cadastrado.
            return false;
        }
    }

    public LinkEncurtado? ObterPorCodigo(string codigo)
    {
        using var conexao = new SqliteConnection(_connectionString);

        conexao.Open();

        using var comando = conexao.CreateCommand();

        comando.CommandText = """
            SELECT Codigo, UrlOriginal, CriadoEm
            FROM Links
            WHERE Codigo = $codigo;
            """;

        comando.Parameters.AddWithValue("$codigo", codigo);

        using var leitor = comando.ExecuteReader();

        if (!leitor.Read())
            return null;

        return MapearLink(leitor);
    }

    public LinkEncurtado? ObterPorUrlOriginal(string urlOriginal)
    {
        using var conexao = new SqliteConnection(_connectionString);

        conexao.Open();

        using var comando = conexao.CreateCommand();

        comando.CommandText = """
            SELECT Codigo, UrlOriginal, CriadoEm
            FROM Links
            WHERE UrlOriginal = $urlOriginal;
            """;

        comando.Parameters.AddWithValue(
            "$urlOriginal",
            urlOriginal
        );

        using var leitor = comando.ExecuteReader();

        if (!leitor.Read())
            return null;

        return MapearLink(leitor);
    }

    public List<LinkEncurtado> ListarTodos()
    {
        var links = new List<LinkEncurtado>();

        using var conexao = new SqliteConnection(_connectionString);

        conexao.Open();

        using var comando = conexao.CreateCommand();

        comando.CommandText = """
            SELECT Codigo, UrlOriginal, CriadoEm
            FROM Links
            ORDER BY CriadoEm DESC;
            """;

        using var leitor = comando.ExecuteReader();

        while (leitor.Read())
        {
            links.Add(MapearLink(leitor));
        }

        return links;
    }

    public bool ExcluirPorCodigo(string codigo)
    {
        using var conexao = new SqliteConnection(_connectionString);

        conexao.Open();

        using var comando = conexao.CreateCommand();

        comando.CommandText = """
            DELETE FROM Links
            WHERE Codigo = $codigo;
            """;

        comando.Parameters.AddWithValue("$codigo", codigo);

        int registrosAfetados = comando.ExecuteNonQuery();

        return registrosAfetados > 0;
    }

    private static LinkEncurtado MapearLink(
        SqliteDataReader leitor)
    {
        string codigo = leitor.GetString(0);
        string urlOriginal = leitor.GetString(1);
        string criadoEm = leitor.GetString(2);

        DateTimeOffset dataCriacao = DateTimeOffset.ParseExact(
            criadoEm,
            "O",
            CultureInfo.InvariantCulture
        );

        return new LinkEncurtado(
            codigo,
            new Uri(urlOriginal, UriKind.Absolute),
            dataCriacao
        );
    }
}