
using Encurtador.Services;
using Encurtador.Infrastructure;

namespace Encurtador;

public sealed class FormPrincipal : Form
{
    private readonly ServidorHttp _servidor = new();
    private readonly EncurtadorService _servico = new();

    private readonly TextBox _txtUrl = new();
    private readonly TextBox _txtUrlEncurtada = new();
    private readonly TextBox _txtCodigo = new();
    private readonly TextBox _txtUrlOriginal = new();

    public FormPrincipal()
    {
        Text = "Encurtador de URLs";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(750, 620);
        MinimumSize = new Size(650, 550);

        MontarInterface();
    }

    private void MontarInterface()
    {
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(20)
        };

        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

        var titulo = new Label
        {
            Text = "Encurtador de URLs",
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };

        layout.Controls.Add(titulo, 0, 0);
        layout.Controls.Add(CriarGrupoEncurtar(), 0, 1);
        layout.Controls.Add(CriarGrupoConsultar(), 0, 2);

        Controls.Add(layout);

        Shown += FormPrincipal_Shown;
        FormClosed += FormPrincipal_FormClosed;
    }

    private GroupBox CriarGrupoEncurtar()
    {
        var grupo = new GroupBox
        {
            Text = " Encurtar URL ",
            Dock = DockStyle.Fill,
            Padding = new Padding(12)
        };

        var conteudo = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 5
        };

        conteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
        conteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
        conteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        conteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
        conteudo.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var lblUrl = new Label
        {
            Text = "Digite a URL original:",
            Dock = DockStyle.Fill
        };

        _txtUrl.Dock = DockStyle.Fill;

        var btnEncurtar = new Button
        {
            Text = "Encurtar URL",
            Width = 130,
            Height = 32
        };

        btnEncurtar.Click += BtnEncurtar_Click;

        var lblResultado = new Label
        {
            Text = "URL encurtada:",
            Dock = DockStyle.Fill
        };

        _txtUrlEncurtada.Dock = DockStyle.Fill;
        _txtUrlEncurtada.ReadOnly = true;
        _txtUrlEncurtada.Multiline = false;
        _txtUrlEncurtada.Visible = true;

        conteudo.Controls.Add(lblUrl, 0, 0);
        conteudo.Controls.Add(_txtUrl, 0, 1);
        conteudo.Controls.Add(btnEncurtar, 0, 2);
        conteudo.Controls.Add(lblResultado, 0, 3);
        conteudo.Controls.Add(_txtUrlEncurtada, 0, 4);

        grupo.Controls.Add(conteudo);

        return grupo;
    }

    private GroupBox CriarGrupoConsultar()
    {
        var grupo = new GroupBox
        {
            Text = " Consultar link ",
            Dock = DockStyle.Fill,
            Padding = new Padding(12)
        };

        var conteudo = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 5
        };

        conteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
        conteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
        conteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        conteudo.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
        conteudo.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var lblCodigo = new Label
        {
            Text = "Digite o código:",
            Dock = DockStyle.Fill
        };

        _txtCodigo.Dock = DockStyle.Fill;

        var btnConsultar = new Button
        {
            Text = "Consultar link",
            Width = 130,
            Height = 32
        };

        btnConsultar.Click += BtnConsultar_Click;

        var lblOriginal = new Label
        {
            Text = "URL original encontrada:",
            Dock = DockStyle.Fill
        };

        _txtUrlOriginal.Dock = DockStyle.Fill;
        _txtUrlOriginal.ReadOnly = true;
        _txtUrlOriginal.Multiline = false;

        conteudo.Controls.Add(lblCodigo, 0, 0);
        conteudo.Controls.Add(_txtCodigo, 0, 1);
        conteudo.Controls.Add(btnConsultar, 0, 2);
        conteudo.Controls.Add(lblOriginal, 0, 3);
        conteudo.Controls.Add(_txtUrlOriginal, 0, 4);

        grupo.Controls.Add(conteudo);

        return grupo;
    }

    private void BtnEncurtar_Click(object? sender, EventArgs e)
    {
        if (!ValidadorUrl.TentarNormalizar(
                _txtUrl.Text,
                out Uri? url))
        {
            MessageBox.Show(
                this,
                "Informe uma URL válida.",
                "URL inválida",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            return;
        }

        try
        {
            var link = _servico.Criar(url!);
            string urlEncurtada = _servico.ObterUrlEncurtada(link);

            _txtUrlEncurtada.Text = urlEncurtada;
            _txtUrlEncurtada.SelectionStart = 0;
            _txtUrlEncurtada.SelectionLength = 0;

            _txtCodigo.Text = link.Codigo;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"Não foi possível encurtar a URL.\n\n{ex.Message}",
                "Erro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }

    private void BtnConsultar_Click(object? sender, EventArgs e)
    {
        string codigo = _txtCodigo.Text.Trim();

        if (string.IsNullOrWhiteSpace(codigo))
        {
            MessageBox.Show(
                this,
                "Informe o código do link.",
                "Consulta",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            return;
        }

        if (_servico.TentarBuscar(codigo, out var link))
        {
            _txtUrlOriginal.Text = link!.UrlOriginal.AbsoluteUri;
        }
        else
        {
            _txtUrlOriginal.Clear();

            MessageBox.Show(
                this,
                "Link não encontrado.",
                "Consulta",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }

    private async void FormPrincipal_Shown(object? sender, EventArgs e)
    {
        try
        {
            await _servidor.IniciarAsync(_servico);

            Text = "Encurtador de URLs - Servidor ativo";
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"Não foi possível iniciar o servidor HTTP.\n\n{ex.Message}",
                "Erro ao iniciar servidor",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }

    private async void FormPrincipal_FormClosed(
        object? sender,
        FormClosedEventArgs e)
    {
        await _servidor.DisposeAsync();
    }
}
