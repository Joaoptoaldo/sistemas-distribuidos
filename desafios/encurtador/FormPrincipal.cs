using Encurtador.Services;
using Encurtador.Infrastructure;
using Encurtador.Models;
using System.Diagnostics;

namespace Encurtador;

public sealed class FormPrincipal : Form
{
    private readonly ServidorHttp _servidor = new();
    private readonly EncurtadorService _servico = new();

    private readonly TextBox _txtUrl = new();
    private readonly TextBox _txtUrlEncurtada = new();
    private readonly TextBox _txtCodigo = new();
    private readonly TextBox _txtUrlOriginal = new();

    private readonly DataGridView _gridLinks = new();

    public FormPrincipal()
    {
        Text = "Encurtador de URLs";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(1000, 750);
        MinimumSize = new Size(800, 650);

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
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 60));

        var titulo = new Label
        {
            Text = "Encurtador de URLs",
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };

        var gruposSuperiores = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1
        };

        gruposSuperiores.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 50)
        );

        gruposSuperiores.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 50)
        );

        gruposSuperiores.Controls.Add(CriarGrupoEncurtar(), 0, 0);
        gruposSuperiores.Controls.Add(CriarGrupoConsultar(), 1, 0);

        layout.Controls.Add(titulo, 0, 0);
        layout.Controls.Add(gruposSuperiores, 0, 1);
        layout.Controls.Add(CriarGrupoListagem(), 0, 2);

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

        conteudo.Controls.Add(lblCodigo, 0, 0);
        conteudo.Controls.Add(_txtCodigo, 0, 1);
        conteudo.Controls.Add(btnConsultar, 0, 2);
        conteudo.Controls.Add(lblOriginal, 0, 3);
        conteudo.Controls.Add(_txtUrlOriginal, 0, 4);

        grupo.Controls.Add(conteudo);

        return grupo;
    }

   
    private GroupBox CriarGrupoListagem()
    {
        var grupo = new GroupBox
        {
            Text = " Links cadastrados ",
            Dock = DockStyle.Fill,
            Padding = new Padding(12)
        };

        var conteudo = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2
        };

        conteudo.RowStyles.Add(
            new RowStyle(SizeType.Absolute, 40)
        );

        conteudo.RowStyles.Add(
            new RowStyle(SizeType.Percent, 100)
        );

        var barraAcoes = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false
        };

        var btnAtualizar = new Button
        {
            Text = "Atualizar lista",
            Width = 130,
            Height = 30
        };

        btnAtualizar.Click += (_, _) => AtualizarListagem();

        var btnExcluir = new Button
        {
            Text = "Excluir selecionado",
            Width = 150,
            Height = 30
        };

        btnExcluir.Click += BtnExcluir_Click;

        barraAcoes.Controls.Add(btnAtualizar);
        barraAcoes.Controls.Add(btnExcluir);

        ConfigurarTabela();

        conteudo.Controls.Add(barraAcoes, 0, 0);
        conteudo.Controls.Add(_gridLinks, 0, 1);

        grupo.Controls.Add(conteudo);

        return grupo;
    }


    private void ConfigurarTabela()
    {
        _gridLinks.Dock = DockStyle.Fill;

        _gridLinks.ReadOnly = true;
        _gridLinks.AllowUserToAddRows = false;
        _gridLinks.AllowUserToDeleteRows = false;
        _gridLinks.AllowUserToResizeRows = false;

        _gridLinks.RowHeadersVisible = false;
        _gridLinks.MultiSelect = false;

        _gridLinks.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;

        _gridLinks.AutoGenerateColumns = false;

        _gridLinks.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;

        _gridLinks.CellDoubleClick += GridLinks_CellDoubleClick;

        _gridLinks.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Codigo",
            HeaderText = "Código",
            DataPropertyName = nameof(LinkEncurtado.Codigo),
            FillWeight = 15
        });

        _gridLinks.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "UrlOriginal",
            HeaderText = "URL original",
            DataPropertyName = nameof(LinkEncurtado.UrlOriginal),
            FillWeight = 65
        });

        _gridLinks.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "CriadoEm",
            HeaderText = "Criado em",
            DataPropertyName = nameof(LinkEncurtado.CriadoEm),
            FillWeight = 20,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "dd/MM/yyyy HH:mm:ss"
            }
        });

        _gridLinks.DataError += (_, e) =>
        {
            e.ThrowException = false;
        };
    }


    private void BtnExcluir_Click(object? sender, EventArgs e)
    {
        if (_gridLinks.SelectedRows.Count == 0)
        {
            MessageBox.Show(
                this,
                "Selecione um link na tabela para excluí-lo.",
                "Nenhum link selecionado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            return;
        }

        string? codigo = _gridLinks.SelectedRows[0]
            .Cells["Codigo"]
            .Value?
            .ToString();

        if (string.IsNullOrWhiteSpace(codigo))
            return;

        DialogResult confirmacao = MessageBox.Show(
            this,
            $"Tem certeza que deseja excluir o link com código '{codigo}'?\n\n" +
            "Essa operação não poderá ser desfeita.",
            "Confirmar exclusão",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2
        );

        if (confirmacao != DialogResult.Yes)
            return;

        try
        {
            bool excluido = _servico.Excluir(codigo);

            if (!excluido)
            {
                MessageBox.Show(
                    this,
                    "O link não foi encontrado ou já foi excluído.",
                    "Exclusão não realizada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                AtualizarListagem();
                return;
            }

            if (_txtCodigo.Text.Trim() == codigo)
            {
                _txtCodigo.Clear();
                _txtUrlOriginal.Clear();
            }

            if (_txtUrlEncurtada.Text.EndsWith(
                    $"/{codigo}",
                    StringComparison.Ordinal))
            {
                _txtUrlEncurtada.Clear();
            }

            AtualizarListagem();

            MessageBox.Show(
                this,
                "Link excluído com sucesso.",
                "Exclusão",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"Não foi possível excluir o link.\n\n{ex.Message}",
                "Erro ao excluir",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }


    private void AtualizarListagem()
    {
        try
        {
            var links = _servico.ListarTodos();

            var dados = links.Select(link => new
            {
                link.Codigo,
                UrlOriginal = link.UrlOriginal.AbsoluteUri,
                CriadoEm = link.CriadoEm
                    .ToLocalTime()
                    .ToString("dd/MM/yyyy HH:mm:ss")
            }).ToList();

            _gridLinks.DataSource = dados;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"Não foi possível carregar os links.\n\n{ex.Message}",
                "Erro ao carregar listagem",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }

    private void GridLinks_CellDoubleClick(
        object? sender,
        DataGridViewCellEventArgs e)
    {
        // Ignora cliques no cabeçalho da tabela.
        if (e.RowIndex < 0)
            return;

        string? urlOriginal = _gridLinks
            .Rows[e.RowIndex]
            .Cells["UrlOriginal"]
            .Value?
            .ToString();

        if (!Uri.TryCreate(
                urlOriginal,
                UriKind.Absolute,
                out Uri? url))
        {
            MessageBox.Show(
                this,
                "Não foi possível identificar a URL original.",
                "URL inválida",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            return;
        }

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url.AbsoluteUri,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"Não foi possível abrir a URL no navegador.\n\n{ex.Message}",
                "Erro ao abrir URL",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
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

            string urlEncurtada =
                _servico.ObterUrlEncurtada(link);

            _txtUrlEncurtada.Text = urlEncurtada;

            _txtCodigo.Text = link.Codigo;

            AtualizarListagem();
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

    private async void FormPrincipal_Shown(
        object? sender,
        EventArgs e)
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

        AtualizarListagem();
    }

    private async void FormPrincipal_FormClosed(
        object? sender,
        FormClosedEventArgs e)
    {
        await _servidor.DisposeAsync();
    }
}
