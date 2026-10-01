using Cliente.Controllers;

namespace Cliente.Views;

/// <summary>
/// Apresenta o cadastro do cliente e a solicitação periódica de token
/// </summary>
public class CadastroView : Form
{
    private readonly TextBox _campoNome;
    private readonly TextBox _campoEmail;
    private readonly Button _botaoCadastrar;
    private readonly Label _mensagem;

    private readonly Label _labelToken;
    private readonly Button _botaoToken;

    private readonly ClienteController _controller;
    private readonly System.Windows.Forms.Timer _timerToken;

    private bool _cadastrado;
    private bool _solicitandoToken;

    public CadastroView()
    {
        _controller = new ClienteController();

        Text = "Cadastro de Cliente";
        Width = 400;
        Height = 360;
        StartPosition = FormStartPosition.CenterScreen;

        Label labelNome = new()
        {
            Text = "Nome completo:",
            Left = 30,
            Top = 30,
            Width = 120
        };

        _campoNome = new()
        {
            Left = 30,
            Top = 55,
            Width = 320
        };

        Label labelEmail = new()
        {
            Text = "E-mail:",
            Left = 30,
            Top = 95,
            Width = 120
        };

        _campoEmail = new()
        {
            Left = 30,
            Top = 120,
            Width = 320
        };

        _botaoCadastrar = new()
        {
            Text = "Cadastrar",
            Left = 30,
            Top = 165,
            Width = 100
        };

        _mensagem = new()
        {
            Left = 30,
            Top = 210,
            Width = 320,
            Height = 40
        };

        _labelToken = new()
        {
            Text = "Token: -",
            Left = 30,
            Top = 255,
            Width = 200
        };

        _botaoToken = new()
        {
            Text = "Solicitar token",
            Left = 230,
            Top = 250,
            Width = 120,
            Enabled = false
        };

        _botaoCadastrar.Click += BotaoCadastrar_Click;
        _botaoToken.Click += BotaoToken_Click;

        Controls.Add(labelNome);
        Controls.Add(_campoNome);

        Controls.Add(labelEmail);
        Controls.Add(_campoEmail);

        Controls.Add(_botaoCadastrar);
        Controls.Add(_mensagem);

        Controls.Add(_labelToken);
        Controls.Add(_botaoToken);

        _timerToken = new System.Windows.Forms.Timer
        {
            Interval = 30000
        };

        _timerToken.Tick += TimerToken_Tick;
    }

    private async void BotaoCadastrar_Click(object? sender, EventArgs e)
    {
        string nome = _campoNome.Text.Trim();
        string email = _campoEmail.Text.Trim();

        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(email))
        {
            _mensagem.Text = "Informe nome e e-mail.";
            return;
        }

        _botaoCadastrar.Enabled = false;
        _mensagem.Text = "Cadastrando...";

        try
        {
            var resultado = await Task.Run(
                () => _controller.CadastrarPessoa(nome, email)
            );

            _mensagem.Text = resultado.Mensagem;

            if (resultado.Sucesso)
            {
                _cadastrado = true;
                _botaoToken.Enabled = true;
                _timerToken.Start();

                await SolicitarTokenAsync();
            }
        }
        catch (Exception ex)
        {
            _mensagem.Text = $"Erro: {ex.Message}";
        }
        finally
        {
            _botaoCadastrar.Enabled = true;
        }
    }

    private async void BotaoToken_Click(object? sender, EventArgs e)
    {
        await SolicitarTokenAsync();
    }

    private async void TimerToken_Tick(object? sender, EventArgs e)
    {
        await SolicitarTokenAsync();
    }

    private async Task SolicitarTokenAsync()
    {
        if (!_cadastrado || _solicitandoToken)
        {
            return;
        }

        _solicitandoToken = true;
        _botaoToken.Enabled = false;

        try
        {
            var resultado = await Task.Run(
                () => _controller.SolicitarToken()
            );

            _labelToken.Text = resultado.Sucesso
                ? $"Token: {resultado.Mensagem}"
                : resultado.Mensagem;
        }
        catch (Exception ex)
        {
            _labelToken.Text = $"Erro: {ex.Message}";
        }
        finally
        {
            _botaoToken.Enabled = true;
            _solicitandoToken = false;
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _timerToken.Stop();
        _controller.Dispose();

        base.OnFormClosed(e);
    }
}