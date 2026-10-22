using GeraPessoa.Contracts;
using Grpc.Core;

namespace GeraPessoa.Server;

public interface IGeraPessoa
{
    Pessoa GerarPessoa(string nome, string cpf);
}

public sealed class GeraPessoa : GeraPessoaService.GeraPessoaServiceBase, IGeraPessoa
{
    public override Task<PessoaResponse> GerarPessoa(
        PessoaRequest request,
        ServerCallContext context)
    {
        var pessoa = GerarPessoa(request.Nome, request.Cpf);

        return Task.FromResult(new PessoaResponse
        {
            Nome = pessoa.Nome,
            Cpf = pessoa.Cpf,
            Email = pessoa.Email
        });
    }

    public Pessoa GerarPessoa(string nome, string cpf)
    {
        return new Pessoa(nome, cpf);
    }
}
